namespace KurumsalGiderTakipSistemi.Models
{
    public class DashboardViewModel
    {
        // Çalışan Metrikleri
        public decimal ToplamHarcamaTutari { get; set; }
        public decimal OnaylananTutar { get; set; }
        public int BekleyenHarcamaSayisi { get; set; }
        public int OnaylananHarcamaSayisi { get; set; }
        public int ReddedilenHarcamaSayisi { get; set; }

        // Yönetici / Admin Metrikleri
        public decimal SirketToplamHarcama { get; set; }
        public int SirketBekleyenOnaySayisi { get; set; }
        public int ToplamCalisanSayisi { get; set; }
        public List<Harcama> SonHarcamalar { get; set; } = new List<Harcama>();
    }
}