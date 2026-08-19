namespace KurumsalGiderTakipSistemi.Models
{
    public class Departman
    {
        public int Id { get; set; }
        public string Ad { get; set; } = string.Empty;

        public ICollection<Kullanici> Kullanicilar { get; set; } = new List<Kullanici>();
    }
}