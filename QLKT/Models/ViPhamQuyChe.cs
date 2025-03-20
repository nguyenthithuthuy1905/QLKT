using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace QLKT.Models
{
    public class ViPhamQuyChe
    {
        [Key]
        public int MaViPham { get; set; }

        [Required]
        public string MaSV { get; set; }
        public DateTime NgayThi { get; set; }
        public TimeSpan GioThi { get; set; }
        public int? MaPH { get; set; }
        public string MaLop { get; set; }
        public int MaLoaiViPham { get; set; }
        public string NoiDungViPham { get; set; }

        [ForeignKey("MaSV")]
        public SinhVien SinhVien { get; set; }

        [ForeignKey("MaPH")]
        public PhongThi PhongThi { get; set; }

        [ForeignKey("MaLoaiViPham")]
        public LoaiViPham LoaiViPham { get; set; }
    }
}
