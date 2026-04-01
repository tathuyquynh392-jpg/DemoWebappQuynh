using System;
using DemoWebapp.Data;
using Microsoft.AspNetCore.Mvc;

namespace DemoWebapp.Controllers
{
    // HomeController kế thừa BaseController để kiểm tra session
    public class HomeController : BaseController
    {
        private readonly TroDbContext _context;
        public HomeController(TroDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            var tongPhong = _context.Rooms.Count();
            var phongDaThue = _context.Rooms.Count(p => p.TrangThai == "Đã thuê");
            var tongNguoiThue = _context.Tenants.Count();
            var tongDoanhThu = _context.Bills
    .Sum(h => (decimal)h.Dien + (decimal)h.Nuoc);

            ViewBag.TongPhong = tongPhong;
            ViewBag.PhongDaThue = phongDaThue;
            ViewBag.TongNguoiThue = tongNguoiThue;
            ViewBag.TongDoanhThu = tongDoanhThu;
            // Session đã được check ở BaseController
            ViewBag.User = HttpContext.Session.GetString("user");
            return View();
        }
    }
}