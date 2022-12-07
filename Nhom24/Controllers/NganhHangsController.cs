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
    public class NganhHangsController : Controller
    {
        private readonly Nhom24Context _context;
        private ExcelProcess _excelProcess = new ExcelProcess();
        private StringProcess _stringProcess = new StringProcess();

        public NganhHangsController(Nhom24Context context)
        {
            _context = context;
        }

        // GET: NganhHangs
        public async Task<IActionResult> Index()
        {
            var nhom24Context = _context.NganhHang.Include(n => n.DanhMucSanPham);
            return View(await nhom24Context.ToListAsync());
        }

        // GET: NganhHangs/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null || _context.NganhHang == null)
            {
                return NotFound();
            }

            var nganhHang = await _context.NganhHang
                .Include(n => n.DanhMucSanPham)
                .FirstOrDefaultAsync(m => m.NganhHangID == id);
            if (nganhHang == null)
            {
                return NotFound();
            }

            return View(nganhHang);
        }

        // GET: NganhHangs/Create
        public IActionResult Create()
        {
            ViewData["DanhMucSanPhamID"] = new SelectList(_context.DanhMucSanPham, "DanhMucSanPhamID", "DanhMucSanPhamID");
            var newID = "";
            if (_context.NganhHang.Count() == 0)
            {
                newID = "NH0001";
            }
            else
            {
                var id = _context.NganhHang.OrderByDescending(m => m.NganhHangID).First().NganhHangID;
                newID = _stringProcess.AutoGenerateCode(id);
            }
            ViewBag.NganhHangID = newID;
            return View();
        }

        // POST: NganhHangs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("NganhHangID,NganhHangName,DanhMucSanPhamID")] NganhHang nganhHang)
        {
            if (ModelState.IsValid)
            {
                _context.Add(nganhHang);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["DanhMucSanPhamID"] = new SelectList(_context.DanhMucSanPham, "DanhMucSanPhamID", "DanhMucSanPhamID", nganhHang.DanhMucSanPhamID);
            return View(nganhHang);
        }

        // GET: NganhHangs/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null || _context.NganhHang == null)
            {
                return NotFound();
            }

            var nganhHang = await _context.NganhHang.FindAsync(id);
            if (nganhHang == null)
            {
                return NotFound();
            }
            ViewData["DanhMucSanPhamID"] = new SelectList(_context.DanhMucSanPham, "DanhMucSanPhamID", "DanhMucSanPhamID", nganhHang.DanhMucSanPhamID);
            return View(nganhHang);
        }

        // POST: NganhHangs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("NganhHangID,NganhHangName,DanhMucSanPhamID")] NganhHang nganhHang)
        {
            if (id != nganhHang.NganhHangID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(nganhHang);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!NganhHangExists(nganhHang.NganhHangID))
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
            ViewData["DanhMucSanPhamID"] = new SelectList(_context.DanhMucSanPham, "DanhMucSanPhamID", "DanhMucSanPhamID", nganhHang.DanhMucSanPhamID);
            return View(nganhHang);
        }

        // GET: NganhHangs/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null || _context.NganhHang == null)
            {
                return NotFound();
            }

            var nganhHang = await _context.NganhHang
                .Include(n => n.DanhMucSanPham)
                .FirstOrDefaultAsync(m => m.NganhHangID == id);
            if (nganhHang == null)
            {
                return NotFound();
            }

            return View(nganhHang);
        }

        // POST: NganhHangs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            if (_context.NganhHang == null)
            {
                return Problem("Entity set 'Nhom24Context.NganhHang'  is null.");
            }
            var nganhHang = await _context.NganhHang.FindAsync(id);
            if (nganhHang != null)
            {
                _context.NganhHang.Remove(nganhHang);
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
                    ModelState.AddModelError("NganhHang", "Please choose excel file to upload!");
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
                        if (_context.NganhHang.Count() == 0)
                        {
                            id = "NH0000";
                        }
                        else
                        {
                            id = _context.NganhHang.OrderByDescending(m => m.NganhHangID).First().NganhHangID;
                        }
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            var newNh = new NganhHang();
                            id = _stringProcess.AutoGenerateCode(id);
                            newNh.NganhHangID = id;
                            newNh.NganhHangName = dt.Rows[i][0].ToString();
                            newNh.DanhMucSanPhamID = dt.Rows[i][1].ToString();
                            _context.NganhHang.Add(newNh);
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
        private bool NganhHangExists(string id)
        {
          return _context.NganhHang.Any(e => e.NganhHangID == id);
        }
    }
}
