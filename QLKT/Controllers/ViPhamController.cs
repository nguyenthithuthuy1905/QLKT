using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QLKT.Data;
using QLKT.Models;
using System.Linq;
using System.Threading.Tasks;

namespace QLKT.Controllers
{
    public class ViPhamController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ViPhamController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var viPhamList = await _context.ViPhamQuyChes
                .Include(v => v.SinhVien)
                .Include(v => v.LoaiViPham)
                .ToListAsync();
            return View(viPhamList);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(ViPhamQuyChe viPham)
        {
            if (ModelState.IsValid)
            {
                _context.ViPhamQuyChes.Add(viPham);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(viPham);
        }
    }
}
