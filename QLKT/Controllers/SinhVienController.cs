using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QLKT.Data;
using QLKT.Models;
using System.Linq;
using System.Threading.Tasks;

namespace QLKT.Controllers
{
    public class SinhVienController : Controller
    {
        private readonly ApplicationDbContext _context;

        public SinhVienController(ApplicationDbContext context)
        {
            _context = context;
        }

        // Tìm kiếm sinh viên theo MSSV
        public async Task<IActionResult> Index(string search)
        {
            var query = _context.SinhViens.AsQueryable();

            // Tìm kiếm sinh viên theo mã số sinh viên (MSSV)
            if (!string.IsNullOrEmpty(search))
            {
                query = query.Where(s => s.MASV.Contains(search)); // Tìm kiếm theo MSSV
            }

            var sinhVienList = await query.ToListAsync();

            if (sinhVienList.Count == 0 && !string.IsNullOrEmpty(search))
            {
                ViewBag.Message = "Không tìm thấy sinh viên có mã số: " + search;
            }

            return View(sinhVienList);
        }

        // Thêm sinh viên
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(SinhVien sinhVien)
        {
            if (ModelState.IsValid)
            {
                _context.SinhViens.Add(sinhVien);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(sinhVien);
        }

        // Sửa sinh viên
        public async Task<IActionResult> Edit(string id)
        {
            var sinhVien = await _context.SinhViens.FindAsync(id);
            if (sinhVien == null)
            {
                return NotFound();
            }

            return View(sinhVien);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(string id, SinhVien sinhVien)
        {
            if (id != sinhVien.MASV)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(sinhVien);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_context.SinhViens.Any(e => e.MASV == sinhVien.MASV))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }

                return RedirectToAction(nameof(Index));
            }

            return View(sinhVien);
        }

        // Xóa sinh viên
        public async Task<IActionResult> Delete(string id)
        {
            var sinhVien = await _context.SinhViens.FindAsync(id);
            if (sinhVien == null)
            {
                return NotFound();
            }

            _context.SinhViens.Remove(sinhVien);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
