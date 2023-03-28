using System.Collections.Generic;

namespace WPFApplicationArc.Models
{
    // base modelin içerisinde ID ve Name var.
    // Bu sınıfta da başka bir şey gerekmedi.
    public class Department:BaseModel
    {
        
        
        // Departmanın içerisinde birden fazla 
        // kişi olabilir.
        public virtual List<Person>? Persons { get; set; }

    }
}
