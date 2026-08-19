namespace KurumsalGiderTakipSistemi.Models
{
    public class HarcamaKategori
    {
        public int Id { get; set; }
        public string Ad { get; set; } = string.Empty;
        public decimal? MaxLimit { get; set; }

        public ICollection<Harcama> Harcamalar { get; set; } = new List<Harcama>();
    }
}