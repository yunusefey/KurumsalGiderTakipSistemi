using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using KurumsalGiderTakipSistemi.Data;
using KurumsalGiderTakipSistemi.Models;

namespace KurumsalGiderTakipSistemi.Controllers
{
    // Hem Yönetici hem de Admin rolü erişebilir
    [Authorize(Roles = "Yonetici,Admin")]
    public class YoneticiController : Controller
    {
        private readonly AppDbContext _context;

        public YoneticiController(AppDbContext context)
        {
            _context = context;
        }

        // Onay bekleyen harcama taleplerini listele
        public async Task<IActionResult> OnayListesi()
        {
            var bekleyenHarcamalar = await _context.Harcamalar
                .Include(h => h.HarcamaKategori)
                .Include(h => h.Kullanici)
                .Where(h => h.Status == ExpenseStatus.Pending)
                .OrderBy(h => h.OlusturulmaTarihi)
                .ToListAsync();

            return View(bekleyenHarcamalar);
        }

        // Harcamayı Onayla (POST)
        [HttpPost]
        public async Task<IActionResult> Onayla(int id)
        {
            var harcama = await _context.Harcamalar.FindAsync(id);
            if (harcama != null)
            {
                harcama.Status = ExpenseStatus.Approved;
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(OnayListesi));
        }

        // Harcamayı Reddet (POST)
        [HttpPost]
        public async Task<IActionResult> Reddet(int id, string redNedeni)
        {
            var harcama = await _context.Harcamalar.FindAsync(id);
            if (harcama != null)
            {
                harcama.Status = ExpenseStatus.Rejected;
                harcama.RedNedeni = string.IsNullOrWhiteSpace(redNedeni) ? "Gerekçe belirtilmedi." : redNedeni;
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(OnayListesi));
        }
    }
}