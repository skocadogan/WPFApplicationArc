using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using System.Windows;
using WPFApplicationArc.Models;
using WPFApplicationArc.Services.ServiceInterfaces;

namespace WPFApplicationArc.Pages
{
    /// <summary>
    /// PersonDetail.xaml etkileşim mantığı
    /// Burada Edit ve Update Aynı yerde yapılacaktır.
    /// </summary>
    public partial class PersonDetailForm : Window
    {
        
        // Ana ekranda seçilen personeli atamak için.
        public Person selectedPerson = new Person();
        
        // Eğer ana ekrana bir nesne atmamız gerekirse 
        // bu nesneyi ana ekranda kullanmak için.
        public Person createdPerson = new Person();
        
        
        // Bu pencere hem edit hem new yaptığı için 
        // ne zaman hangi işi yapacak ayırmak için
        public bool NewPerson = false;

        // Bu pencerenin kullanacağı servislerin tanımlamaları
        // Yazımı kolay olsun diye tanımlandı. _services üzerinden de 
        // Kod içerisinde çağırılabilirdi.
        private readonly IServiceProvider _services;
        
        private readonly IPersonService _personService;
        private readonly IDepartmentService _departmentService;
       

        // Yukarıdaki person ve department service'lerini buraya 
        // inject etmedik. Belki 8-10 servis kullanılır kalabalık
        // gözükmesini istemedik. O yüzden içeride gerekli servisleri
        // tanımladık.
        public PersonDetailForm(IServiceProvider _services)
        {
            InitializeComponent();
            _personService = _services.GetRequiredService<IPersonService>();
            _departmentService = _services.GetRequiredService<IDepartmentService>();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            
            // Main Window'dan çağırırken gerekli olacak. 
            DataContext = selectedPerson;
            
            // Bu combobox'a veri yüklemek için gerekli.
            DeparmentListBox.ItemsSource = _departmentService.GetAll().ToList();
        }

        // Kayıt ya da güncelleme Butonu
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (NewPerson)
            {
                _personService.Add(selectedPerson);
                createdPerson = selectedPerson;
            }
            else
            {
                _personService.Update(selectedPerson);
            }

            // Ana Ekranda bu duruma göre yapılacak işler için
            // Close kullanılmıyor..!!! Dikkat.
            // Çünkü burası MainWindow'da ShowDialog olarak çağırılıyor.
            // Eğer işlem başarılıysa true olarak döner.
            this.DialogResult = true;
        }
    }
}
