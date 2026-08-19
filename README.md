# 💼 Kurumsal Gider ve Masraf Takip Sistemi

Modern işletmeler için geliştirilmiş; çalışanların harcama taleplerini fiş/fatura belgeleriyle sisteme iletebildiği, yöneticilerin bu talepleri onay/red mekanizmasıyla inceleyebildiği ve sistem yöneticilerinin kullanıcı ile departmanları yönetebildiği kapsamlı bir kurumsal web uygulamasıdır.

---

## 🛠️ Kullanılan Teknolojiler

* **Backend:** ASP.NET Core MVC (.NET 8)
* **Veritabanı & ORM:** Microsoft SQL Server, Entity Framework Core (Code-First)
* **Kimlik Doğrulama & Yetkilendirme:** Cookie Authentication, Rol Bazlı Yetkilendirme (RBAC)
* **Frontend:** Bootstrap 5, Sneat Admin Template, HTML5, CSS3, JavaScript
* **Raporlama:** CSV / Excel Veri Dışa Aktarımı

---

## ✨ Temel Özellikler

### 1. Rol Bazlı Erişim ve Güvenlik (RBAC)
* **Çalışan (Employee):** Yalnızca kendi harcamalarını listeleme, harcama fişi/belgesi yükleyerek yeni talep oluşturma.
* **Yönetici (Manager):** Onay bekleyen harcamaları inceleme, gerekçe belirterek onaylama veya reddetme; harcama raporlarını filtreleme ve Excel olarak dışa aktarma.
* **Sistem Yöneticisi (Admin):** Tüm yönetici yetkilerine ek olarak yeni kullanıcı tanımlama, rol atama, departman ve kategori/limit parametrelerini yönetme.

### 2. Harcama & Belge Yönetimi
* Masraf girişi esnasında PDF, PNG ve JPEG formatlarında fiş/fatura yükleme.
* Kategori bazlı harcama üst limiti (`MaxLimit`) kontrolü (Limit aşımında sistem tarafından otomatik uyarı mekanizması).
* Durum rozetleri ile canlı takip (*Onay Bekliyor*, *Onaylandı*, *Reddedildi*).

### 3. Dinamik Dashboard & KPI Analitiği
* Giriş yapan kullanıcının rolüne göre özelleştirilen istatistik paneli.
* Toplam harcama tutarları, onaylanan masraflar ve bekleyen talep adetlerinin anlık hesaplanması.

### 4. Gelişmiş Filtreleme & Raporlama
* Harcamaları tarih aralığı, departman, kategori ve duruma göre filtreleme.
* UTF-8 Türkçe karakter destekli CSV/Excel formatında rapor çıktısı alma.

---

## 📸 Ekran Görüntüleri

*(Ekran görüntülerinizi bu alana ekleyebilirsiniz)*

---

## 🚀 Kurulum ve Çalıştırma

Projeyi yerel ortamınızda çalıştırmak için aşağıdaki adımları izleyin:

### 1. Projeyi Klonlayın
```bash
git clone [https://github.com/kullaniciadi/KurumsalGiderTakipSistemi.git](https://github.com/kullaniciadi/KurumsalGiderTakipSistemi.git)
cd KurumsalGiderTakipSistemi
