# imigram
[![.NET](https://img.shields.io/badge/.NET-10.0-512BD4?logo=dotnet&logoColor=white)](https://dotnet.microsoft.com/)
[![C#](https://img.shields.io/badge/C%23-13-239120?logo=csharp&logoColor=white)](https://learn.microsoft.com/dotnet/csharp/)
[![Angular](https://img.shields.io/badge/Angular-22-DD0031?logo=angular&logoColor=white)](https://angular.dev/)
[![MongoDB](https://img.shields.io/badge/MongoDB-47A248?logo=mongodb&logoColor=white)](https://www.mongodb.com/)
[![SignalR](https://img.shields.io/badge/SignalR-512BD4?logo=dotnet&logoColor=white)](https://dotnet.microsoft.com/apps/aspnet/signalr)

Seminarski rad iz predmeta Web Programiranje 2
## Opis projekta

Web aplikacija koja predstavlja društvenu mrežu i omogućava korisnicima da se povezuju, dele objave i komuniciraju sa drugim korisnicima.

Funkcionalnosti:
- Login/Registracija
- Kreiranja objava
- Prikaz objava
- Pretraga profila drugih korisnika
- Praćenje drugih korisnika
- Slanje poruka
- Obaveštenja
- Lajkovanje, komentarisanje objava
- Admin interfejs

## Tehnologije

- **Backend:** ASP .NET Core
- **Frontend:** Angular
- **Baza podataka:** MongoDB
- **Real-time obaveštenja i poruke:** SignalR
- **Autentifikacija:** JWT

## Arhitektura

![Dijagram Arhitekture](assets/images/WebArhitektura2.svg)

## Strukutra projekta

```text
Imigram/
├── backend/
│   └── ImigramAPI/
│       ├── Controllers/
│       ├── Database/
│       ├── DTOs/
│       ├── Hubs/
│       ├── Interfaces/
│       ├── Models/
│       ├── Properties/
│       ├── Repositories/
│       │   └── Interfaces/
│       ├── Services/
│       │   └── Interfaces/
│       └── wwwroot/
│           ├── images/
│           │   └── users/
│           └── uploads/
│
├── frontend/
│   ├── public/
│   │   └── sounds/
│   └── src/
│       ├── app/
│       │   ├── core/
│       │   │   ├── guards/
│       │   │   ├── interceptors/
│       │   │   └── services/
│       │   │
│       │   ├── features/
│       │   │   ├── auth/
│       │   │   │   ├── login/
│       │   │   │   └── register/
│       │   │   ├── models/
│       │   │   ├── pages/
│       │   │   │   ├── admin-panel/
│       │   │   │   ├── create-post/
│       │   │   │   ├── followers-page/
│       │   │   │   ├── home/
│       │   │   │   ├── inbox-page/
│       │   │   │   ├── post-details/
│       │   │   │   └── profile/
│       │   │   └── shared/
│       │   │
│       │   └── shared/
│       │       ├── navbar/
│       │       ├── post-card/
│       │       ├── toast-action/
│       │       └── toast-notification/
│       │
│       └── assets/
│
└── README.md
```

## Pokretanje

### Unapred potrebno:

- .NET Core 10.0
- Angular 22.0.8
- MongoDB

### Konfiguracija:

Pre pokretanja backend-a neophodno je podesiti `appsetting.json` fajl sa odgovarajućim MongoDB konekcionim stringom i JWT podešavanjima
```json
{
  "Jwt": {
    "Key": "UNESITE_VAŠ_KLJUČ",
    "Issuer": "ImigramAPI",
    "Audience": "ImigramClient",
    "ExpireMinutes": 120
  },
  "MongoDb": {
    "ConnectionString": "UNESITE_VAŠ_KONEKCIONI_STRING",
    "Database": "ImigramDB"
  },
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  },
  "AllowedHosts": "*"
}
```

### Pokretanje backenda:
```shell
cd imigram/backend/ImigramAPI
dotnet restore
dotnet run
```

### Pokretanje frontenda:
```shell
cd imigram/frontend
npm install
ng serve
```

### Dostupnost aplikacije

Backend će raditi na: 
- https://localhost:7109
- http://localhost:5233 
 

Frontend će raditi na:
- http://localhost:4200/

## API Dokumentacija
Backend api je implementiran korišćenjem ASP .NET Core Web API-ja.

API koristi JWT auth za zaštićene endpoint-e.

`https://localhost:7109/swagger/index.html`

### Authentication
| Method | Endpoint | Opis | Auth |
|--------|----------|------|------|
| POST   | `api/Auth/register`| Registracija korisnika | Ne |
| POST   | `api/Auth/login`   | Prijava korisnika | Ne |

### Users
| Method | Endpoint | Opis | Auth |
|--------|----------|------|------|
| GET   | `api/User/{userId}`| Dohvatanje korisnika po ID | Da |
| GET   | `api/User/search`| Pretraga korisnika po query-u | Da |
| PUT   | `api/User/update/profile-image`| Ažuriranje profile slike korisnika | Da |

### Posts
| Method | Endpoint | Opis | Auth |
|--------|----------|------|------|
| POST   | `api/Post`| Kreiranje nove objave | Da |
| GET   | `api/Post`| Dohvatanje svih objava | Da |
| GET   | `api/Post/{id}`| Dohvatanje objave po ID | Da |
| GET   | `api/Post/all/{userId}`| Dohvatanje svih objava jednog korisnika po ID korisnika | Da |
| GET   | `api/Post/feed`| Dohvatanje svih objava za feed korisnika | Da |
| DELETE   | `api/Post/{postId}`| Brisanje objave po ID | Da |

### Follow
| Method | Endpoint | Opis | Auth |
|--------|----------|------|------|
| POST   | `api/Follow/send_request`| Slanje zahteva za praćenje | Da |
| POST   | `api/Follow/accept_request/{requestId}`| Prihvatanje zahteva za pracenje | Da |
| POST   | `api/Follow/decline_request/{requestId}`| Odbijanje zahteva za pracenje | Da |
| GET   | `api/Follow/followings/{userId}`| Dohvatanje svih korisnika koje korisnik `userId` prati | Da |
| GET   | `api/Follow/followers/{userId}`| Dohvatanje svih pratilaca korisnika `userId` | Da |
| GET   | `api/Follow/follow_request`| Dohvatanje svih zahteva za praćenje prijavljenog korisnika | Da |
| POST   | `api/Follow/follow_status/{userId}`| Dohvatanje statusa pracenja izmedju prijavljenog korisnika i korisnika `userId` | Da |


### Admin
| Method | Endpoint | Opis | Auth |
|--------|----------|------|------|
| POST   | `api/Admin/ban/{userId}`| Banovanje korisnika `userId` | Da |
| POST   | `api/Admin/unban/{userId}`| Uklanjanje ban-a korisnika `userId` | Da |
| GET   | `api/Admin/`| Dohvatanje svih banovanih korisnika | Da |
| DELETE   | `api/Admin/{postId}`| Brisanje objave `postId` | Da |

### Chat
| Method | Endpoint | Opis | Auth |
|--------|----------|------|------|
| GET   | `api/Chat`| Dohvatanje svih chat-ova prijavljenog korisnika | Da |
| GET   | `api/Chat/{chatId}`| Dohvatanje chat-a `chatId` | Da |
| GET   | `api/Chat/with/{userId}`| Dohvatanje chat-a izmedju prijavljenog korisnika i korisnika `userId` | Da |

### Comment
| Method | Endpoint | Opis | Auth |
|--------|----------|------|------|
| POST   | `api/Comment`| Kreiranje novog komentara | Da |
| GET   | `api/Comment/{commentId}`| Dohvatanje komentara `commentId` | Da |
| GET   | `api/Comment/all/{postId}`| Dohvatanje svih komentara na objavi `postId` | Da |

### Like
| Method | Endpoint | Opis | Auth |
|--------|----------|------|------|
| POST   | `api/Like`| Kreiranje novog like-a | Da |
| GET   | `api/Like/{postId}`| Dohvatanje svih like-ova za objavu `postId`| Da |
| GET   | `api/Like/check-like/{postId}`| Provera da li je prijavljen korisnik like-ova objavu `postId` | Da |
| DELETE   | `api/Like/{postId}`| Brisanje like-a prijavljenog korisnika na objavi `postId` | Da |

### Message 
| Method | Endpoint | Opis | Auth |
|--------|----------|------|------|
| GET   | `api/Message/{chatId}`| Dohvatanje svih poruka iz chat-a `chatId` | Da |
| PUT   | `api/Message/{messageId}/read`| Obelezavanje poruke `messageId` kao procitane | Da |

### Notification
| Method | Endpoint | Opis | Auth |
|--------|----------|------|------|
| POST   | `api/Notification/read/{notificationId}`| Obelezavanje obavestenja `notificationId` kao procitano | Da |
| GET   | `api/Notification`| Dohvatanje svih obavestenja iz za prijavljenog korisnika | Da |


### Report
| Method | Endpoint | Opis | Auth |
|--------|----------|------|------|
| POST   | `api/Report`| Kreiranje prijave za objavu | Da |
| POST   | `api/Report/get/all`| Dohvatanje svih prijava | Da |
| GET   | `api/Report/get/all/{status}`| Dohvatanje svih prijava za objavu po statusu `status` | Da |
| GET   | `api/Report/{id}`| Dohvatanje prijave `id` | Da |

