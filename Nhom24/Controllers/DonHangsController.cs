using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Nhom24.Data;
using Nhom24.Models;
using Nhom24.Models.Process;

namespace Nhom24.Controllers
{
    public class DonHangsController : Controller
    {
        private readonly Nhom24Context _context;
        private ExcelProcess _excelProcess = new ExcelProcess();
        private StringProcess _stringProcess = new StringProcess();

        public DonHangsController(Nhom24Context context)
        {
            _context = context;
        }

        // GET: DonHangs
        public async Task<IActionResult> Index()
        {
            var nhom24Context = _context.DonHang.Include(d => d.NguoiDung);
            return View(await nhom24Context.ToListAsync());
        }

        // GET: DonHangs/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null || _context.DonHang == null)
            {
                return NotFound();
            }

            var donHang = await _context.DonHang
                .Include(d => d.NguoiDung)
                .FirstOrDefaultAsync(m => m.DonHangID == id);
            if (donHang == null)
            {
                return NotFound();
            }

            return View(donHang);
        }

        // GET: DonHangs/Create
        public IActionResult Create()
        {
            ViewData["NguoiDungID"] = new SelectList(_context.NguoiDung, "NguoiDungID", "NguoiDungID");
            var newID = "";
            if (_context.DonHang.Count() == 0)
            {
                newID = "DH0001";
            }
            else
            {
                var id = _context.DonHang.OrderByDescending(m => m.DonHangID).First().DonHangID;
                newID = _stringProcess.AutoGenerateCode(id);
            }
            ViewBag.DonHangID = newID;
            return View();
        }

        // POST: DonHangs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("DonHangID,DiaChiDonHang,ThanhTien,NguoiDungID")] DonHang donHang)
        {
            if (ModelState.IsValid)
            {
                _context.Add(donHang);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["NguoiDungID"] = new SelectList(_context.NguoiDung, "NguoiDungID", "NguoiDungID", donHang.NguoiDungID);
            return View(donHang);
        }

        // GET: DonHangs/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null || _context.DonHang == null)
            {
                return NotFound();
            }

            var donHang = await _context.DonHang.FindAsync(id);
            if (donHang == null)
            {
                return NotFound();
            }
            ViewData["NguoiDungID"] = new SelectList(_context.NguoiDung, "NguoiDungID", "NguoiDungID", donHang.NguoiDungID);
            return View(donHang);
        }

        // POST: DonHangs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("DonHangID,DiaChiDonHang,ThanhTien,NguoiDungID")] DonHang donHang)
        {
            if (id != donHang.DonHangID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(donHang);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DonHangExists(donHang.DonHangID))
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
            ViewData["NguoiDungID"] = new SelectList(_context.NguoiDung, "NguoiDungID", "NguoiDungID", donHang.NguoiDungID);
            return View(donHang);
        }

        // GET: DonHangs/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null || _context.DonHang == null)
            {
                return NotFound();
            }

            var donHang = await _context.DonHang
                .Include(d => d.NguoiDung)
                .FirstOrDefaultAsync(m => m.DonHangID == id);
            if (donHang == null)
            {
                return NotFound();
            }

            return View(donHang);
        }

        // POST: DonHangs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            if (_context.DonHang == null)
            {
                return Problem("Entity set 'Nhom24Context.DonHang'  is null.");
            }
            var donHang = await _context.DonHang.FindAsync(id);
            if (donHang != null)
            {
                _context.DonHang.Remove(donHang);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DonHangExists(string id)
        {
          return _context.DonHang.Any(e => e.DonHangID == id);
        }
    }
}
