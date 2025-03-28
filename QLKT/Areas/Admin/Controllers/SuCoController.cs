using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QLKT.Areas.Admin.Models;
using QLKT.Data;
using QLKT.Models;
using System.Linq;
using System.Threading.Tasks;

namespace QLKT.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = SD.Role_Admin)]
    public class SuCoController : Controller
    {
        private readonly ApplicationDbContext _context;

        public SuCoController(ApplicationDbContext context)
        {
            _context = context;
        }

        // Hiển thị danh sách sự cố bất thường
        public async Task<IActionResult> Index(string search)
        {
            var query = _context.SuCoBatThuongs.Include(s => s.LoaiSuCo).AsQueryable();

            // Tìm kiếm theo mã sự cố hoặc loại sự cố
            if (!string.IsNullOrEmpty(search) && int.TryParse(search, out int maSuCo))
            {
                query = query.Where(s => s.MaSuCo == maSuCo);
            }
            else if (!string.IsNullOrEmpty(search))
            {
                query = query.Where(s => s.LoaiSuCo.TenLoaiSuCo.Contains(search) || s.MaSuCo.ToString().Contains(search));
            }

            var suCoList = await query.ToListAsync();

            // Thông báo nếu không tìm thấy sự cố
            if (suCoList.Count == 0 && !string.IsNullOrEmpty(search))
            {
                ViewBag.Message = "Không tìm thấy sự cố có mã: " + search;
            }

            return View(suCoList);
        }

        // Thêm sự cố
        public IActionResult Create()
        {
            // Populate ViewBag with data for dropdown or other fields
            ViewBag.LoaiSuCoList = _context.LoaiSuCos.ToList();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(SuCoBatThuong suCo)
        {
            if (ModelState.IsValid)
            {
                // Check if the MaKyThi exists, if not, show an error
                var kyThi = await _context.KyThis.FirstOrDefaultAsync(k => k.MaKyThi == suCo.MaKyThi);
                if (kyThi == null)
                {
                    TempData["Error"] = "Mã kỳ thi không hợp lệ!";
                    return View(suCo);
                }

                // Get other related data based on MaKyThi
                var loaiSuCo = await _context.LoaiSuCos.FindAsync(suCo.MaLoaiSuCo);
                var phongThi = await _context.PhongThis.FindAsync(suCo.MaPH);

                // Set default values or update the model
                suCo.LoaiSuCo = loaiSuCo;
                suCo.PhongThi = phongThi;

                _context.SuCoBatThuongs.Add(suCo);
                await _context.SaveChangesAsync();
                TempData["Success"] = "Thêm sự cố thành công!";
                return RedirectToAction(nameof(Index));
            }
            // If there are validation errors, reload the ViewBag
            ViewBag.LoaiSuCoList = _context.LoaiSuCos.ToList();
            return View(suCo);
        }
        // Sửa sự cố
        public async Task<IActionResult> Edit(int id)
        {
            var suCo = await _context.SuCoBatThuongs.Include(s => s.LoaiSuCo).FirstOrDefaultAsync(s => s.MaSuCo == id);
            if (suCo == null)
            {
                return NotFound();
            }

            ViewBag.LoaiSuCoList = _context.LoaiSuCos.ToList();
            return View(suCo);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, SuCoBatThuong suCo)
        {
            if (id != suCo.MaSuCo)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(suCo);
                    await _context.SaveChangesAsync();
                    TempData["Success"] = "Sửa sự cố thành công!";
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_context.SuCoBatThuongs.Any(e => e.MaSuCo == suCo.MaSuCo))
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

            ViewBag.LoaiSuCoList = _context.LoaiSuCos.ToList();
            TempData["Error"] = "Dữ liệu không hợp lệ!";
            return View(suCo);
        }

        // Xóa sự cố
        public async Task<IActionResult> Delete(int id)
        {
            var suCo = await _context.SuCoBatThuongs.FindAsync(id);
            if (suCo == null)
            {
                return NotFound();
            }

            _context.SuCoBatThuongs.Remove(suCo);
            await _context.SaveChangesAsync();
            TempData["Success"] = "Xóa sự cố thành công!";
            return RedirectToAction(nameof(Index));
        }
        [HttpGet]
        public async Task<IActionResult> GetStudentInfo(int maSV)
        {
            // Fetch the exam by MaKyThi (exam ID)
            var kyThi = await _context.KyThis
                .Where(k => k.MaKyThi == maSV) // Direct comparison since both are integers
                .FirstOrDefaultAsync();

            if (kyThi == null)
            {
                return Json(null); // Return null if no matching exam is found
            }

            // Fetch the associated room for the exam
            var phongThi = await _context.PhongThis
                .Where(p => p.MaPH == kyThi.MaPH)
                .FirstOrDefaultAsync();

            return Json(new
            {
                tenPhong = phongThi?.TenPH, // Room name
                tenMonHoc = kyThi.MonHoc?.TenMonHoc, // Subject name
                gioThi = kyThi.GioThi.ToString(@"hh\:mm") // Exam time
            });
        }



    }
}
