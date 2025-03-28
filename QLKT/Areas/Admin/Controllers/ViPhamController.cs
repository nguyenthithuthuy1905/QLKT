// ViPhamController.cs
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QLKT.Areas.Admin.Models;
using QLKT.Data;
using QLKT.Models;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace QLKT.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = SD.Role_Admin)]
    public class ViPhamController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ViPhamController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var violations = await _context.ViPhamQuyChes
                .Include(v => v.SinhVien)
                .Include(v => v.LoaiViPham)
                .ToListAsync();
            return View(violations);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpGet]
        public IActionResult GetStudentInfo(string maSV)
        {
            if (string.IsNullOrEmpty(maSV))
                return BadRequest(new { error = "Mã sinh viên không hợp lệ." });

            var student = _context.SinhViens
                .Include(sv => sv.LichThiSinhViens)
                    .ThenInclude(lt => lt.MonHoc)
                .Include(sv => sv.LichThiSinhViens)
                    .ThenInclude(lt => lt.PhongThi)
                .FirstOrDefault(sv => sv.MASV == maSV);

            if (student == null)
                return Json(new { error = "Không tìm thấy sinh viên." });

            var exam = student.LichThiSinhViens.FirstOrDefault();
            if (exam == null)
                return Json(new { error = "Sinh viên không có lịch thi." });

            return Json(new
            {
                hoTen = student.HoLot + " " + student.TenSV,
                maLop = student.MaLop,
                tenMonHoc = exam.MonHoc?.TenMonHoc,
                ngayThi = exam.NgayThi.ToString("yyyy-MM-dd"),
                gioThi = exam.GioThi.ToString(@"hh\:mm"),
                phongThi = exam.PhongThi?.TenPH,
                maPhong = exam.MaPH
            });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ViPhamQuyChe viPhamQuyChe)
        {
            Console.WriteLine("==> Dữ liệu nhận được:");
            Console.WriteLine(Newtonsoft.Json.JsonConvert.SerializeObject(viPhamQuyChe));

            if (!ModelState.IsValid)
            {
                foreach (var error in ModelState)
                {
                    Console.WriteLine($"{error.Key} => {string.Join(", ", error.Value.Errors.Select(e => e.ErrorMessage))}");
                }
                TempData["Error"] = "Dữ liệu nhập chưa hợp lệ!";
                return View(viPhamQuyChe);
            }

            try
            {
                _context.ViPhamQuyChes.Add(viPhamQuyChe);
                await _context.SaveChangesAsync();

                TempData["Success"] = "Thêm vi phạm thành công!";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi khi lưu vào DB: " + ex.Message);
                TempData["Error"] = "Lỗi khi lưu dữ liệu: " + ex.Message;
                return View(viPhamQuyChe);
            }
        }
    }
}
