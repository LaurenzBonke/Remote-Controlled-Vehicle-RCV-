# Remote Controlled Vehicle (RCV) – pigames API

ASP.NET Core 10 Web API mit MySQL (Entity Framework Core) für Teams, Mitglieder und Fahrzeuge.

## Starten

Die Zugangsdaten der Datenbank stehen **nicht** im Repo. Lokal werden sie einmalig als User Secret hinterlegt:

```bash
cd pigames
dotnet user-secrets set "ConnectionStrings:DefaultConnection" "Server=<host>;Port=3306;Database=pigamesos;Uid=<benutzer>;Pwd=<passwort>;"
dotnet run
```

Auf einem Server stattdessen die Umgebungsvariable `ConnectionStrings__DefaultConnection` setzen.

API-Doku (nur Development): `http://localhost:5245/scalar/v1`
