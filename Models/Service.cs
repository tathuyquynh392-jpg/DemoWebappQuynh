using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DemoWebapp.Models
{
    public class Service
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string TenDichVu { get; set; }
        [Column(TypeName ="decimal(18,2)")]
        public decimal Gia { get; set; }
    }
}
