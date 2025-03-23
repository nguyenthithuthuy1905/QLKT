using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QLKT.Areas.Admin.Models;
using QLKT.Data;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace QLKT.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = SD.Role_Admin)]
    public class DashboardController : Controller
    {
        private readonly ApplicationDbContext _context;

        public DashboardController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var totalStudents = await _context.SinhViens.CountAsync();
            var totalViolations = await _context.ViPhamQuyChes.CountAsync();
            var totalIncidents = await _context.SuCoBatThuongs.CountAsync();
            var totalCommitments = await _context.CamDoans.CountAsync();

            var violationStats = await _context.ViPhamQuyChes
                .GroupBy(v => v.MaLoaiViPham)
                .Select(g => new
                {
                    Type = g.Key.ToString(),
                    Count = g.Count()
                }).ToListAsync();

            var incidentStats = await _context.SuCoBatThuongs
                .GroupBy(s => s.MaLoaiSuCo)
                .Select(g => new
                {
                    Type = g.Key.ToString(),
                    Count = g.Count()
                }).ToListAsync();

            ViewBag.TotalStudents = totalStudents;
            ViewBag.TotalViolations = totalViolations;
            ViewBag.TotalIncidents = totalIncidents;
            ViewBag.TotalCommitments = totalCommitments;

            // Chuyển đổi sang JSON để tránh lỗi khi render trên View
            ViewBag.ViolationStats = JsonSerializer.Serialize(violationStats);
            ViewBag.IncidentStats = JsonSerializer.Serialize(incidentStats);

            return View(); // 👈 Đảm bảo return View() đúng
        }
    }
}
