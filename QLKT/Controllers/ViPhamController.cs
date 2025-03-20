using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QLKT.Data;
using QLKT.Models;
using System.Linq;
using System.Threading.Tasks;

namespace QLKT.Controllers
{
    public class ViPhamController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ViPhamController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var viPhamList = await _context.ViPhamQuyChes
                .Include(v => v.SinhVien)
                .Include(v => v.LoaiViPham)
                .ToListAsync();
            return View(viPhamList);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(ViPhamQuyChe viPham)
        {
            if (ModelState.IsValid)
            {
                _context.ViPhamQuyChes.Add(viPham);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(viPham);
        }

        [HttpGet]
        public async Task<IActionResult> GetStudentInfo(string maSV)
        {
            var sinhVien = await _context.SinhViens
                .Where(sv => sv.MASV == maSV)
                .FirstOrDefaultAsync();

            if (sinhVien == null)
            {
                return Json(new { error = "Không tìm thấy sinh viên" });
            }

            var lichThi = await _context.LichThiSinhViens
                .Where(lt => lt.MaSV == maSV)
                .Include(lt => lt.MonHoc)
                .FirstOrDefaultAsync();

            return Json(new
            {
                hoTen = $"{sinhVien.HoLot} {sinhVien.TenSV}",
                maLop = sinhVien.MaLop ?? "Không có lớp",
                ngayThi = lichThi?.NgayThi.ToString("yyyy-MM-dd") ?? null,
                tenMonHoc = lichThi?.MonHoc.TenMonHoc ?? "Không có môn học"
            });
        }
    }
}
