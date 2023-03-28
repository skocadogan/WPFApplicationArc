using Microsoft.EntityFrameworkCore;
using WPFApplicationArc.Models;

namespace WPFApplicationArc.DBContexts
{
    public class AppDBContext: DbContext
    {

        public AppDBContext(DbContextOptions options) : base(options){}

        public DbSet<Person> People { get; set; }
        public DbSet<Department> Departments { get; set; }
    }
}
