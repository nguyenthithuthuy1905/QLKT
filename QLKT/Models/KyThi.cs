using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace QLKT.Models
{
    public class KyThi
    {
        [Key] 
        public int MaKyThi { get; set; }

        public string TenKyThi { get; set; }
        public DateTime NgayThi { get; set; }
        public TimeSpan GioThi { get; set; }
        public int ThoiGianLamBai { get; set; }

        public int MaMonHoc { get; set; }
        public int? MaPH { get; set; }

        [ForeignKey("MaMonHoc")]
        public MonHoc MonHoc { get; set; }

        [ForeignKey("MaPH")]
        public PhongThi PhongThi { get; set; }

        public ICollection<PhanCongCoiThi> PhanCongCoiThis { get; set; }
        public ICollection<SuCoBatThuong> SuCoBatThuongs { get; set; }
    }
}
