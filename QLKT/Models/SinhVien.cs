using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace QLKT.Models
{
    public class SinhVien
    {
        [Key]
        public string MASV { get; set; }
        public string HoLot { get; set; }
        public string TenSV { get; set; }
        public string MaLop { get; set; }
        public string NienKhoa { get; set; }
        public string GioiTinh { get; set; }
        public DateTime NgaySinh { get; set; }
        public string NoiSinh { get; set; }
        public string SoCMND { get; set; }
        public string HoTenCha { get; set; }
        public string HoTenMe { get; set; }
        public string DienThoai { get; set; }
        public string DienThoaiBaoTinGap { get; set; }
        public int? MaKhoa { get; set; }
        public string ChuyenNganh { get; set; }
        public string Email { get; set; }

        [ForeignKey("MaKhoa")]
        public Khoa Khoa { get; set; }

        public ICollection<LichThiSinhVien> LichThiSinhViens { get; set; }
        public ICollection<CamDoan> CamDoans { get; set; }
        public ICollection<ViPhamQuyChe> ViPhamQuyChes { get; set; }
    }
}
