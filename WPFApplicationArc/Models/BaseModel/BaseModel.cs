
using System;
using System.ComponentModel.DataAnnotations;

namespace WPFApplicationArc.Models
{

    /// <summary>
    /// Yaratılacak Tüm DB nesnelerinin ortak özellikleri için
    /// Temel Sınıf Tanımı
    /// </summary>
    public class BaseModel
    {

        [Key]
        public int Id { get; set; }
        public string? Name { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime? UpdatedAt { get; set; }
    }
}
