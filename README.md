# CurrencyListing
Kur xml servisinden verileri çekip filtreleme, sıralama seçenekleriyle listeleyen ve istenilen formatlarda export edebilen c# uygulamasıdır.  

## Özellikler
- Proje .NET Core 3.1 framework ile geliştirildi. 
- IIntegration interface'i ile Tcmb, europe xml servislerinden (seçenekler arttırılabilir) kur entegrasyon alt yapısı sağlandı.
- ICurrencyExport ile export edilmek istenen dosya sistemi ile çıktı alınabilir.
- Models.Constants.SourceConstants class'ı "internal" access modifiers ile farklı projelere erişimi kapatıldı.
- Helpers.CurrencyListingHelper.List metodu içerisinde Parse metodundan dönen tüm liste static değişkende Tutuldu. Her istek için Parse metodunu işlemediği için burada bir seviye performans sağlanmıştır.

## Kullanılan Kütüphaneler
- Unit testler için xUnit test kütüphanesi kullanıldı.
- CsvExport için "CsvHelper" kütüphanesi kullanıldı.
- Helpers.CurrencyListingHelper.List metodu içerisinde gelen parametreye göre dinamik OrderBy yapabilmek için, "System.Linq.Dynamic.Core" kütüphanesi kullanıldı.

## Eklenebilir Geliştirmeler
- Helpers.CurrencyListingHelper.List metodunda gelen parametrelere göre cache key oluşturulup dinamik bir cache yapısı kurularak performans iyileştirilebilir. 
- Kur xml entegrasyonu içerisine Hedef kur parametresi eklenerek listelenen tüm kurlar hedef kura çevrilerek listelenebilir.
- Unit testler dahada detaylandırılabilir.
