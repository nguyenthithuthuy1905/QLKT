using System.ComponentModel.DataAnnotations;

namespace QLKT.Models
{
    public class PhongThi
    {
        [Key]
        public int MaPH { get; set; }
        [Required]
        public string TenPH { get; set; }
        public string ViTri { get; set; }
        public int SoLuongThiSinh { get; set; }

        public ICollection<KyThi> KyThis { get; set; }
    }
}
