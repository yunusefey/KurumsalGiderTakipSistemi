using Microsoft.EntityFrameworkCore;
using KurumsalGiderTakipSistemi.Models;

namespace KurumsalGiderTakipSistemi.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Kullanici> Kullanicilar { get; set; }
        public DbSet<Departman> Departmanlar { get; set; }
        public DbSet<HarcamaKategori> HarcamaKategorileri { get; set; }
        public DbSet<Harcama> Harcamalar { get; set; }
    }
}