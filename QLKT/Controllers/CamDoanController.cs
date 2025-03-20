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

       public async Task<IActionResult> GetStudentInfo(string maSV)
{
    var sinhVien = await _context.SinhViens
        .Where(sv => sv.MASV == maSV)
        .FirstOrDefaultAsync();

    if (sinhVien == null)
    {
        return Json(null);
    }

    var lichThi = await _context.LichThiSinhViens
        .Where(lt => lt.MaSV == maSV)
        .Include(lt => lt.MonHoc)
        .Include(lt => lt.PhongThi)
        .FirstOrDefaultAsync();

    return Json(new
    {
        hoTen = sinhVien.HoLot + " " + sinhVien.TenSV,
        maLop = sinhVien.MaLop ?? "Không có lớp",
        ngayThi = lichThi?.NgayThi.ToString("yyyy-MM-dd") ?? "Không có lịch thi",
        gioThi = lichThi?.GioThi.ToString(@"hh\:mm") ?? "Không có giờ thi",
        phongThi = lichThi?.PhongThi.TenPH ?? "Không có phòng thi",
        tenMonHoc = lichThi?.MonHoc.TenMonHoc ?? "Không có môn học"
    });
}

    }
}
