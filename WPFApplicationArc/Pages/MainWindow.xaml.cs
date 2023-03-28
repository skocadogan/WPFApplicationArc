using Microsoft.Extensions.DependencyInjection;
using System;
using System.Diagnostics;
using System.Windows;
using WPFApplicationArc.Models;
using WPFApplicationArc.Pages;
using WPFApplicationArc.Services.ServiceInterfaces;

namespace WPFApplicationArc
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        // Servis Çağırma Arabirimi
        private readonly IServiceProvider _services;

        // Ekran Foknsiyonlarında kullanılacak servis tanımı
        // Anlaşılabilirlik açısından tanımlandı
        // _services üzerinden de çağırılabilirdi.
        private readonly IPersonService _personService;

        // Eğer bu ekran başka ekranlar çağıracaksa bu şekilde kullanıyoruz.
        // Başka ekran çağırmayacak ise sadece DB ile ilgili servisi set etmemiz
        // yeterli. Bkz. DepartmentForm.xaml.cs
        public MainWindow(IServiceProvider services)
        {
            InitializeComponent();
            _services = services;
            _personService = services.GetRequiredService<IPersonService>();

        }

        // Gride datayı yükler
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var persons = _personService.GetPeople();
            personDataGrid.ItemsSource = persons;
        }

        // Gridi Temizler
        private void Button_Click1(object sender, RoutedEventArgs e)
        {
            personDataGrid.ItemsSource = null;
        }

        // Grid üzerinde seçilen satırı güncellemek için ekran açar.
        private void Button_Click2(object sender, RoutedEventArgs e)
        {
            // Formlarda bir servis gibi çağırılıyor.
            var pfrm = _services.GetRequiredService<PersonDetailForm>();

            pfrm.BtnEditOrNew.Content = "Güncelle";
            pfrm.NewPerson = false;
            pfrm.selectedPerson = (Person)personDataGrid.SelectedItem;

            if (pfrm.ShowDialog() == true)
            {
                personDataGrid.ItemsSource = _personService.GetPeople();
            }
        }

        // Yeni bir satır ekler
        private void Button_Click3(object sender, RoutedEventArgs e)
        {
            var pfrm = _services.GetRequiredService<PersonDetailForm>();

            pfrm.NewPerson = true;
            pfrm.BtnEditOrNew.Content = "Ekle";
            if (pfrm.ShowDialog() == true)
            {
                if (pfrm.createdPerson != null)
                {
                    Debug.WriteLine(pfrm.createdPerson.Name?.ToString());
                }

                personDataGrid.ItemsSource = _personService.GetPeople();

            }
        }
        // Yeni Departman Ekler..
        private void Button_Click4(object sender, RoutedEventArgs e)
        {
            var depFrm = _services.GetRequiredService<DepartmentForm>();
            if (depFrm.ShowDialog() == true)
            {
                MessageBox.Show("Yeni Bölüm Eklendi");
            }
        }
    }
}
