using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using System;
using System.IO;
using System.Windows;
using Microsoft.Data.Sqlite;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using WPFApplicationArc.DBContexts;
using WPFApplicationArc.Services;
using WPFApplicationArc.Pages;
using WPFApplicationArc.Services.ServiceInterfaces;
using System.Diagnostics;

namespace WPFApplicationArc
{
    public partial class App : Application
    {
        /// <summary>
        /// Dependency Incejtion İçin Host
        /// </summary>
        private readonly IHost host;
        
        /// <summary>
        /// appsettings.json dosyasındaki 
        /// Bilgileri okumak için
        /// DB bilgisi burada
        /// </summary>
        public static IConfiguration? _configuration { get; private set; }

        public App()
        {

            // apsettings.json dosyasının yolu
            // Bunu build esnasında build klasörüne kopyalanması gerektiğini unutmayın.
            IConfigurationBuilder configurationBuilder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
           
            _configuration = configurationBuilder.Build();
            string connectionString = _configuration.GetConnectionString("AppContextConnection") ?? throw new InvalidOperationException("Connection string 'AppContextConnection' not found.");

            // SQLite DB bağlantısı için ayarlamalar
            var connectionStringBuilder = new SqliteConnectionStringBuilder { DataSource = connectionString };
            var appConnectionString = connectionStringBuilder.ToString();

            // Dependency Injection Hostu yapılandırılıyor
            host = Host.CreateDefaultBuilder()
               .ConfigureServices(services =>
               {
                   // Veri Tabanı Contexti 
                   services.AddDbContext<AppDBContext>(o =>
                   {
                       o.UseSqlite(new SqliteConnection(appConnectionString));
                       o.UseLazyLoadingProxies();
                   });

                   // İhtiyaç duyulan servislerin ana tanımları burada yüklenecek.
                   services.AddScoped<IPersonService, PersonService>();
                   services.AddScoped<IDepartmentService, DepartmentService>();

                   // Çağırılacak ekranların tanımlamaları
                   services.AddTransient<MainWindow>();
                   services.AddTransient<PersonDetailForm>();
                   services.AddTransient<DepartmentForm>();
                   

               }).Build();

        }

        protected override async void OnStartup(StartupEventArgs e)
        {
            Debug.WriteLine("Yükleniyor...");
            // Açılış Ekranı.
            // İşlemler yapılırken Gösterilmesi için
            var splashScreen = new Pages.SplashScreen();
            splashScreen.Show();

            await host.StartAsync();
            var scope = host.Services.CreateScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<AppDBContext>();
            
            // Veritabanı yoksa sıfırdan yarat.
            await dbContext.Database.EnsureCreatedAsync();
            
            // Ana Ekran tanımlanıp gösteriliyor.
            var mainWindow = scope.ServiceProvider.GetRequiredService<MainWindow>();

            mainWindow.Show();

            Debug.WriteLine("Yüklendi...");
            // Açılış ekranını kapat.
            splashScreen.Close();
            
            base.OnStartup(e);
        }

        protected override async void OnExit(ExitEventArgs e)
        {
            // Program kapatıldığında Dependency Injection Container'i hafızadan sil.
            await host.StopAsync();
            host.Dispose();
            base.OnExit(e);
        }
    }
}
