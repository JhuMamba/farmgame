Proje Hakkında

Bu proje, kullanıcıların üretim alanları kurarak tarımsal ürünler yetiştirmesini ve kaynak yönetimini içeren bir oyun geliştirme çalışmasıdır.

🚀 Projenin Çalıştırılması

Oyun açıldığında doğrudan üretim alanı yerleştirme talebi ile başlar.

Üretime başlamak için öncelikle tohum satın alınmalıdır.

Alınan tohumlar, ekranın üst tarafındaki envanter panelinde görüntülenir.

Ekranın sol tarafında bulunan panel aracılığıyla gerekli araçlara erişilebilir.

Araçlar kullanılarak tohum ekilir ve büyüme süreci başlar.

Yeterli zaman geçtikten sonra ürünler hasat edilebilir.

Hasat edilen ürünler envanterde görüntülenebilir.

Bu süreç döngüsel olarak devam eder.

🛠 Kullanılan Teknolojiler

🎮 Oyun Motoru & Grafik

Unity 3D: Oyun motoru olarak kullanıldı.

URP (Universal Render Pipeline): Platform desteği ve shader optimizasyonu için tercih edildi.

2D/3D Grafikler: Oyun 3D odaklı olup, UI ve efektlerde 2D sprite'lar kullanılabilir.

Shader Graph / Custom Shaders: Hayalet bina efekti ve diğer görsel efektler için özel shader'lar geliştirildi.

🎮 Gameplay Sistemleri

🏗️ Grid Tabanlı Yerleştirme Sistemi

Yapılar belirli bir grid sistemine göre yerleştirilir.

Bina döndürme ve yerleştirme doğrulama mekanizmaları bulunur.

Veri akışı optimizasyonu için HashSet kullanılmıştır.

🏢 Bina Türleri

Ana bina sınıfından türetilmiş üç temel bina türü bulunmaktadır:

Kaynak üreten binalar

Depo binaları

Boş binalar (Henüz oyunda kullanılmıyor, ancak sisteme dahil edilmiştir.)

🌾 Kaynak Yönetimi & Üretim

Kaynak üreten binalar, büyüme süresine bağlı olarak üretim yapar.

Kaynak üretim sistemi modülerdir, her bina farklı kaynak türlerini üretebilir.

Kaynaklar üç aşamadan geçer: Ekim → Büyüme → Hasat

Üretim süreci UTC tabanlıdır, böylece backend ile kolay entegrasyon sağlanır.

Modüler item ve kaynak sistemi sayesinde yeni içerikler hızlıca eklenebilir.

🚜 Araç Kullanımı

Ekin ekme ve hasat etme işlemleri için araçlar geliştirilmiştir.

Varsayılan bir temel araç bulunmaktadır, farklı araç türleri eklenebilir.

🛒 Market & Envanter Sistemleri

Oyuncunun sahip olduğu tüm eşyalar envanter panelinde görüntülenir.

Slot tabanlı modüler envanter sistemi sayesinde esnek bir yapı oluşturulmuştur.

Bina ve tohum satın almak için basit bir market sistemi mevcuttur.

🌍 Çevre Tasarımı

Ücretsiz model paketleri kullanılarak basit bir seviye tasarımı oluşturulmuştur.

Seviye genişletilebilir ve içerisine yeni modeller eklenebilir.

📸 Önizleme

![Screenshot 2025-03-01 025739](https://github.com/user-attachments/assets/bd7ed43b-03c3-4dd8-bd21-d30b99ff5c23)
![Screenshot 2025-03-01 025826](https://github.com/user-attachments/assets/b9fa5ccd-562a-4c35-98d4-292699d90943)
![Screenshot 2025-03-01 025841](https://github.com/user-attachments/assets/98bf1eec-a48c-475a-8e83-c73a6168d561)
![Screenshot 2025-03-01 030122](https://github.com/user-attachments/assets/3a648c76-d4f7-4ac8-9573-cb5823ee53d2)

