using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using KurumsalGiderTakipSistemi.Data;
using KurumsalGiderTakipSistemi.Models;

namespace KurumsalGiderTakipSistemi.Controllers
{
    public class DepartmanController : Controller
    {
        private readonly AppDbContext _context;

        public DepartmanController(AppDbContext context)
        {
            _context = context;
        }

        // Departmanları Listele
        public async Task<IActionResult> Index()
        {
            var departmanlar = await _context.Departmanlar
                .Include(d => d.Kullanicilar)
                .ToListAsync();

            return View(departmanlar);
        }

        // Yeni Departman Ekle (POST)
        [HttpPost]
        public async Task<IActionResult> Ekle(string ad)
        {
            if (!string.IsNullOrWhiteSpace(ad))
            {
                _context.Departmanlar.Add(new Departman { Ad = ad });
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }

        // Departman Sil (POST)
        [HttpPost]
        public async Task<IActionResult> Sil(int id)
        {
            var departman = await _context.Departmanlar.FindAsync(id);
            if (departman != null)
            {
                _context.Departmanlar.Remove(departman);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }
    }
}