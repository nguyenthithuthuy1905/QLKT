using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QLKT.Models;
using CsvHelper;
using OfficeOpenXml;  // For reading Excel
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using QLKT.Areas.Admin.Models;
using QLKT.Data;

namespace QLKT.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = SD.Role_Admin)]
    public class ImportController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ImportController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Import
        public IActionResult Index()
        {
            return View();
        }

        // POST: Import CSV
        [HttpPost]
        public async Task<IActionResult> ImportCSV(IFormFile file)
        {
            if (file == null || file.Length == 0)
                return Content("No file selected");

            using (var reader = new StreamReader(file.OpenReadStream()))
            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                var records = csv.GetRecords<SinhVien>().ToList();
                foreach (var record in records)
                {
                    _context.SinhViens.Add(record);
                }
                await _context.SaveChangesAsync();
            }

            return RedirectToAction("Index");
        }

        // POST: Import Excel
        [HttpPost]
        public async Task<IActionResult> ImportExcel(IFormFile file)
        {
            if (file == null || file.Length == 0)
                return Content("No file selected");

            using (var package = new ExcelPackage(file.OpenReadStream()))
            {
                var worksheet = package.Workbook.Worksheets[0]; // Lấy sheet đầu tiên

                var rowCount = worksheet.Dimension.Rows;
                for (int row = 2; row <= rowCount; row++) // Bắt đầu từ dòng thứ 2 nếu dòng đầu là header
                {
                    var sinhVien = new SinhVien
                    {
                        MASV = worksheet.Cells[row, 1].Text,
                        HoLot = worksheet.Cells[row, 2].Text,
                        TenSV = worksheet.Cells[row, 3].Text,
                        MaLop = worksheet.Cells[row, 4].Text,
                        NgaySinh = DateTime.Parse(worksheet.Cells[row, 5].Text)
                    };

                    _context.SinhViens.Add(sinhVien);
                }

                await _context.SaveChangesAsync();
            }

            return RedirectToAction("Index");
        }
    }
}
