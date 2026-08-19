namespace KurumsalGiderTakipSistemi.Models
{
    public enum ExpenseStatus
    {
        Pending = 1,   // Onay Bekliyor
        Approved = 2,  // Onaylandı
        Rejected = 3   // Reddedildi
    }

    public class Harcama
    {
        public int Id { get; set; }
        public decimal Tutar { get; set; }
        public string Aciklama { get; set; } = string.Empty;
        public DateTime HarcamaTarihi { get; set; } = DateTime.Now;
        public DateTime OlusturulmaTarihi { get; set; } = DateTime.Now;

        public string? FisYolu { get; set; }
        public ExpenseStatus Status { get; set; } = ExpenseStatus.Pending;
        public string? RedNedeni { get; set; }

        public int KullaniciId { get; set; }
        public Kullanici? Kullanici { get; set; }

        public int HarcamaKategoriId { get; set; }
        public HarcamaKategori? HarcamaKategori { get; set; }
    }
}