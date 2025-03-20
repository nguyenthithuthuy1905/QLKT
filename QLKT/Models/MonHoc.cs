using System.ComponentModel.DataAnnotations;

namespace QLKT.Models
{
    public class MonHoc
    {
        [Key]
        public int MaMonHoc { get; set; }
        [Required]
        public string TenMonHoc { get; set; }

        public ICollection<KyThi> KyThis { get; set; }
    }
}
