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

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ViPhamQuyChe viPham)
        {
            if (!ModelState.IsValid)
            {
                TempData["Error"] = "Dữ liệu nhập chưa hợp lệ!";
                return View(viPham);
            }

            try
            {
                _context.ViPhamQuyChes.Add(viPham);
                await _context.SaveChangesAsync();

                TempData["Success"] = "Thêm vi phạm thành công!";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi lưu DB: " + ex.Message);
                TempData["Error"] = "Đã xảy ra lỗi khi lưu dữ liệu!";
                return View(viPham);
            }
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
                .Include(lt => lt.PhongThi)
                .FirstOrDefaultAsync();

            return Json(new
            {
                hoTen = $"{sinhVien.HoLot} {sinhVien.TenSV}",
                maLop = sinhVien.MaLop,
                ngayThi = lichThi?.NgayThi.ToString("yyyy-MM-dd"),
                gioThi = lichThi?.GioThi.ToString(@"hh\:mm"),
                tenMonHoc = lichThi?.MonHoc.TenMonHoc,
                tenPhong = lichThi?.PhongThi?.TenPH,
                maPhong = lichThi?.MaPH
            });
        }
    }
}