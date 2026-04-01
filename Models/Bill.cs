using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DemoWebapp.Models
{
    public class Bill
    {
        [Key]
        public int BillId { get; set; }
        [ForeignKey("Room")]
        public int RoomId { get; set; }
        public int Dien { get; set; }
        public int Nuoc { get; set; }
        [Column(TypeName ="decimal(18,2)")]
        public decimal TongTien { get; set; }
        [ForeignKey("RoomId")]
        public Room? Room { get; set; }
    }
}
