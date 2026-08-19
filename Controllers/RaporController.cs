using System.Text;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using KurumsalGiderTakipSistemi.Data;
using KurumsalGiderTakipSistemi.Models;

namespace KurumsalGiderTakipSistemi.Controllers
{
    [Authorize(Roles = "Yonetici,Admin")]
    public class RaporController : Controller
    {
        private readonly AppDbContext _context;

        public RaporController(AppDbContext context)
        {
            _context = context;
        }

        // Filtreleme ve Listeleme Ekranı
        public async Task<IActionResult> Index(DateTime? baslangicTarihi, DateTime? bitisTarihi, int? kategoriId, int? departmanId, ExpenseStatus? durum)
        {
            var query = _context.Harcamalar
                .Include(h => h.Kullanici)
                    .ThenInclude(u => u!.Departman)
                .Include(h => h.HarcamaKategori)
                .AsQueryable();

            if (baslangicTarihi.HasValue)
                query = query.Where(h => h.HarcamaTarihi >= baslangicTarihi.Value);

            if (bitisTarihi.HasValue)
                query = query.Where(h => h.HarcamaTarihi <= bitisTarihi.Value);

            if (kategoriId.HasValue && kategoriId > 0)
                query = query.Where(h => h.HarcamaKategoriId == kategoriId.Value);

            if (departmanId.HasValue && departmanId > 0)
                query = query.Where(h => h.Kullanici != null && h.Kullanici.DepartmanId == departmanId.Value);

            if (durum.HasValue)
                query = query.Where(h => h.Status == durum.Value);

            var liste = await query.OrderByDescending(h => h.HarcamaTarihi).ToListAsync();

            var model = new RaporFiltreViewModel
            {
                BaslangicTarihi = baslangicTarihi,
                BitisTarihi = bitisTarihi,
                KategoriId = kategoriId,
                DepartmanId = departmanId,
                Durum = durum,
                Harcamalar = liste,
                ToplamTutar = liste.Sum(h => h.Tutar)
            };

            ViewBag.Kategoriler = new SelectList(await _context.HarcamaKategorileri.ToListAsync(), "Id", "Ad", kategoriId);
            ViewBag.Departmanlar = new SelectList(await _context.Departmanlar.ToListAsync(), "Id", "Ad", departmanId);

            return View(model);
        }

        // Excel/CSV Çıktısı Alma (Türkçe Karakter Destekli)
        public async Task<IActionResult> ExcelIndir(DateTime? baslangicTarihi, DateTime? bitisTarihi, int? kategoriId, int? departmanId, ExpenseStatus? durum)
        {
            var query = _context.Harcamalar
                .Include(h => h.Kullanici)
                    .ThenInclude(u => u!.Departman)
                .Include(h => h.HarcamaKategori)
                .AsQueryable();

            if (baslangicTarihi.HasValue)
                query = query.Where(h => h.HarcamaTarihi >= baslangicTarihi.Value);

            if (bitisTarihi.HasValue)
                query = query.Where(h => h.HarcamaTarihi <= bitisTarihi.Value);

            if (kategoriId.HasValue && kategoriId > 0)
                query = query.Where(h => h.HarcamaKategoriId == kategoriId.Value);

            if (departmanId.HasValue && departmanId > 0)
                query = query.Where(h => h.Kullanici != null && h.Kullanici.DepartmanId == departmanId.Value);

            if (durum.HasValue)
                query = query.Where(h => h.Status == durum.Value);

            var harcamalar = await query.OrderByDescending(h => h.HarcamaTarihi).ToListAsync();

            var csv = new StringBuilder();
            csv.AppendLine("ID;Çalışan;Departman;Kategori;Tutar (TL);Tarih;Durum;Açıklama");

            foreach (var item in harcamalar)
            {
                var durumMetni = item.Status switch
                {
                    ExpenseStatus.Approved => "Onaylandı",
                    ExpenseStatus.Rejected => "Reddedildi",
                    _ => "Onay Bekliyor"
                };

                csv.AppendLine($"{item.Id};{item.Kullanici?.TamAd ?? "Bilinmiyor"};{item.Kullanici?.Departman?.Ad ?? "Yok"};{item.HarcamaKategori?.Ad ?? "Genel"};{item.Tutar:F2};{item.HarcamaTarihi:dd.MM.yyyy};{durumMetni};{item.Aciklama.Replace(";", ",")}");
            }

            var bytes = Encoding.UTF8.GetPreamble().Concat(Encoding.UTF8.GetBytes(csv.ToString())).ToArray();
            return File(bytes, "text/csv; charset=utf-8", $"HarcamaRaporu_{DateTime.Now:yyyyMMdd_HHmm}.csv");
        }
    }
}