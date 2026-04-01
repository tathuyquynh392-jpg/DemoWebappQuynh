using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DemoWebapp.Data;
using DemoWebapp.Models;

namespace DemoWebapp.Controllers
{
    public class BillController : Controller
    {
        private readonly TroDbContext _context;

        public BillController(TroDbContext context)
        {
            _context = context;
        }

        // GET: Bills
        public async Task<IActionResult> Index()
        {
            var troDbContext = _context.Bills.Include(b => b.Room);
            return View(await troDbContext.ToListAsync());
        }

        // GET: Bills/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bill = await _context.Bills
                .Include(b => b.Room)
                .FirstOrDefaultAsync(m => m.BillId == id);
            if (bill == null)
            {
                return NotFound();
            }

            return View(bill);
        }

        // GET: Bills/Create
        public IActionResult Create()
        {
            ViewBag.RoomList = new SelectList(_context.Rooms, "RoomId", "TenPhong");
            return View();
        }

        // POST: Bills/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("RoomId,Dien,Nuoc,TongTien")] Bill bill)
        {
            // 🔥 DEBUG THẲNG RA MÀN HÌNH
            if (!ModelState.IsValid)
            {
                string loi = "";

                foreach (var state in ModelState)
                {
                    foreach (var error in state.Value.Errors)
                    {
                        loi += state.Key + ": " + error.ErrorMessage + "\n";
                    }
                }

                return Content("LỖI:\n" + loi);
            }

            int giaDien = 3500;
            int giaNuoc = 15000;

            var room = await _context.Rooms.FindAsync(bill.RoomId);
            decimal tienPhong = room?.Gia ?? 0;

            bill.TongTien = (bill.Dien * giaDien)
                          + (bill.Nuoc * giaNuoc)
                          + tienPhong;

            _context.Add(bill);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var bill = await _context.Bills.FindAsync(id);
            if (bill == null) return NotFound();

            ViewBag.RoomList = new SelectList(_context.Rooms, "RoomId", "TenPhong", bill.RoomId);

            return View(bill);
        }
        // POST: Bills/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Bill bill)
        {
            if (id != bill.BillId) return NotFound();

            if (ModelState.IsValid)
            {
                int giaDien = 3500;
                int giaNuoc = 15000;

                var room = await _context.Rooms.FindAsync(bill.RoomId);
                decimal tienPhong = room?.Gia ?? 0;

                bill.TongTien = (bill.Dien * giaDien)
                              + (bill.Nuoc * giaNuoc)
                              + tienPhong;

                _context.Update(bill);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }

            ViewBag.RoomList = new SelectList(_context.Rooms, "RoomId", "TenPhong", bill.RoomId);
            return View(bill);
                }

        // GET: Bills/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bill = await _context.Bills
                .Include(b => b.Room)
                .FirstOrDefaultAsync(m => m.BillId == id);
            if (bill == null)
            {
                return NotFound();
            }

            return View(bill);
        }

        // POST: Bills/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var bill = await _context.Bills.FindAsync(id);
            if (bill != null)
            {
                _context.Bills.Remove(bill);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BillExists(int id)
        {
            return _context.Bills.Any(e => e.BillId == id);
        }
    }
}
