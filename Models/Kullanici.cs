namespace KurumsalGiderTakipSistemi.Models
{
    public enum UserRole
    {
        Calisan = 1,
        Yonetici = 2,
        Admin = 3
    }

    public class Kullanici
    {
        public int Id { get; set; }
        public string TamAd { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Sifre { get; set; } = string.Empty;
        public UserRole Role { get; set; } = UserRole.Calisan;

        public int? DepartmanId { get; set; }
        public Departman? Departman { get; set; }

        public ICollection<Harcama> Harcamalar { get; set; } = new List<Harcama>();
    }
}