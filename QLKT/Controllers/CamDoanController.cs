using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QLKT.Data;
using QLKT.Models;
using System.Linq;
using System.Threading.Tasks;
using System;

namespace QLKT.Controllers
{
    public class CamDoanController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CamDoanController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> ChuaBoSung()
        {
            var dsSinhVienChuaCamDoan = await _context.CamDoans
                .Where(cd => cd.NoiDungCamDoan == null || cd.NoiDungCamDoan == "")
                .Include(cd => cd.SinhVien)
                .ToListAsync();

            return View(dsSinhVienChuaCamDoan);
        }

        public async Task<IActionResult> ThemCamDoan(int maSinhVien)
        {
            var sinhVien = await _context.SinhViens.FindAsync(maSinhVien);
            if (sinhVien == null)
            {
                return NotFound();
            }

            var camDoan = new CamDoan
            {
                MaSV = sinhVien.MASV,
                NgayThi = System.DateTime.Now,
                NoiDungCamDoan = "Đã bổ sung cam đoan"
            };

            _context.CamDoans.Add(camDoan);
            await _context.SaveChangesAsync();

            return RedirectToAction("ChuaBoSung");
        }

        public async Task<IActionResult> XoaCamDoan(int maCamDoan)
        {
            var camDoan = await _context.CamDoans.FindAsync(maCamDoan);
            if (camDoan == null)
            {
                return NotFound();
            }

            _context.CamDoans.Remove(camDoan);
            await _context.SaveChangesAsync();

            return RedirectToAction("ChuaBoSung");
        }

        // Phương thức trả về thông tin sinh viên khi nhập MSSV
        public async Task<IActionResult> GetStudentInfo(string maSV)
        {
            var sinhVien = await _context.SinhViens
                                          .Where(sv => sv.MASV == maSV)
                                          .FirstOrDefaultAsync();

            if (sinhVien != null)
            {
                // Truy vấn thông tin từ bảng LichThiSinhViens để lấy thông tin về lịch thi của sinh viên
                var lichThi = await _context.LichThiSinhViens
                                             .Where(lt => lt.MaSV == maSV)
                                             .Include(lt => lt.MonHoc)
                                             .Include(lt => lt.PhongThi)
                                             .FirstOrDefaultAsync();

                // Trả về thông tin sinh viên kèm với lịch thi, môn học và phòng thi
                return Json(new
                {
                    HoTen = sinhVien.HoLot + " " + sinhVien.TenSV,
                    MaLop = sinhVien.MaLop,
                    NgayThi = lichThi?.NgayThi.ToString("yyyy-MM-dd"), // Đảm bảo NgayThi được định dạng đúng
                    PhongThi = lichThi?.PhongThi.TenPH ?? "Chưa xác định", // Lấy tên phòng thi
                    GioThi = lichThi?.GioThi.ToString(@"hh\:mm"), // Đảm bảo giờ thi có định dạng hợp lệ
                    HoLotSV = sinhVien.HoLot,
                    TenSV = sinhVien.TenSV,
                    TenMonHoc = lichThi?.MonHoc.TenMonHoc ?? "Chưa xác định" // Lấy tên môn học
                });
            }

            return Json(null);  // Nếu không tìm thấy sinh viên
        }

    }
}
