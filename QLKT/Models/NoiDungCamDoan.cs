using System.ComponentModel.DataAnnotations;

namespace QLKT.Models
{
    public class NoiDungCamDoan
    {
        [Key]
        public int MaNoiDung { get; set; }

        [Required]
        public string NoiDung { get; set; }

        public ICollection<CamDoan> CamDoans { get; set; }
    }
}
