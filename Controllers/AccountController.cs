using DemoWebapp.Data;
using DemoWebapp.Models;
using Microsoft.AspNetCore.Mvc;

namespace DemoWebapp.Controllers
{
    public class AccountController : Controller
    {
        private readonly TroDbContext _context;

        public AccountController(TroDbContext context)
        {
            _context = context;
        }

        // GET: /Account/Login
        public IActionResult Login()
        {
            return View();
        }

        // POST: /Account/Login
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Login(string username, string password)
        {
            // Kiểm tra username/password
            // Ở đây tạm dùng tài khoản cứng, sau này bạn có thể check DB
            if (username == "admin" && password == "123")
            {
                // ✅ Lưu username vào session
                HttpContext.Session.SetString("user", username);

                // Redirect về Home
                return RedirectToAction("Index", "Home");
            }

            ViewBag.Error = "Sai tài khoản hoặc mật khẩu";
            return View();
        }

        // Logout
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login");
        }
    }
}