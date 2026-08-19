using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using KurumsalGiderTakipSistemi.Data;
using KurumsalGiderTakipSistemi.Models;

namespace KurumsalGiderTakipSistemi.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly AppDbContext _context;

        public HomeController(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var userIdString = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userIdString) || !int.TryParse(userIdString, out int userId))
            {
                return RedirectToAction("Giris", "Hesap");
            }

            var model = new DashboardViewModel();

            // Aktif kullanıcının kendi harcamaları
            var userHarcamalari = await _context.Harcamalar
                .Where(h => h.KullaniciId == userId)
                .ToListAsync();

            model.ToplamHarcamaTutari = userHarcamalari.Sum(h => h.Tutar);
            model.OnaylananTutar = userHarcamalari.Where(h => h.Status == ExpenseStatus.Approved).Sum(h => h.Tutar);
            model.BekleyenHarcamaSayisi = userHarcamalari.Count(h => h.Status == ExpenseStatus.Pending);
            model.OnaylananHarcamaSayisi = userHarcamalari.Count(h => h.Status == ExpenseStatus.Approved);
            model.ReddedilenHarcamaSayisi = userHarcamalari.Count(h => h.Status == ExpenseStatus.Rejected);

            // Yönetici veya Admin ise şirket geneli istatistikleri de ekle
            if (User.IsInRole("Yonetici") || User.IsInRole("Admin"))
            {
                var tumHarcamalar = await _context.Harcamalar.ToListAsync();
                model.SirketToplamHarcama = tumHarcamalar.Where(h => h.Status == ExpenseStatus.Approved).Sum(h => h.Tutar);
                model.SirketBekleyenOnaySayisi = tumHarcamalar.Count(h => h.Status == ExpenseStatus.Pending);
                model.ToplamCalisanSayisi = await _context.Kullanicilar.CountAsync();

                model.SonHarcamalar = await _context.Harcamalar
                    .Include(h => h.Kullanici)
                    .Include(h => h.HarcamaKategori)
                    .OrderByDescending(h => h.OlusturulmaTarihi)
                    .Take(5)
                    .ToListAsync();
            }

            return View(model);
        }
    }
}