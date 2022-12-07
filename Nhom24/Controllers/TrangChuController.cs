using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Nhom24.Data;
using Nhom24.Models;


namespace Nhom24.Controllers
{
    public class TrangChuController : Controller
    {
        private readonly Nhom24Context _context;
        public TrangChuController(Nhom24Context context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            var nhom24Context = _context.SanPham.Include(s => s.NganhHang).ToList();
            ViewBag.Message = nhom24Context;
            return View();
        }
        // GET: SanPhams/Details/5
        public async Task<IActionResult> Details(string id)
        {
            var sanPham = await _context.SanPham
                .Include(s => s.NganhHang)
                .FirstOrDefaultAsync(m => m.SanPhamID == id);
            if (sanPham == null)
            {
                return NotFound();
            }
            ViewBag.Message = sanPham;
            return View();
        }
        public async Task<IActionResult> Makeup()
        {
            var nhom24Context = _context.SanPham.Include(s => s.NganhHang).ToList();
            ViewBag.Message = nhom24Context;
            return View();
        }
    }
    

}
