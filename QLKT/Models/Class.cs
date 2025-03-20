using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace QLKT.Models
{
    public class Class
    {
        [Key]
        public int MaPhanCong { get; set; }

        public int MaGV { get; set; }
        public int MaKyThi { get; set; }
        public string VaiTro { get; set; }

        [ForeignKey("MaGV")]
        public GiangVien GiangVien { get; set; }
        [ForeignKey("MaKyThi")]
        public KyThi KyThi { get; set; }
    }
}
