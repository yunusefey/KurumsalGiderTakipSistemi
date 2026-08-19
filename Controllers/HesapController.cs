using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using KurumsalGiderTakipSistemi.Data;

namespace KurumsalGiderTakipSistemi.Controllers
{
    public class HesapController : Controller
    {
        private readonly AppDbContext _context;

        public HesapController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Giris()
        {
            if (User.Identity != null && User.Identity.IsAuthenticated)
                return RedirectToAction("Index", "Home");

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Giris(string email, string sifre)
        {
            var user = await _context.Kullanicilar
                .FirstOrDefaultAsync(u => u.Email == email && u.Sifre == sifre);

            if (user == null)
            {
                ViewBag.Hata = "E-posta veya şifre hatalı!";
                return View();
            }

            // Kimlik ve Rol Bilgilerini (Claims) Tanımla
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.TamAd),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Role, user.Role.ToString()) // "Calisan" veya "Yonetici"
            };

            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var authProperties = new AuthenticationProperties
            {
                IsPersistent = true // Beni Hatırla
            };

            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity),
                authProperties);

            return RedirectToAction("Index", "Home");
        }

        public async Task<IActionResult> Cikis()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Giris", "Hesap");
        }

        public IActionResult ErisimEngellendi()
        {
            return View();
        }
    }
}