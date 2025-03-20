using System.ComponentModel.DataAnnotations;

namespace QLKT.Models
{
    public class Khoa
    {
        [Key]
        public int MaKhoa { get; set; }
        [Required]
        public string TenKhoa { get; set; }

        public ICollection<SinhVien> SinhViens { get; set; }
        public ICollection<GiangVien> GiangViens { get; set; }
    }
}
