using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QLKT.Data;
using QLKT.Models;
using System.Linq;
using System.Threading.Tasks;

namespace QLKT.Controllers
{
    public class SuCoController : Controller
    {
        private readonly ApplicationDbContext _context;

        public SuCoController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index(string search)
        {
            // Khởi tạo query với việc Include để truy vấn dữ liệu liên kết với LoaiSuCo
            var query = _context.SuCoBatThuongs.Include(s => s.LoaiSuCo).AsQueryable();

            // Nếu tìm kiếm theo mã sự cố
            if (!string.IsNullOrEmpty(search) && int.TryParse(search, out int maSuCo))
            {
                query = query.Where(s => s.MaSuCo == maSuCo);
            }
            // Nếu tìm kiếm theo tên loại sự cố hoặc mã sự cố (dưới dạng chuỗi)
            else if (!string.IsNullOrEmpty(search))
            {
                query = query.Where(s => s.LoaiSuCo.TenLoaiSuCo.Contains(search) || s.MaSuCo.ToString().Contains(search));
            }

            // Lấy danh sách sự cố bất thường theo query đã lọc
            var suCoList = await query.ToListAsync();

            // Thông báo nếu không tìm thấy kết quả
            if (suCoList.Count == 0 && !string.IsNullOrEmpty(search))
            {
                ViewBag.Message = "Không tìm thấy sự cố có mã: " + search;
            }

            return View(suCoList);
        }



        // 🟢 ✅ Thêm sự cố
        public IActionResult Create()
        {
            // Thêm dữ liệu loại sự cố vào ViewBag để chọn loại sự cố
            ViewBag.LoaiSuCoList = _context.LoaiSuCos.ToList();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(SuCoBatThuong suCo)
        {
            if (ModelState.IsValid)
            {
                _context.SuCoBatThuongs.Add(suCo);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            // Nếu không hợp lệ, lại thêm dữ liệu loại sự cố vào ViewBag và quay lại form
            ViewBag.LoaiSuCoList = _context.LoaiSuCos.ToList();
            return View(suCo);
        }

        // 🟢 ✅ Xóa sự cố
        public async Task<IActionResult> XoaSuCo(int id)
        {
            var suCo = await _context.SuCoBatThuongs.FindAsync(id);
            if (suCo == null)
            {
                return NotFound();
            }

            _context.SuCoBatThuongs.Remove(suCo);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
