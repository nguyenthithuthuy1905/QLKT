using System.ComponentModel.DataAnnotations;

namespace QLKT.Models
{
    public class LoaiViPham
    {
        [Key]
        public int MaViPham { get; set; }

        [Required]
        public string LoaiViPhamThi { get; set; }

        public ICollection<ViPhamQuyChe> ViPhamQuyChes { get; set; }
    }
}
