using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DemoWebapp.Models
{
    public class Tenant
    {
        [Key]
        
            public int TenantId { get; set; }
        [Required]
            public string TenNguoi { get; set; }
        [StringLength(10)]
            public string SDT { get; set; }
        
        [Required]
            public int RoomId { get; set; }
[ForeignKey("RoomId")]
            public Room? Room { get; set; }
        
    }
}
