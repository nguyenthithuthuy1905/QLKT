using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace QLKT.Models
{
    public class LichThiSinhVien
    {
        [Key]
        public int MaLichThi { get; set; }

        public string MaSV { get; set; }
        public int MaMonHoc { get; set; }
        public DateTime NgayThi { get; set; }
        public TimeSpan GioThi { get; set; }
        public int MaPH { get; set; }

        [ForeignKey("MaSV")]
        public SinhVien SinhVien { get; set; }
        [ForeignKey("MaMonHoc")]
        public MonHoc MonHoc { get; set; }
        [ForeignKey("MaPH")]
        public PhongThi PhongThi { get; set; }
    }
}
