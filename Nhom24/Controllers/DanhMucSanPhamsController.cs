using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Nhom24.Data;
using Nhom24.Models;
using System.Runtime.InteropServices;
using Nhom24.Models.Process;
namespace Nhom24.Controllers
{
    public class DanhMucSanPhamsController : Controller
    {
        private readonly Nhom24Context _context;
        private ExcelProcess _excelProcess = new ExcelProcess();
        private StringProcess _stringProcess = new StringProcess();

        public DanhMucSanPhamsController(Nhom24Context context)
        {
            _context = context;
        }

        // GET: DanhMucSanPhams
        public async Task<IActionResult> Index()
        {
              return View(await _context.DanhMucSanPham.ToListAsync());
        }

        // GET: DanhMucSanPhams/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null || _context.DanhMucSanPham == null)
            {
                return NotFound();
            }

            var danhMucSanPham = await _context.DanhMucSanPham
                .FirstOrDefaultAsync(m => m.DanhMucSanPhamID == id);
            if (danhMucSanPham == null)
            {
                return NotFound();
            }

            return View(danhMucSanPham);
        }

        // GET: DanhMucSanPhams/Create
        public IActionResult Create()
        {
            var newID = "";
            if (_context.DanhMucSanPham.Count() == 0)
            {
                newID = "DMSP0001";
            }
            else
            {
                var id = _context.DanhMucSanPham.OrderByDescending(m => m.DanhMucSanPhamID).First().DanhMucSanPhamID;
                newID = _stringProcess.AutoGenerateCode(id);
            }
            ViewBag.DanhMucSanPhamID = newID;
            return View();
        }

        // POST: DanhMucSanPhams/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("DanhMucSanPhamID,DanhMucSanPhamName")] DanhMucSanPham danhMucSanPham)
        {
            if (ModelState.IsValid)
            {
                _context.Add(danhMucSanPham);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(danhMucSanPham);
        }

        // GET: DanhMucSanPhams/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null || _context.DanhMucSanPham == null)
            {
                return NotFound();
            }

            var danhMucSanPham = await _context.DanhMucSanPham.FindAsync(id);
            if (danhMucSanPham == null)
            {
                return NotFound();
            }
            return View(danhMucSanPham);
        }

        // POST: DanhMucSanPhams/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("DanhMucSanPhamID,DanhMucSanPhamName")] DanhMucSanPham danhMucSanPham)
        {
            if (id != danhMucSanPham.DanhMucSanPhamID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(danhMucSanPham);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DanhMucSanPhamExists(danhMucSanPham.DanhMucSanPhamID))
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
            return View(danhMucSanPham);
        }

        // GET: DanhMucSanPhams/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null || _context.DanhMucSanPham == null)
            {
                return NotFound();
            }

            var danhMucSanPham = await _context.DanhMucSanPham
                .FirstOrDefaultAsync(m => m.DanhMucSanPhamID == id);
            if (danhMucSanPham == null)
            {
                return NotFound();
            }

            return View(danhMucSanPham);
        }

        // POST: DanhMucSanPhams/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            if (_context.DanhMucSanPham == null)
            {
                return Problem("Entity set 'Nhom24Context.DanhMucSanPham'  is null.");
            }
            var danhMucSanPham = await _context.DanhMucSanPham.FindAsync(id);
            if (danhMucSanPham != null)
            {
                _context.DanhMucSanPham.Remove(danhMucSanPham);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        
        private bool DanhMucSanPhamExists(string id)
        {
          return _context.DanhMucSanPham.Any(e => e.DanhMucSanPhamID == id);
        }
    }
}
