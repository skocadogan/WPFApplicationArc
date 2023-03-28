using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using WPFApplicationArc.DBContexts;
using WPFApplicationArc.Models;
using WPFApplicationArc.Services.ServiceInterfaces;

namespace WPFApplicationArc.Services
{
    // Sınıfın internal olması dikkati çekebilir.
    // interface üzerinden işlem yaptığımız için
    // Bu sınıfa direkt erişime ihtiyaç yok. 
    // O yüzden ayrıca public yapılmadı.
    internal class PersonService : GenericService<Person>, IPersonService
    {
        public PersonService(AppDBContext context) : base(context) { }
        
        // WPF'nin kullanması için..
        // Nesne içerisinde yapılan bir değişiklik anında 
        // bu nesne içerisine yansıyacak
        public ObservableCollection<Person> People { get; set; }


        // IPersonService içerisinde tanımlanan özelleştirilmiş bir metod
        // Bu metod miras aldığı Generic servis içerisinde yok. Buraya özel.
        public List<Person> GetPeople()
        {
            // db context'i alabilmek için base parametresini kullanıyoruz.
            People = new ObservableCollection<Person>(base.GetAll().ToList());
            return People.ToList();
        }

        
    }
}
