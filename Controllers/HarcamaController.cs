using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using KurumsalGiderTakipSistemi.Data;
using KurumsalGiderTakipSistemi.Models;

namespace KurumsalGiderTakipSistemi.Controllers
{
    [Authorize]
    public class HarcamaController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;

        public HarcamaController(AppDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        public async Task<IActionResult> Index()
        {
            var userIdString = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userIdString) || !int.TryParse(userIdString, out int userId))
            {
                return RedirectToAction("Giris", "Hesap");
            }

            var harcamalar = await _context.Harcamalar
                .Include(h => h.HarcamaKategori)
                .Where(h => h.KullaniciId == userId)
                .OrderByDescending(h => h.HarcamaTarihi)
                .ToListAsync();

            return View(harcamalar);
        }

        [HttpGet]
        public async Task<IActionResult> Ekle()
        {
            var kategoriler = await _context.HarcamaKategorileri.ToListAsync();
            ViewBag.Kategoriler = new SelectList(kategoriler, "Id", "Ad");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Ekle(Harcama harcama, IFormFile? fisDosyasi)
        {
            var kategori = await _context.HarcamaKategorileri.FindAsync(harcama.HarcamaKategoriId);

            // 1. Kategori Limit Aşımı Kontrolü
            if (kategori != null && kategori.MaxLimit.HasValue && harcama.Tutar > kategori.MaxLimit.Value)
            {
                ViewBag.Hata = $"'{kategori.Ad}' kategorisi için izin verilen maksimum tutar {kategori.MaxLimit.Value:C2}'dir. Girdiğiniz tutar ({harcama.Tutar:C2}) bu limiti aşıyor!";
                ViewBag.Kategoriler = new SelectList(await _context.HarcamaKategorileri.ToListAsync(), "Id", "Ad", harcama.HarcamaKategoriId);
                return View(harcama);
            }

            // 2. Fiş Yükleme İşlemi
            if (fisDosyasi != null && fisDosyasi.Length > 0)
            {
                var uploadsFolder = Path.Combine(_env.WebRootPath, "uploads");
                if (!Directory.Exists(uploadsFolder))
                {
                    Directory.CreateDirectory(uploadsFolder);
                }

                var uniqueFileName = Guid.NewGuid().ToString() + "_" + Path.GetFileName(fisDosyasi.FileName);
                var filePath = Path.Combine(uploadsFolder, uniqueFileName);

                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await fisDosyasi.CopyToAsync(fileStream);
                }

                harcama.FisYolu = "/uploads/" + uniqueFileName;
            }

            var userIdString = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userIdString) || !int.TryParse(userIdString, out int userId))
            {
                return RedirectToAction("Giris", "Hesap");
            }

            harcama.KullaniciId = userId;
            harcama.Status = ExpenseStatus.Pending;
            harcama.OlusturulmaTarihi = DateTime.Now;

            _context.Harcamalar.Add(harcama);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
    }
}