using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DemoWebapp.Models
{
    public class Room
    {
        [Key]
        public int RoomId { get; set; }
        [Required]
        public string TenPhong { get; set; }
        [Column(TypeName ="decimal(18,2)")]
        public decimal Gia { get; set; }
        public string TrangThai { get; set; }
    }
}
