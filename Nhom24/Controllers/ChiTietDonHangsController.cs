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
    public class ChiTietDonHangsController : Controller
    {
        private readonly Nhom24Context _context;

        public ChiTietDonHangsController(Nhom24Context context)
        {
            _context = context;
        }

        // GET: ChiTietDonHangs
        public async Task<IActionResult> Index()
        {
            var nhom24Context = _context.ChiTietDonHang.Include(c => c.DonHang).Include(c => c.SanPham);
            return View(await nhom24Context.ToListAsync());
        }

        // GET: ChiTietDonHangs/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null || _context.ChiTietDonHang == null)
            {
                return NotFound();
            }

            var chiTietDonHang = await _context.ChiTietDonHang
                .Include(c => c.DonHang)
                .Include(c => c.SanPham)
                .FirstOrDefaultAsync(m => m.DonHangID == id);
            if (chiTietDonHang == null)
            {
                return NotFound();
            }

            return View(chiTietDonHang);
        }

        // GET: ChiTietDonHangs/Create
        public IActionResult Create()
        {
            ViewData["DonHangID"] = new SelectList(_context.DonHang, "DonHangID", "DonHangID");
            ViewData["SanPhamID"] = new SelectList(_context.SanPham, "SanPhamID", "SanPhamID");
            return View();
        }

        // POST: ChiTietDonHangs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("DonHangID,SanPhamID,SoLuongSanPham")] ChiTietDonHang chiTietDonHang)
        {
            if (ModelState.IsValid)
            {
                _context.Add(chiTietDonHang);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["DonHangID"] = new SelectList(_context.DonHang, "DonHangID", "DonHangID", chiTietDonHang.DonHangID);
            ViewData["SanPhamID"] = new SelectList(_context.SanPham, "SanPhamID", "SanPhamID", chiTietDonHang.SanPhamID);
            return View(chiTietDonHang);
        }

        // GET: ChiTietDonHangs/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null || _context.ChiTietDonHang == null)
            {
                return NotFound();
            }

            var chiTietDonHang = await _context.ChiTietDonHang.FindAsync(id);
            if (chiTietDonHang == null)
            {
                return NotFound();
            }
            ViewData["DonHangID"] = new SelectList(_context.DonHang, "DonHangID", "DonHangID", chiTietDonHang.DonHangID);
            ViewData["SanPhamID"] = new SelectList(_context.SanPham, "SanPhamID", "SanPhamID", chiTietDonHang.SanPhamID);
            return View(chiTietDonHang);
        }

        // POST: ChiTietDonHangs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("DonHangID,SanPhamID,SoLuongSanPham")] ChiTietDonHang chiTietDonHang)
        {
            if (id != chiTietDonHang.DonHangID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(chiTietDonHang);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ChiTietDonHangExists(chiTietDonHang.DonHangID))
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
            ViewData["DonHangID"] = new SelectList(_context.DonHang, "DonHangID", "DonHangID", chiTietDonHang.DonHangID);
            ViewData["SanPhamID"] = new SelectList(_context.SanPham, "SanPhamID", "SanPhamID", chiTietDonHang.SanPhamID);
            return View(chiTietDonHang);
        }

        // GET: ChiTietDonHangs/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null || _context.ChiTietDonHang == null)
            {
                return NotFound();
            }

            var chiTietDonHang = await _context.ChiTietDonHang
                .Include(c => c.DonHang)
                .Include(c => c.SanPham)
                .FirstOrDefaultAsync(m => m.DonHangID == id);
            if (chiTietDonHang == null)
            {
                return NotFound();
            }

            return View(chiTietDonHang);
        }

        // POST: ChiTietDonHangs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            if (_context.ChiTietDonHang == null)
            {
                return Problem("Entity set 'Nhom24Context.ChiTietDonHang'  is null.");
            }
            var chiTietDonHang = await _context.ChiTietDonHang.FindAsync(id);
            if (chiTietDonHang != null)
            {
                _context.ChiTietDonHang.Remove(chiTietDonHang);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ChiTietDonHangExists(string id)
        {
          return _context.ChiTietDonHang.Any(e => e.DonHangID == id);
        }
    }
}
