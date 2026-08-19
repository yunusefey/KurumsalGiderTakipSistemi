using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using KurumsalGiderTakipSistemi.Data;
using KurumsalGiderTakipSistemi.Models;

var builder = WebApplication.CreateBuilder(args);

// MVC Servisleri
builder.Services.AddControllersWithViews();

// Veritabanı Bağlantısı
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Cookie Authentication (Kimlik Doğrulama & Oturum Yönetimi)
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Hesap/Giris";
        options.LogoutPath = "/Hesap/Cikis";
        options.AccessDeniedPath = "/Hesap/ErisimEngellendi";
        options.ExpireTimeSpan = TimeSpan.FromDays(7);
        options.SlidingExpiration = true;
    });

var app = builder.Build();

// --- BAŞLANGIÇ VERİLERİNİ YÜKLEME (SEED DATA) ---
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();

    // 1. Departmanlar (Gerekli temel liste)
    if (!context.Departmanlar.Any())
    {
        context.Departmanlar.AddRange(
            new Departman { Ad = "Yazılım & Ar-Ge" },
            new Departman { Ad = "Finans & Muhasebe" },
            new Departman { Ad = "İnsan Kaynakları" },
            new Departman { Ad = "Satış & Pazarlama" }
        );
        context.SaveChanges();
    }

    // 2. Kategoriler (Gerekli temel liste)
    if (!context.HarcamaKategorileri.Any())
    {
        context.HarcamaKategorileri.AddRange(
            new HarcamaKategori { Ad = "Yemek & Temsil", MaxLimit = 1000 },
            new HarcamaKategori { Ad = "Ulaşım & Seyahat", MaxLimit = 2500 },
            new HarcamaKategori { Ad = "Konaklama", MaxLimit = 5000 },
            new HarcamaKategori { Ad = "Ofis & Kırtasiye", MaxLimit = 750 }
        );
        context.SaveChanges();
    }

    // 3. Sadece İlk Kurulum İçin Tek Bir Ana Admin (Sisteme ilk girişi yapabilmek için)
    if (!context.Kullanicilar.Any(u => u.Role == UserRole.Admin))
    {
        var adminDep = context.Departmanlar.FirstOrDefault();
        context.Kullanicilar.Add(new Kullanici
        {
            TamAd = "Sistem Yöneticisi",
            Email = "admin@sirket.com",
            Sifre = "admin123",
            Role = UserRole.Admin,
            DepartmanId = adminDep?.Id
        });
        context.SaveChanges();
    }
}
// ------------------------------------------------
// ------------------------------------------------

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

// Authentication her zaman Authorization'dan önce gelmelidir
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();