using System.ComponentModel.DataAnnotations;

namespace QLKT.Models
{
    public class LoaiSuCo
    {
        [Key]
        public int MaLoaiSuCo { get; set; }

        [Required]
        public string TenLoaiSuCo { get; set; }

        public ICollection<SuCoBatThuong> SuCoBatThuongs { get; set; }
    }
}
