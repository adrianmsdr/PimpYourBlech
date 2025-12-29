using Microsoft.AspNetCore.Components.Forms;
namespace PimpYourBlech_BlazorApp.Services;

// Implementierung des Image Service
// Dieser Service ist nur in der Blazor-App, nicht in der Class Library
// Er arbeitet direkt mit dem Dateisystem (wwwroot-Verzeichnis)
// wwwroot ist der Ort, aus dem die Web-App statische Dateien wie z.B. Bilder ausliefert

public class ImageService: IImageService
{


        // IWebHostEnvironment enthält Informationen über die laufende Web-App,
        // insbesondere den Pfad zu wwwroot (Wo unsere Bilder liegen sollen)
        // -> IWebHostEnvironment sagt uns, wo auf der Festplatte die Bilder und so liegen
        private readonly IWebHostEnvironment _env;

      
        // Konstruktor bekommt das IWebHostEnvironment über Dependency Injection
        // Dadurch müssen wir den wwwroot - Pfad nicht selbst suchen, sondern bekommen ihn geliefert
        public ImageService(IWebHostEnvironment env)
        {
            _env = env;
        }
        
        // Liest aus dem Ordner "wwwroot/CarImages/{carId}" alle Dateien aus
        // Dieser Ordner enthält die Bilder für ein bestimmtes Auto
        public string GetCarImageUrl(int carId)
        {
            // System-Pfad zum Ordner des Autos bauen
            // Beispiel:
            // _env.WebRootPath ="/.../wwwroot"
            // Path.Combine(…, "CarImages", car.Id (z.B. 1)) = "/.../wwwroot/CarImages/1"
            var folder = Path.Combine(_env.WebRootPath, "CarImages", carId.ToString(), "PresentationImage");

            // Wenn der Ordner nicht existiert, geben wir einfach eine leere Liste zurück
            if (!Directory.Exists(folder))
                return "/no-img.png";
            
            //erste Bilddatei holen
            var file = Directory
                .GetFiles(folder)
                .OrderBy(f => f)
                .FirstOrDefault();
            
            if(file == null)
                return "/no-img.png";
            
            //Zu Web-URL Umwandeln
            return $"/CarImages/{carId}/PresentationImage/{Path.GetFileName(file)}";
        }

        
        // Speichert mehrere hochgeladene Bilder für ein Auto in genau den Ordner,
        // aus dem später die App lesen kann
    
        // Wichtig: Diese Methode arbeitet mit IBrowserFile
        // Das ist die Datenstruktur, die Blazor verwendet,
        // wenn ein Benutzer im Browser ein Bild hochlädt
        public async Task<string> SaveCarImageAsync(int carId, IBrowserFile file)
        {
            // vollständigen System-Pfad zum Autoordner bauen
            var folder = Path.Combine(_env.WebRootPath, "CarImages", carId.ToString(), "PresentationImage");

            // Ordner anlegen, falls er noch nicht existiert
            Directory.CreateDirectory(folder);
            
            //Dateiendung des Uploads Übernehmen (.webp)
            var extension = Path.GetExtension(file.Name);
            
            // Fester Dateiname 
            var fileName = "Car" + extension;

            // Voller System-Pfad
            var fullPath = Path.Combine(folder, fileName);

            // Datei speichern
            await using var fs = new FileStream(fullPath, FileMode.Create);
            await file.OpenReadStream(maxAllowedSize: 10 * 1024 * 1024).CopyToAsync(fs);

            // Web-URL zurückgeben
            var relativePath = Path.Combine("CarImages", carId.ToString(),"PresentationImage", fileName)
                .Replace("\\", "/");

            return "/" + relativePath;
        }

        public async Task<string> SaveColorImagesAsync(int carId, int productId, IReadOnlyList<IBrowserFile> files)
        { 
            // vollständigen System-Pfad zum Autoordner + Farbe bauen
            var folder = Path.Combine(_env.WebRootPath, "CarImages", carId.ToString(), productId.ToString());

            // Ordner anlegen, falls er noch nicht existiert
            Directory.CreateDirectory(folder);

            // Dateinamen fortlaufend generieren: frame_00, frame_01 ...
            int index = 0;

            foreach (var file in files)
            {
                // Dateiendung des Originals übernehmen ( .webp bei mir grad)
                var extension = Path.GetExtension(file.Name);

                // Der neue Dateiname:
                // frame_00.webp, frame_01.webp ...
                var fileName = $"frame_{index:D2}{extension}";

                // Zielpfad auf der Festplatte
                var fullPath = Path.Combine(folder, fileName);

                // Datei in den Zielordner kopieren
                
                // FileStream = Klasse in .NET die eine Verbindung zu
                // einer Datei auf der Festplatte darstellt (erlaubt anlegen, schreiben, lesen)
                // using -> damit Stream sicher geschlossen wird, auch wenns kracht
                // await -> damit nichts blockiert
                await using var fs = new FileStream(fullPath, FileMode.Create);

                // IBrowserFile.OpenReadStream öffnet den Upload-Stream
                // maxAllowedSize ist ein Schutz gegen zu große Dateien
                await file.OpenReadStream(maxAllowedSize: 10 * 1024 * 1024).CopyToAsync(fs);

                index++;
            }

            // Wir geben einen relativen Pfad zurück:
            // "images/cars/{carId}"
            
            // Dieser kann später in der Datenbank gespeichert werden,
            // falls man wissen will, wo die Bilder liegen
            
            var relativeFolder = Path.Combine("CarImages", carId.ToString(), productId.ToString())
                .Replace("\\", "/"); // wichtig für Windows

            return relativeFolder;
        }
        public string GetProductImageUrl(int productId)
        {
            // Ordner für das Produkt
            var folder = Path.Combine(_env.WebRootPath, "ProductImages", productId.ToString());

            if (!Directory.Exists(folder))
                return "/no-image.png";

            // erste Bilddatei holen
            var file = Directory
                .GetFiles(folder)
                .OrderBy(f => f)
                .FirstOrDefault();

            if (file == null)
                return "/no-image.png"; 

            // Zu Web-URL umwandeln
            return $"/ProductImages/{productId}/{Path.GetFileName(file)}";
        }

        public async Task<string> SaveProductImagesAsync(int productId, IBrowserFile file)
        {
            // Zielordner für das Produkt
            var folder = Path.Combine(_env.WebRootPath, "ProductImages", productId.ToString());
            Directory.CreateDirectory(folder);

            // Dateiendung des Uploads übernehmen (.webp)
            var extension = Path.GetExtension(file.Name);

            // Fester Dateiname 
            var fileName = "Product" + extension;

            // Voller System-Pfad
            var fullPath = Path.Combine(folder, fileName);

            // Datei speichern
            await using var fs = new FileStream(fullPath, FileMode.Create);
            await file.OpenReadStream(maxAllowedSize: 10 * 1024 * 1024).CopyToAsync(fs);

            // Web-URL zurückgeben
            var relativePath = Path.Combine("ProductImages", productId.ToString(), fileName)
                .Replace("\\", "/");

            return "/" + relativePath;
        }

        public async Task DeleteCarImagesAsync(int productId, int carId)
        {
            // Ordner, in dem die Bilder dieses Autos liegen
            var folder = Path.Combine(_env.WebRootPath, "CarImages", carId.ToString());

            if (!Directory.Exists(folder))
                return; // nichts zu tun

            try
            {
                // true = rekursiv, falls Unterordner vorhanden
                Directory.Delete(folder, recursive: true);
            }
            catch (Exception ex)
            {
                // je nach Anspruch:
                // - hier nur loggen und weitermachen
                // - oder Fehler nach oben durchreichen
            }
        }
        public async Task DeleteProductImagesAsync(int ProductId)
        {
            // Ordner, in dem die Bilder dieses Autos liegen
            var folder = Path.Combine(_env.WebRootPath, "ProductImages", ProductId.ToString());

            if (!Directory.Exists(folder))
                return; // nichts zu tun

            try
            {
                // true = rekursiv, falls Unterordner vorhanden
                Directory.Delete(folder, recursive: true);
            }
            catch (Exception ex)
            {
                // je nach Anspruch:
                // - hier nur loggen und weitermachen
                // - oder Fehler nach oben durchreichen
            }
        }

        public List<string> GetColorFrameUrls(int carId,int ProductId)
        {
            // System-Pfad zum Ordner des Autos bauen
            // Beispiel:
            // _env.WebRootPath ="/.../wwwroot"
            // Path.Combine(…, "CarImages", car.Id (z.B. 1), product.ProductId (z.B. 1)) = "/.../wwwroot/CarImages/1/1"
            var folder = Path.Combine(_env.WebRootPath, "CarImages", carId.ToString(), ProductId.ToString());

            // Wenn der Ordner nicht existiert, geben wir einfach eine leere Liste zurück
            if (!Directory.Exists(folder))
                return new List<string>();

            // Alle Dateien im Ordner lesen, alphabetisch sortieren und in Web-URLs umwandeln
            // Beispiel: "/.../wwwroot/CarImages/1/frame_00.webp"
            // wird zur Web-URL: "/CarImages/1/frame_00.webp"
            return Directory.GetFiles(folder)
                .OrderBy(f => f) // sorgt dafür, dass frame_00 vor frame_01 kommt
                .Select(f => $"/CarImages/{carId}/{ProductId}/{Path.GetFileName(f)}")
                .ToList();
        }
        
        public List<string> GetCustomerImageUrl()
        {

            // System-Pfad zum Ordner des Autos bauen
            // Beispiel:
            // _env.WebRootPath ="/.../wwwroot"
            // Path.Combine(…, "CarImages", car.Id (z.B. 1), product.ProductId (z.B. 1)) = "/.../wwwroot/CarImages/1/1"
            var folder = Path.Combine(_env.WebRootPath, "CustomerImages");

            // Wenn der Ordner nicht existiert, geben wir einfach eine leere Liste zurück
            if (!Directory.Exists(folder))
                return new List<string>();

            // Alle Dateien im Ordner lesen, alphabetisch sortieren und in Web-URLs umwandeln
            // Beispiel: "/.../wwwroot/CarImages/1/frame_00.webp"
            // wird zur Web-URL: "/CarImages/1/frame_00.webp"
            return Directory.GetFiles(folder)
                .OrderBy(f => f) // sorgt dafür, dass frame_00 vor frame_01 kommt
                .Select(f => $"/CustomerImages/{Path.GetFileName(f)}")
                .ToList();
        }

}