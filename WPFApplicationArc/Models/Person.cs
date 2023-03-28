namespace WPFApplicationArc.Models
{

    /// <summary>
    /// Örnek Person Sınıfı
    /// </summary>
    public class Person : BaseModel
    {
        // Gerekli olan diğer kısımlar BaseModel sınıfından geliyor.
        // Burada BaseModel de tanımlanmayanlar var.

        public string? LastName { get; set; }
        public string? Email { get; set; }
        public string? Address { get; set; }

        // Bir kişi bir departmana tanımlıdır.
        public int? DepartmentId { get; set; }
        public virtual Department? Department { get; set; }
    }
}
