# 🚗 PimpYourBlech – Fahrzeugkonfigurator

PimpYourBlech ist eine webbasierte Anwendung zur individuellen Fahrzeugkonfiguration.  
Nutzerinnen und Nutzer können Fahrzeuge konfigurieren, Konfigurationen speichern, später fortsetzen und verschiedene Fahrzeuge oder Konfigurationen miteinander vergleichen.

Zusätzlich bietet die Anwendung einen integrierten Ersatzteilshop, eine FAQ- und Community-Sektion sowie einen Administrationsbereich zur Verwaltung der Systemdaten.

Die Anwendung ist rollenbasiert aufgebaut – bestimmte Funktionen stehen ausschließlich Administratoren zur Verfügung.

---

## ⚙️ Funktionen

### 🚘 Fahrzeugkonfigurator
- Auswahl und individuelle Konfiguration von Fahrzeugen
- Konfigurierbare Komponenten:
    - 🎨 Lack
    - 🛞 Felgen
    - ⚙️ Motor
    - ⭐ Optionale Zusatzprodukte (z. B. Scheinwerfer, Spoiler, etc.)
- Konfigurationen können gespeichert und später wieder geladen werden

---

### 🔍 Vergleichsfunktionen
- Vergleich von Standardfahrzeugen
- Vergleich von gespeicherten Konfigurationen
- Übersichtlicher Vergleich relevanter Fahrzeugdaten  
  (z. B. Preis, Leistung, Baujahr)

---

### 🔄 360°-Darstellung
- Fahrzeuge können mit interaktiven 360°-Bildern angezeigt werden
- Voraussetzung:
    - Das Fahrzeug besitzt mindestens eine Felge und einen Lack
- Die Bilder sind fahrzeug- und felgenspezifisch
- Für jede Fahrzeug-Felgen-Kombination können eigene Bilder hinterlegt werden

---

### 🛒 Ersatzteilshop
- Anzeige verfügbarer Ersatz- und Zusatzteile
- Teile können bestellt und verwaltet werden
- Zuordnung der Teile zu Fahrzeugen

---

### 💬 FAQ & Community
- Statische FAQ-Sektion
- Community-Bereich:
    - Nutzer können Fragen stellen
    - Andere Nutzer können Antworten posten

---

### 🔐 Administrationsbereich (nur für Admins)
- Verwaltung von:
    - 🚗 Fahrzeugen
    - 📦 Produkten / Ersatzteilen
    - 👤 Nutzern
    - 🧾 Bestellungen
    - 🖼️ Bildern (inkl. 360°-Darstellungen)
- Zugriff nur für Benutzer mit Administratorrechten

---

## 🧩 Voraussetzungen
- 🐳 Docker
- 🐳 Docker Compose  
  ➡️ Weitere manuelle Konfigurationsschritte sind nicht erforderlich.

---

## ▶️ Installation & Start

### 1) Container starten (BlazorApp)
Wechsle in das **BlazorApp-Projekt** und starte die Container:

docker compose up -d

### 2) Datenbank aufsetzen (Data)
Wechsle in das **Data-Projekt** und führe folgenden Befehl aus:

dotnet ef database update

### 👤 Zugangsdaten (Demo)
🔑 Administrator

Username: MusterAdmin

Passwort: pimpyourblech123!

👤 Standard

Username: MusterMax

Passwort: pimpyourblech123!

**FERTIG!**

Viel Pimp- und Fahrspaß wünscht dir dein Team **JungBrutalÜberfordert**

![PimpYourBlech](PimpYourBlech_BlazorApp/wwwroot/Logo.png)