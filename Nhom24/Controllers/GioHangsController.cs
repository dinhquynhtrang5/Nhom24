using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Nhom24.Data;
using Nhom24.Models;

namespace Nhom24.Controllers
{
    public class GioHangsController : Controller
    {
        private readonly Nhom24Context _context;

        public GioHangsController(Nhom24Context context)
        {
            _context = context;
        }

        // GET: GioHangs
        public async Task<IActionResult> Index()
        {
            var nhom24Context = _context.GioHang.Include(g => g.NguoiDung).Include(g => g.SanPham);
            return View(await nhom24Context.ToListAsync());
        }

        // GET: GioHangs/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null || _context.GioHang == null)
            {
                return NotFound();
            }

            var gioHang = await _context.GioHang
                .Include(g => g.NguoiDung)
                .Include(g => g.SanPham)
                .FirstOrDefaultAsync(m => m.NguoiDungID == id);
            if (gioHang == null)
            {
                return NotFound();
            }

            return View(gioHang);
        }

        // GET: GioHangs/Create
        public IActionResult Create()
        {
            ViewData["NguoiDungID"] = new SelectList(_context.NguoiDung, "NguoiDungID", "NguoiDungID");
            ViewData["SanPhamID"] = new SelectList(_context.SanPham, "SanPhamID", "SanPhamID");
            return View();
        }

        // POST: GioHangs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("NguoiDungID,SanPhamID,SoLuongSanPham")] GioHang gioHang)
        {
            if (ModelState.IsValid)
            {
                _context.Add(gioHang);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["NguoiDungID"] = new SelectList(_context.NguoiDung, "NguoiDungID", "NguoiDungID", gioHang.NguoiDungID);
            ViewData["SanPhamID"] = new SelectList(_context.SanPham, "SanPhamID", "SanPhamID", gioHang.SanPhamID);
            return View(gioHang);
        }

        // GET: GioHangs/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null || _context.GioHang == null)
            {
                return NotFound();
            }

            var gioHang = await _context.GioHang.FindAsync(id);
            if (gioHang == null)
            {
                return NotFound();
            }
            ViewData["NguoiDungID"] = new SelectList(_context.NguoiDung, "NguoiDungID", "NguoiDungID", gioHang.NguoiDungID);
            ViewData["SanPhamID"] = new SelectList(_context.SanPham, "SanPhamID", "SanPhamID", gioHang.SanPhamID);
            return View(gioHang);
        }

        // POST: GioHangs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("NguoiDungID,SanPhamID,SoLuongSanPham")] GioHang gioHang)
        {
            if (id != gioHang.NguoiDungID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(gioHang);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!GioHangExists(gioHang.NguoiDungID))
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
            ViewData["NguoiDungID"] = new SelectList(_context.NguoiDung, "NguoiDungID", "NguoiDungID", gioHang.NguoiDungID);
            ViewData["SanPhamID"] = new SelectList(_context.SanPham, "SanPhamID", "SanPhamID", gioHang.SanPhamID);
            return View(gioHang);
        }

        // GET: GioHangs/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null || _context.GioHang == null)
            {
                return NotFound();
            }

            var gioHang = await _context.GioHang
                .Include(g => g.NguoiDung)
                .Include(g => g.SanPham)
                .FirstOrDefaultAsync(m => m.NguoiDungID == id);
            if (gioHang == null)
            {
                return NotFound();
            }

            return View(gioHang);
        }

        // POST: GioHangs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            if (_context.GioHang == null)
            {
                return Problem("Entity set 'Nhom24Context.GioHang'  is null.");
            }
            var gioHang = await _context.GioHang.FindAsync(id);
            if (gioHang != null)
            {
                _context.GioHang.Remove(gioHang);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool GioHangExists(string id)
        {
          return _context.GioHang.Any(e => e.NguoiDungID == id);
        }
    }
}
