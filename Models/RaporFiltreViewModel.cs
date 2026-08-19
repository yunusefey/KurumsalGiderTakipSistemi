namespace KurumsalGiderTakipSistemi.Models
{
    public class RaporFiltreViewModel
    {
        public DateTime? BaslangicTarihi { get; set; }
        public DateTime? BitisTarihi { get; set; }
        public int? KategoriId { get; set; }
        public int? DepartmanId { get; set; }
        public ExpenseStatus? Durum { get; set; }

        public decimal ToplamTutar { get; set; }
        public List<Harcama> Harcamalar { get; set; } = new List<Harcama>();
    }
}