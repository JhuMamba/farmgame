Projenin Çalıştırılması

Proje direkt oyuna girer ve kullanıcıdan oyunun devam ettirilebilmesi için üretim alanı koyulmasını ister.
Koyulan üretim alanı veya alanlarına ekim işlemi için öncelikle tohum alınması gerekir.
Alınan tohum - tohumlar ekranın üst tarafında bulunan envanter panelinde anlık görüntülenmektedir.
Kullanıcı ekranın sol tarafında bulunan panel yardımı ile gerekli araçları görebilmektedir.
Araçlar yardımı ile alınan tohumlar üretim alanlarına ekilir ve büyüme süreci başlar.
Yeterince zaman geçtikten sonra ürünler yine araç yardımı ile hasat edilebilmektedir.
Hasat edilen ürün envanter üzerinden kontrol edilebilmektedir.
Döngü bu şekilde devam eder.


Kullanılan Teknolojiler



1. Oyun Motoru & Grafik

Unity 3D: Oyun motoru olarak kullanılıyor.

URP: URP genel platform desteği ve shader optimizasyonu sebebi ile tercih edilmiştir.

2D/3D Grafikler: Oyun 3D odaklı ama 2D sprite tabanlı UI ve efektler kullanılabilir.

Shader Graph / Custom Shaders: Hayalet bina efekti sağlamak için özel shader kullanımı.


3. Gameplay Sistemleri

Grid Tabanlı Özel Yerleştirme Sistemi:

Yapılar belirli bir grid boyutuna göre yerleştirilir.

Bina döndürme ve yerleştirme doğrulama sistemleri vardır.

Veri akış hızı ve kolaylığı sebebi ile HashSet kullanılmıştır.


Çeşitli Bina Tipleri:

Ana bina sınıfından türetilmiş üç adet çocuk sınıf bulunmaktadır.

Bu sınıflar kaynak üretebilen, depo ve boş şeklindedir.

Boş ve depo binaları sisteme dahil edilmiş fakat oyuna dahil edilmemiştir.


Kaynak Yönetimi & Üretim:

Kaynak üretebilen binalar büyüme süresine bağlı olarak kaynak üretebilir.

Kaynak üretebilen binalar kaynak tipine göre modüler bir şekilde her kaynağı üretebilir.

Kaynaklar üç ana süreçten geçer ve bunlar oyun içerisinde gözlemlenebilir. (Ekim, Büyüme, Hasat)

Kaynak üretimi UTC' ye bağlı olarak üretim yapmaktadır. Backend kolaylığı düşünülmüştür.

Tasarlanan modüler item ve kaynak sistemleri ile istenilen kaynak ve item anında implemente edilebilmektedir.


Araç Kullanımı:

Ekin ekmeye ve biçmeye yarayan araçlar implemente edilmiştir.

Baz araç default olarak belirtilmiştir.


Market ve Envanter Sistemleri:

Oyuncunun anlık sahip olduğu eşyalar envanter kısmında görüntülenmektedir.

Slota dayalı bu sistem sayesinde basit ama modüler bir yapı elde edilmiştir.

Bina ve tohum alımı için basit bir market sistemi oyuna dahil edilmiştir.

Çevre Tasarımı:
Ücretsiz bir model paketinden [1] faydalanılarak basit bir seviye tasarlanmıştır.
Seviye istenilen boyutta büyütülerek içi modellerle doldurulabilir.

KAYNAK

[1] https://crisdias.itch.io/farm-asset-pack

ÖNİZLEME

![Screenshot 2025-03-01 025739](https://github.com/user-attachments/assets/bd7ed43b-03c3-4dd8-bd21-d30b99ff5c23)
![Screenshot 2025-03-01 025826](https://github.com/user-attachments/assets/b9fa5ccd-562a-4c35-98d4-292699d90943)
![Screenshot 2025-03-01 025841](https://github.com/user-attachments/assets/98bf1eec-a48c-475a-8e83-c73a6168d561)
![Screenshot 2025-03-01 030122](https://github.com/user-attachments/assets/3a648c76-d4f7-4ac8-9573-cb5823ee53d2)

