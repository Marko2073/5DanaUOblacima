# 5 dana u oblacima

## Opis
API za upravljanje igračima, timovima i mečevima, sa izračunavanjem ELO rejtinga.

## Endpoints

### Players
- **`POST /players/create`** - Kreira novog igrača.
- **`GET /players`** - Vraća sve igrače.
- **`GET /players/{id}`** - Vraća igrača po ID-ju.

### Teams
- **`POST /teams`** - Kreira novi tim.
- **`GET /teams`** - Vraća sve timove.
- **`GET /teams/{id}`** - Vraća tim po ID-ju.

### Matches
- **`POST /matches`** - Kreira novi meč.
- **`GET /matches`** - Vraća sve mečeve.

## Tehnologije
- **ASP.NET Core**
- **Entity Framework Core** (InMemory baza)
- **xUnit** za testiranje

## Kako pokrenuti

### Pokretanje API-ja
Da biste pokrenuli API, otvorite terminal u root folderu projekta i pokrenite sledeće komande:
```bash
dotnet restore    # Instalira sve potrebne zavisnosti
dotnet run        # Pokreće API (kada se postavite sa `cd` na API projekat)
```
### Pokretanje Testova
Da biste pokrenuli testove, otvorite terminal u root folderu projekta i pokrenite sledeće komande:
```bash
cd 5DanaUOblacima.Tests     # Uđite u folder sa testovima
dotnet test                 # Pokreće testove
```
