# LibraryManagement

LibraryManagement, **ASP.NET Core Web API** ile geliştirilmiş, **Domain-Driven Design (DDD)** ve **Clean Architecture** prensiplerini temel alan bir kütüphane yönetim sistemi API projesidir.

Projede yazar, kategori, kitap, kitap kopyası, üye ve ödünç alma süreçleri uçtan uca ele alınmıştır. Kitap ödünç verme, iade etme ve süresi geçen ödünç kayıtlarının Hangfire ile otomatik olarak **Overdue** durumuna geçirilmesi gibi temel kütüphane senaryoları uygulanmıştır.

---

# Özellikler

- Domain-Driven Design (DDD)
- Clean Architecture
- CQRS + MediatR
- Entity Framework Core ile SQL Server
- FluentValidation
- Global Exception Handling
- Hangfire Background Jobs
- Docker Compose ile local development
- Author, Category, Book, BookCopy, Member ve Loan modülleri
- Loan oluşturma ve iade işlemleri
- Süresi geçen loan kayıtlarının otomatik olarak **Overdue** durumuna geçirilmesi

---

# Kullanılan Teknolojiler

- ASP.NET Core Web API
- C#
- .NET 9
- Entity Framework Core
- SQL Server
- MediatR
- FluentValidation
- Hangfire
- Docker
- Scalar / OpenAPI

---

# Proje Yapısı

```text
LibraryManagement
├── LibraryManagement.API
├── LibraryManagement.Application
├── LibraryManagement.Domain
├── LibraryManagement.Infrastructure
├── docker-compose.yml
└── LibraryManagement.sln
```

---

# Katmanlar

## Domain

Domain katmanı uygulamanın iş kurallarını içerir ve herhangi bir dış teknolojiye bağımlı değildir.

Başlıca entity'ler:

- Author
- Category
- Book
- BookCopy
- Member
- Loan

Bu katmanda ayrıca:

- Aggregate Root yapısı
- Entity taban sınıfları
- Value Object (Barcode)
- Domain Exception
- Enum yapıları
- Repository abstraction'ları
- Unit of Work abstraction'ı

bulunmaktadır.

İş kuralları servis sınıflarında değil, doğrudan entity davranışları içerisinde tanımlanmıştır.

Örnek davranışlar:

```text
BookCopy.Borrow()
BookCopy.Return()
BookCopy.MarkAsLost()
BookCopy.MarkAsDamaged()

Loan.Return()
Loan.MarkAsOverdue()
```

Bu sayede domain modeli **Rich Domain Model** yaklaşımına uygun olarak tasarlanmıştır.

---

## Application

Application katmanı uygulamanın use-case'lerini içerir ve CQRS yaklaşımı uygulanmıştır.

Bu katmanda:

- Command sınıfları
- Query sınıfları
- Handler sınıfları
- FluentValidation validator'ları
- Request / Response modelleri
- Repository abstraction'ları
- Unit of Work abstraction'ı
- Dependency Injection konfigürasyonu

bulunmaktadır.

Her iş senaryosu bağımsız bir Command veya Query üzerinden yönetilir.

Handler sınıfları doğrudan Entity Framework Core, SQL Server veya Hangfire gibi teknolojilere bağımlı değildir. Veri erişimi repository abstraction'ları üzerinden gerçekleştirilerek katmanlar arasındaki bağımlılık minimum seviyede tutulmuştur.

---

## Infrastructure

Infrastructure katmanı dış bağımlılıkların implementasyonlarını içerir.

Bu katmanda:

- LibraryDbContext
- Entity Framework Core Configuration'ları
- Repository implementasyonları
- Unit of Work implementasyonu
- Hangfire Background Job (LoanOverdueJob)
- Dependency Injection kayıtları

bulunmaktadır.

Ayrıca Hangfire kullanılarak süresi geçen aktif loan kayıtları belirli aralıklarla kontrol edilir ve otomatik olarak **Overdue** durumuna geçirilir.

---

## API

API katmanı uygulamanın dış dünyaya açılan katmanıdır.

Bu katmanda:

- REST Controller'lar
- OpenAPI / Scalar konfigürasyonu
- Global Exception Handler
- Hangfire Dashboard
- Dependency Injection başlangıç ayarları

bulunmaktadır.

Controller'lar yalnızca HTTP isteklerini karşılar ve ilgili Command veya Query nesnelerini MediatR aracılığıyla Application katmanına iletir.

---

# Domain-Driven Design

Projede **Domain-Driven Design (DDD)** yaklaşımı benimsenmiştir.

Öne çıkan DDD bileşenleri:

- Aggregate Root
- Rich Domain Model
- Entity
- Value Object
- Repository Pattern
- Unit of Work
- Domain Exception

İş kuralları servis katmanında değil, doğrudan domain entity'leri içerisinde tanımlanmıştır. Böylece domain modeli uygulamanın merkezinde yer almakta ve iş kuralları tek bir noktadan yönetilmektedir.

---

# Loan Süreci

Loan durumları:

```text
Active
Returned
Overdue
```

BookCopy durumları:

```text
Available
Borrowed
Lost
Damaged
```

Genel akış:

```text
1. Üye kitap ödünç alır.
2. Loan oluşturulur.
3. BookCopy Available durumundan Borrowed durumuna geçer.
4. Kitap iade edilirse Loan Returned olur.
5. BookCopy tekrar Available durumuna geçer.
6. Süresi geçen aktif Loan kayıtları Hangfire tarafından otomatik olarak Overdue durumuna geçirilir.
```

---

# Hangfire

Projede Hangfire, süresi geçen ödünç kayıtlarını otomatik olarak güncellemek amacıyla kullanılmıştır.

Her gün çalışan background job aşağıdaki işlemi gerçekleştirir:

```text
Loan (Active)
        │
DueDate < Today
        │
        ▼
Loan (Overdue)
```

Bu sayede kullanıcı herhangi bir işlem yapmasa bile sistem loan durumlarını otomatik olarak güncel tutar.

---

# Önemli Endpointler

## Authors

```http
POST   /api/authors
GET    /api/authors
GET    /api/authors/{id}
PUT    /api/authors/{id}
DELETE /api/authors/{id}
```

## Categories

```http
POST   /api/categories
GET    /api/categories
GET    /api/categories/{id}
PUT    /api/categories/{id}
DELETE /api/categories/{id}
```

## Books

```http
POST   /api/books
GET    /api/books
GET    /api/books/{id}
PUT    /api/books/{id}
DELETE /api/books/{id}
```

## Book Copies

```http
POST   /api/bookcopies
GET    /api/bookcopies
GET    /api/bookcopies/{id}
PUT    /api/bookcopies/{id}
DELETE /api/bookcopies/{id}
```

## Members

```http
POST   /api/members
GET    /api/members
GET    /api/members/{id}
PUT    /api/members/{id}
DELETE /api/members/{id}
```

## Loans

```http
POST /api/loans
GET  /api/loans
GET  /api/loans/{id}
POST /api/loans/{id}/return
```

---

# Docker ile Çalıştırma

Docker container'larını başlatmak için:

```bash
docker compose up -d
```

Container'ları durdurmak için:

```bash
docker compose down
```

---

# Kurulum

Repository'yi klonlayın:

```bash
git clone https://github.com/fskalkan/LibraryManagement.git
cd LibraryManagement
```

Bağımlılıkları yükleyin:

```bash
dotnet restore
```

Docker container'larını başlatın:

```bash
docker compose up -d
```

Projeyi build edin:

```bash
dotnet build
```

Migration uygulayın:

```bash
dotnet ef database update --project LibraryManagement.Infrastructure --startup-project LibraryManagement.API
```

API'yi çalıştırın:

```bash
dotnet run --project LibraryManagement.API
```

Development ortamında Scalar/OpenAPI arayüzüne erişebilirsiniz.

---

# Geliştirici

**Ferhat Samet Kalkan**