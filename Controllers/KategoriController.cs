using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using KurumsalGiderTakipSistemi.Data;
using KurumsalGiderTakipSistemi.Models;

namespace KurumsalGiderTakipSistemi.Controllers
{
    public class KategoriController : Controller
    {
        private readonly AppDbContext _context;

        public KategoriController(AppDbContext context)
        {
            _context = context;
        }

        // Kategorileri Listele
        public async Task<IActionResult> Index()
        {
            var kategoriler = await _context.HarcamaKategorileri
                .Include(k => k.Harcamalar)
                .ToListAsync();

            return View(kategoriler);
        }

        // Yeni Kategori Ekle (POST)
        [HttpPost]
        public async Task<IActionResult> Ekle(string ad, decimal? maxLimit)
        {
            if (!string.IsNullOrWhiteSpace(ad))
            {
                var yeniKategori = new HarcamaKategori
                {
                    Ad = ad,
                    MaxLimit = maxLimit
                };

                _context.HarcamaKategorileri.Add(yeniKategori);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }

        // Kategori Sil (POST)
        [HttpPost]
        public async Task<IActionResult> Sil(int id)
        {
            var kategori = await _context.HarcamaKategorileri.FindAsync(id);
            if (kategori != null)
            {
                _context.HarcamaKategorileri.Remove(kategori);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }
    }
}