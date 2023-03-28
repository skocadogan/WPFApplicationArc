using WPFApplicationArc.DBContexts;
using WPFApplicationArc.Models;
using WPFApplicationArc.Services.ServiceInterfaces;

namespace WPFApplicationArc.Services
{
    internal class DepartmentService : GenericService<Department>, IDepartmentService
    {
        public DepartmentService(AppDBContext context) : base(context)
        {
        }
        // Generic Service içerisindeki metodlar şu anda bize yetiyor
        // O yüzden başka bir metod tanımlamadık.


    }
}
