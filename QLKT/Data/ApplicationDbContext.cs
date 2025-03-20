using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using QLKT.Models;


namespace QLKT.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options) { }

        public DbSet<CamDoan> CamDoans { get; set; }
        public DbSet<GiangVien> GiangViens { get; set; }
        public DbSet<Khoa> Khoas { get; set; }
        public DbSet<KyThi> KyThis { get; set; }
        public DbSet<LichThiSinhVien> LichThiSinhViens { get; set; }
        public DbSet<LoaiViPham> LoaiViPhams { get; set; }
        public DbSet<MonHoc> MonHocs { get; set; }
        public DbSet<NoiDungCamDoan> NoiDungCamDoans { get; set; }
        public DbSet<PhanCongCoiThi> PhanCongCoiThis { get; set; }
        public DbSet<PhongThi> PhongThis { get; set; }
        public DbSet<SinhVien> SinhViens { get; set; }
        public DbSet<ViPhamQuyChe> ViPhamQuyChes { get; set; }
        public DbSet<SuCoBatThuong> SuCoBatThuongs { get; set; }
        public DbSet<LoaiSuCo> LoaiSuCos { get; set; }

    }
}
