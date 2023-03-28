using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using WPFApplicationArc.Models;

namespace WPFApplicationArc.Services.ServiceInterfaces
{
    public interface IPersonService : IGenericService<Person>
    {

        // Burada IGeneric Servis dışında kalan 
        // Metodları tanımlayabilir
        // ilgili Class içerisinde de bunları yapabiliriz.
        List<Person> GetPeople();



    }
}