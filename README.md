# LogParser

Eine einfache ASP.NET Core Webanwendung zum Hochladen und Durchsuchen von Logdateien.

## Features
- Logdateien per Drag & Drop hochladen.
- Aufbereitung der Logeinträge und Zusammenbau von Texten aus Vorlagen und Parametern.
- Registrierbare Suchanbieter mit eigenen Parametern.
- Kombination der Suchanbieter per AND/OR.

## Entwicklung
```
dotnet restore
cd LogParser.Web
dotnet run
```

Die Anwendung steht anschließend unter `http://localhost:5261` (HTTP) bzw. `https://localhost:7192` (HTTPS) bereit.
