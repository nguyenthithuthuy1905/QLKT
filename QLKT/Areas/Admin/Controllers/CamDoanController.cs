﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QLKT.Models;
using System.Linq;
using System.Threading.Tasks;
using System;
using Microsoft.AspNetCore.Authorization;
using QLKT.Areas.Admin.Models;
using QLKT.Data;

namespace QLKT.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = SD.Role_Admin)]
    public class CamDoanController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CamDoanController(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            var dsSinhVienChuaCamDoan = await _context.CamDoans
                .Include(cd => cd.SinhVien)
                .Include(cd => cd.SinhVien.LichThiSinhViens)
                .ThenInclude(lt => lt.MonHoc)
                .Include(cd => cd.SinhVien.LichThiSinhViens)
                .ThenInclude(lt => lt.PhongThi)
                .ToListAsync();

            return View(dsSinhVienChuaCamDoan);
        }
        public async Task<IActionResult> ChuaBoSung()
        {
            var dsSinhVienChuaCamDoan = await _context.CamDoans
             .Where(cd => cd.NoiDungCamDoan == null || cd.NoiDungCamDoan == "")
             .Include(cd => cd.SinhVien)
             .Include(cd => cd.SinhVien.LichThiSinhViens)  // Bao gồm LịchThiSinhVien để lấy thông tin môn học
             .ThenInclude(lt => lt.MonHoc)  // Bao gồm môn học từ lịch thi
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

            //return RedirectToAction("ChuaBoSung");
            return RedirectToAction("Index");
        }
        [HttpPost]
        public async Task<IActionResult> ThemCamDoan(string maSinhVien)
        {
            if (string.IsNullOrEmpty(maSinhVien))
            {
                TempData["Error"] = "Mã sinh viên không được để trống";
                return RedirectToAction("ChuaBoSung");
            }

            // Kiểm tra xem cam đoan đã tồn tại chưa
            var camDoanExist = await _context.CamDoans
                .AnyAsync(cd => cd.MaSV == maSinhVien);

            if (camDoanExist)
            {
                TempData["Error"] = "Sinh viên này đã có cam đoan trong hệ thống";
                return RedirectToAction("ChuaBoSung");
            }

            var sinhVien = await _context.SinhViens.FindAsync(maSinhVien);
            if (sinhVien == null)
            {
                TempData["Error"] = "Không tìm thấy sinh viên";
                return RedirectToAction("ChuaBoSung");
            }

            var camDoan = new CamDoan
            {
                MaSV = sinhVien.MASV,
                NgayThi = DateTime.Now,
                NoiDungCamDoan = "Đã bổ sung cam đoan",
                MaLop = sinhVien.MaLop
            };

            _context.CamDoans.Add(camDoan);
            await _context.SaveChangesAsync();

            TempData["Success"] = "Thêm cam đoan thành công";
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