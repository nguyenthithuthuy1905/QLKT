using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QLKT.Data;
using QLKT.Models;
using System.Threading.Tasks;

namespace QLKT.Controllers
{
    public class SettingsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public SettingsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Admin Settings (Xem thông tin người dùng)
        public async Task<IActionResult> Index(string id)
        {
            // Tìm kiếm sinh viên hoặc giảng viên theo mã sinh viên (MASV)
            var user = await _context.SinhViens.FirstOrDefaultAsync(u => u.MASV == id); // Tìm kiếm sinh viên theo MASV
            if (user == null)
            {
                return NotFound(); // Nếu không tìm thấy, trả về Not Found
            }

            return View(user); // Hiển thị thông tin người dùng
        }

        // POST: Cập nhật thông tin tài khoản
        [HttpPost]
        public async Task<IActionResult> UpdateProfile(SinhVien updatedSinhVien)
        {
            if (ModelState.IsValid)
            {
                var user = await _context.SinhViens.FindAsync(updatedSinhVien.MASV);  // Tìm sinh viên cần cập nhật
                if (user != null)
                {
                    user.DienThoai = updatedSinhVien.DienThoai;
                    user.Email = updatedSinhVien.Email;

                    _context.Update(user);  // Cập nhật thông tin
                    await _context.SaveChangesAsync();  // Lưu vào cơ sở dữ liệu
                    TempData["SuccessMessage"] = "Cập nhật thông tin thành công!";
                    return RedirectToAction("Index", new { id = user.MASV });  // Quay lại trang thông tin tài khoản
                }
                return NotFound();  // Nếu không tìm thấy, trả về Not Found
            }

            return View("Index", updatedSinhVien); // Trả về lại trang cũ nếu có lỗi
        }

    }
}
