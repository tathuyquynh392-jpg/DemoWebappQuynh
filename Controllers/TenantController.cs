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
    public class TenantController : Controller
    {
        private readonly TroDbContext _context;

        public TenantController(TroDbContext context)
        {
            _context = context;
        }

        // GET: Tenants
        public async Task<IActionResult> Index()
        {
            var tenants = await _context.Tenants.Include(t => t.Room).ToListAsync();
            return View(tenants);
        }

        // GET: Tenants/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var tenant = await _context.Tenants
                .Include(t => t.Room)
                .FirstOrDefaultAsync(m => m.TenantId == id);

            if (tenant == null) return NotFound();

            return View(tenant);
        }

        // GET: Tenants/Create
        // GET: Tenants/Create
        public IActionResult Create()
        {
            // Lấy list phòng, Value = RoomId, Text = TenPhong
            ViewData["RoomList"] = new SelectList(_context.Rooms, "RoomId", "TenPhong");
            return View();
        }

        // POST: Tenants/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TenantId,TenNguoi,SDT,RoomId")] Tenant tenant)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();
                return Content("ModelState invalid: " + string.Join(", ", errors));
            }

            _context.Add(tenant);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // GET: Tenants/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var tenant = await _context.Tenants.FindAsync(id);
            if (tenant == null) return NotFound();

            ViewData["RoomList"] = new SelectList(_context.Rooms, "RoomId", "TenPhong");
            return View(tenant);
        }

        // POST: Tenants/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("TenantId,TenNguoi,SDT,RoomId")] Tenant tenant)
        {
            if (id != tenant.TenantId) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tenant);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_context.Tenants.Any(e => e.TenantId == tenant.TenantId))
                        return NotFound();
                    else
                        throw;
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["RoomList"] = new SelectList(_context.Rooms, "RoomId", "TenPhong", tenant.RoomId);
            return View(tenant);
        }

        // GET: Tenants/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var tenant = await _context.Tenants
                .Include(t => t.Room)
                .FirstOrDefaultAsync(m => m.TenantId == id);

            if (tenant == null) return NotFound();

            return View(tenant);
        }

        // POST: Tenants/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var tenant = await _context.Tenants.FindAsync(id);
            if (tenant != null)
            {
                _context.Tenants.Remove(tenant);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }
    }
}