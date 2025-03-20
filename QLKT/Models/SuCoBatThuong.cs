using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace QLKT.Models
{
    public class SuCoBatThuong
    {
        [Key]
        public int MaSuCo { get; set; }

        public int MaKyThi { get; set; }
        public int MaLoaiSuCo { get; set; }
        public DateTime NgayThi { get; set; }
        public TimeSpan GioThi { get; set; }
        public int? MaPH { get; set; }
        public string NoiDungSuCo { get; set; }
        public string HuongXuLy { get; set; }
        public string NguoiLapBB1 { get; set; }
        public string NguoiLapBB2 { get; set; }
        public string NguoiLapBB3 { get; set; }

        [ForeignKey("MaKyThi")]
        public KyThi KyThi { get; set; }

        [ForeignKey("MaLoaiSuCo")]
        public LoaiSuCo LoaiSuCo { get; set; }

        [ForeignKey("MaPH")]
        public PhongThi PhongThi { get; set; }
    }
}
