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

### U napred potrebno:

- .NET Core 10.0
- Angular 22.0.8
- MongoDB

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

