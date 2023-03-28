using System.Windows;
using WPFApplicationArc.Models;
using WPFApplicationArc.Services.ServiceInterfaces;

namespace WPFApplicationArc.Pages
{
    /// <summary>
    /// DepartmentForm.xaml etkileşim mantığı
    /// </summary>
    public partial class DepartmentForm : Window
    {
        // Bu ekranda sadece bir servis yeterli idi.
        // Bu yüzden personDetail formundaki gibi bir tanımlama
        // yapmadık. Direk servisi inject ettik.
        private readonly IDepartmentService _departmentService;
        public DepartmentForm(IDepartmentService departmentService)
        {
            InitializeComponent();
            _departmentService = departmentService;
        }

        // Yeni bir departman kaydı oluşturur.
        private void DepSaveBtn_Click(object sender, RoutedEventArgs e)
        {
           if (DepartmentName.Text!=null || DepartmentName.Text !=string.Empty)
            {
                Department dep = new Department();
                dep.Name = DepartmentName.Text;
                _departmentService.Add(dep);
                
                // Herşey başarılı ise..
                this.DialogResult = true;
            }
        }
    }
}
