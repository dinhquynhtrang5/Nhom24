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
    public class SanPhamsController : Controller
    {
        private readonly Nhom24Context _context;
        private ExcelProcess _excelProcess = new ExcelProcess();
        private StringProcess _stringProcess = new StringProcess();
        public SanPhamsController(Nhom24Context context)
        {
            _context = context;
        }

        // GET: SanPhams
        public async Task<IActionResult> Index()
        {
            var nhom24Context = _context.SanPham.Include(s => s.NganhHang);
            return View(await nhom24Context.ToListAsync());
        }

        // GET: SanPhams/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null || _context.SanPham == null)
            {
                return NotFound();
            }

            var sanPham = await _context.SanPham
                .Include(s => s.NganhHang)
                .FirstOrDefaultAsync(m => m.SanPhamID == id);
            if (sanPham == null)
            {
                return NotFound();
            }

            return View(sanPham);
        }

        // GET: SanPhams/Create
        public IActionResult Create()
        {
            ViewData["NganhHangID"] = new SelectList(_context.NganhHang, "NganhHangID", "NganhHangName");
            var newID = "";
            if (_context.SanPham.Count() == 0)
            {
                newID = "SP0001";
            }
            else
            {
                var id = _context.SanPham.OrderByDescending(m => m.SanPhamID).First().SanPhamID;
                newID = _stringProcess.AutoGenerateCode(id);
            }
            ViewBag.SanPhamID = newID;
            return View();
        }

        // POST: SanPhams/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("SanPhamID,SanPhamName,AnhSanPham,GiaSanPham,SoLuongSanPham,MoTaSanPham,NganhHangID")] SanPham sanPham)
        {
            if (ModelState.IsValid)
            {
                _context.Add(sanPham);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["NganhHangID"] = new SelectList(_context.NganhHang, "NganhHangID", "NganhHangID", sanPham.NganhHangID);
            return View(sanPham);
        }

        // GET: SanPhams/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null || _context.SanPham == null)
            {
                return NotFound();
            }

            var sanPham = await _context.SanPham.FindAsync(id);
            if (sanPham == null)
            {
                return NotFound();
            }
            ViewData["NganhHangID"] = new SelectList(_context.NganhHang, "NganhHangID", "NganhHangID", sanPham.NganhHangID);
            return View(sanPham);
        }

        // POST: SanPhams/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("SanPhamID,SanPhamName,AnhSanPham,GiaSanPham,SoLuongSanPham,MoTaSanPham,NganhHangID")] SanPham sanPham)
        {
            if (id != sanPham.SanPhamID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(sanPham);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SanPhamExists(sanPham.SanPhamID))
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
            ViewData["NganhHangID"] = new SelectList(_context.NganhHang, "NganhHangID", "NganhHangID", sanPham.NganhHangID);
            return View(sanPham);
        }

        // GET: SanPhams/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null || _context.SanPham == null)
            {
                return NotFound();
            }

            var sanPham = await _context.SanPham
                .Include(s => s.NganhHang)
                .FirstOrDefaultAsync(m => m.SanPhamID == id);
            if (sanPham == null)
            {
                return NotFound();
            }

            return View(sanPham);
        }

        // POST: SanPhams/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            if (_context.SanPham == null)
            {
                return Problem("Entity set 'Nhom24Context.SanPham'  is null.");
            }
            var sanPham = await _context.SanPham.FindAsync(id);
            if (sanPham != null)
            {
                _context.SanPham.Remove(sanPham);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Upload()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Upload(IFormFile file)
        {
            if (file != null)
            {
                string fileExtension = Path.GetExtension(file.FileName);
                if (fileExtension != ".xls" && fileExtension != ".xlsx")
                {
                    Console.WriteLine("cant upload");
                    ModelState.AddModelError("SanPham", "Please choose excel file to upload!");
                }
                else
                {
                    //rename file when upload to server 
                    var fileName = DateTime.Now.ToShortTimeString() + fileExtension;
                    var filePath = Path.Combine(Directory.GetCurrentDirectory() + "/Uploads_Excels", fileName);
                    var fileLocation = new FileInfo(filePath).ToString();
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        //save file to update
                        await file.CopyToAsync(stream);
                        var dt = _excelProcess.ExcelToDataTable(fileLocation);
                        //Sinh mã tự động cho file Excel
                        var id = "";
                        if (_context.SanPham.Count() == 0)
                        {
                            id = "SP0000";
                        }
                        else
                        {
                            id = _context.SanPham.OrderByDescending(m => m.SanPhamID).First().SanPhamID;
                        }
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            var newSp = new SanPham();
                            id = _stringProcess.AutoGenerateCode(id);
                            newSp.SanPhamID = id;
                            newSp.SanPhamName = dt.Rows[i][0].ToString();
                            newSp.AnhSanPham = dt.Rows[i][1].ToString();
                            newSp.GiaSanPham = Convert.ToDouble(dt.Rows[i][2]);
                            newSp.GiaSanPham = Convert.ToDouble(dt.Rows[i][2]);
                            newSp.SoLuongSanPham = Convert.ToInt32(dt.Rows[i][3]);
                            newSp.MoTaSanPham = dt.Rows[i][4].ToString();
                            newSp.NganhHangID = dt.Rows[i][5].ToString();
                            _context.SanPham.Add(newSp);
                        }
                        await _context.SaveChangesAsync();
                        return RedirectToAction(nameof(Index));
                    }
                }
            }
            else
            {
                Console.WriteLine("cant upload");
            }
            return View();
        }
        private bool SanPhamExists(string id)
        {
          return _context.SanPham.Any(e => e.SanPhamID == id);
        }
    }
}
