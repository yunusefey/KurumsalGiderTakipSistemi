using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using KurumsalGiderTakipSistemi.Data;
using KurumsalGiderTakipSistemi.Models;

namespace KurumsalGiderTakipSistemi.Controllers
{
    [Authorize(Roles = "Admin")] // Bu sayfaya SADECE Admin rütbesi girebilir
    public class KullaniciController : Controller
    {
        private readonly AppDbContext _context;

        public KullaniciController(AppDbContext context)
        {
            _context = context;
        }

        // Kullanıcı Listesi ve Ekleme Ekranı
        public async Task<IActionResult> Index()
        {
            var kullanicilar = await _context.Kullanicilar
                .Include(k => k.Departman)
                .OrderBy(k => k.Id)
                .ToListAsync();

            ViewBag.Departmanlar = new SelectList(await _context.Departmanlar.ToListAsync(), "Id", "Ad");
            return View(kullanicilar);
        }

        // Yeni Kullanıcı Kaydet (POST)
        [HttpPost]
        public async Task<IActionResult> Ekle(Kullanici yeniKullanici)
        {
            // E-posta adresi sistemde zaten kayıtlı mı kontrolü
            var emailVarMi = await _context.Kullanicilar.AnyAsync(k => k.Email == yeniKullanici.Email);
            if (emailVarMi)
            {
                TempData["Hata"] = "Bu e-posta adresiyle kayıtlı bir kullanıcı zaten mevcut!";
                return RedirectToAction(nameof(Index));
            }

            if (!string.IsNullOrWhiteSpace(yeniKullanici.TamAd) && !string.IsNullOrWhiteSpace(yeniKullanici.Email) && !string.IsNullOrWhiteSpace(yeniKullanici.Sifre))
            {
                _context.Kullanicilar.Add(yeniKullanici);
                await _context.SaveChangesAsync();
                TempData["Basari"] = $"{yeniKullanici.TamAd} başarıyla sisteme eklendi.";
            }

            return RedirectToAction(nameof(Index));
        }

        // Kullanıcı Sil (POST)
        [HttpPost]
        public async Task<IActionResult> Sil(int id)
        {
            var kullanici = await _context.Kullanicilar.FindAsync(id);
            if (kullanici != null)
            {
                // Admin kendi hesabını silemesin kontrolü
                if (kullanici.Email == "admin@sirket.com")
                {
                    TempData["Hata"] = "Ana Süper Admin hesabı silinemez!";
                    return RedirectToAction(nameof(Index));
                }

                _context.Kullanicilar.Remove(kullanici);
                await _context.SaveChangesAsync();
                TempData["Basari"] = "Kullanıcı başarıyla silindi.";
            }

            return RedirectToAction(nameof(Index));
        }
    }
}