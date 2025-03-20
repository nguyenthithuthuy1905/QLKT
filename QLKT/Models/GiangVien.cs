using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace QLKT.Models
{
    public class GiangVien
    {
        [Key]
        public int MaGV { get; set; }
        [Required]
        public string HoTen { get; set; }
        public string Email { get; set; }
        public string DienThoai { get; set; }

        public int? MaKhoa { get; set; }
        [ForeignKey("MaKhoa")]
        public Khoa Khoa { get; set; }

        public ICollection<PhanCongCoiThi> PhanCongCoiThis { get; set; }
    }
}
