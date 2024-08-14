# Döviz Takip Programı

Bu proje, döviz kurlarının takibi için Clean Architecture prensiplerine göre tasarlanmış bir web uygulamasıdır. Proje, ASP.NET Core ile backend tarafında, Angular ile frontend tarafında geliştirilmiştir ve sağlam, sürdürülebilir bir yapı sağlamak amacıyla katmanlı mimari kullanılmıştır.

## Proje Yapısı

### Backend (Sunucu) - DovizTakipServer

#### DovizTakipServer.Application
- **Application Katmanı**: Uygulamanın iş mantığını içerir.
  - **Behaviors**: Ortak davranışları tanımlar.
  - **Features**: Uygulamanın ana özelliklerini ve iş kurallarını içerir.
  - **Hubs**: SignalR ile gerçek zamanlı iletişimi sağlar.
  - **Mapping**: Nesne dönüştürme (mapping) işlemlerini içerir.
  - **Services**: Uygulama hizmetlerini tanımlar.

#### DovizTakipServer.Domain
- **Domain Katmanı**: Uygulamanın çekirdek iş mantığı ve kurallarını içerir.
  - **Entities**: Temel veri yapıları ve iş varlıkları.
  - **Enums**: Sabit değerler için kullanılan numaralandırmalar.
  - **Repositories**: Veriye erişim arayüzlerini tanımlar, ancak veri kaynağının nasıl erişildiğine dair detaylar içermez.

#### DovizTakipServer.Infrastructure
- **Infrastructure Katmanı**: Uygulamanın altyapı hizmetlerini sağlar.
  - **Configurations**: Veritabanı ve diğer altyapı bileşenleri için yapılandırmalar.
  - **Context**: Veritabanı bağlantısı ve işlemlerini yöneten yapı.
  - **Migrations**: Veritabanı şeması değişikliklerini yönetir.
  - **Repositories**: Domain katmanındaki repository arayüzlerinin somut implementasyonları.

#### DovizTakipServer.WebAPI
- **WebAPI Katmanı**: Uygulamanın dış dünya ile etkileşimde bulunduğu katmandır.
  - **Controllers**: HTTP isteklerini işler ve uygun yanıtları döner.
  - **Middlewares**: İsteklerin işlenmesi sırasında kullanılan ara yazılımlar.
  - **Services**: WebAPI ile ilgili özel hizmetleri tanımlar.

### Frontend (İstemci) - DovizTakipClient

- **Angular** ile geliştirilmiş kullanıcı arayüzü.
- **@microsoft/signalr**: SignalR ile gerçek zamanlı veri güncellemeleri sağlanır.
- **ngx-sweetalert2**: Kullanıcı etkileşimleri için kullanılır.
- JWT tabanlı kimlik doğrulama için **jwt-decode** kullanılır.

#### Kullanılan Kütüphaneler (Frontend):
- **Angular**: Uygulama çatısı.
- **SignalR**: Gerçek zamanlı veri güncellemeleri.
- **SweetAlert2**: Kullanıcıya gösterilen uyarılar ve bildirimler.
- **JWT-decode**: JWT (JSON Web Token) işlemleri.

