using Microsoft.AspNetCore.Components.Forms;
namespace PimpYourBlech_BlazorApp.Services.Images;

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
          var folder = Path.Combine(_env.WebRootPath, "CarImages", carId.ToString(), "PresentationImage");
          if (!Directory.Exists(folder))
              return "/no-img.png";

          var file = Directory.EnumerateFiles(folder, "Car_*.*")
              .OrderByDescending(File.GetLastWriteTimeUtc)
              .FirstOrDefault();

          if (file is null)
              return "/no-img.png";

          return $"/CarImages/{carId}/PresentationImage/{Path.GetFileName(file)}";
        }

        
        // Speichert mehrere hochgeladene Bilder für ein Auto in genau den Ordner,
        // aus dem später die App lesen kann
    
        // Wichtig: Diese Methode arbeitet mit IBrowserFile
        // Das ist die Datenstruktur, die Blazor verwendet,
        // wenn ein Benutzer im Browser ein Bild hochlädt
        public async Task<string> SaveCarImageAsync(int carId, IBrowserFile file)
        {
          
          var folder = Path.Combine(_env.WebRootPath, "CarImages", carId.ToString(), "PresentationImage");
          Directory.CreateDirectory(folder);

          var extension = Path.GetExtension(file.Name);
          if (string.IsNullOrWhiteSpace(extension))
              extension = ".webp"; // fallback

          // 1) Alte "Car_*" Dateien löschen (egal welche Extension)
          foreach (var old in Directory.EnumerateFiles(folder, "Car*.*"))
          {
              try { File.Delete(old); } catch { /* optional loggen */ }
          }

          // 2) Neuer eindeutiger Name (Version)
          var version = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();
          var fileName = $"Car_{version}{extension}";
          var fullPath = Path.Combine(folder, fileName);

          // 3) Speichern
          await using var fs = new FileStream(fullPath, FileMode.Create);
          await file.OpenReadStream(maxAllowedSize: 10 * 1024 * 1024).CopyToAsync(fs);

          // 4) URL zurückgeben (neu -> kein Cache)
          return $"/CarImages/{carId}/PresentationImage/{fileName}";
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

        public async Task<string> SaveProductImageAsync(int productId, IBrowserFile file)
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

        public bool DeleteCarImages(int carId)
        {
            // Ordner, in dem die Bilder dieses Autos liegen
            var folder = Path.Combine(_env.WebRootPath, "CarImages", carId.ToString());

            if (!Directory.Exists(folder))
                return true; // nichts zu tun

            try
            {
                // true = rekursiv, falls Unterordner vorhanden
                Directory.Delete(folder, recursive: true);
                return true;
            }
            catch (Exception ex)
            {
                return false;
              
            }
        }
        public bool DeleteProductImage(int ProductId)
        {
            // Ordner, in dem die Bilder dieses Autos liegen
            var folder = Path.Combine(_env.WebRootPath, "ProductImages", ProductId.ToString());

            if (!Directory.Exists(folder))
                return true; // nichts zu tun

            try
            {
                // true = rekursiv, falls Unterordner vorhanden
                Directory.Delete(folder, recursive: true);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

       
        
        public bool Delete360Images(int carId, int colorId, int rimId)
        {
            // Ordner, in dem die Bilder dieses Autos liegen
            var folder = Path.Combine(_env.WebRootPath, "CarImages", carId.ToString(), "Combos", colorId.ToString(), rimId.ToString());

            if (!Directory.Exists(folder))
                return true; // nichts zu tun

            try
            {
                Directory.Delete(folder, recursive: true);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public List<string> Get360ImageUrls(int carId, int colorId, int rimId)
        {
            var folder = Path.Combine(_env.WebRootPath, "CarImages", carId.ToString(),"Combos", colorId.ToString(),rimId.ToString());

            // Wenn der Ordner nicht existiert, geben wir einfach eine leere Liste zurück
            if (!Directory.Exists(folder))
                return new List<string>();

            // Alle Dateien im Ordner lesen, alphabetisch sortieren und in Web-URLs umwandeln
            // Beispiel: "/.../wwwroot/CarImages/1/frame_00.webp"
            // wird zur Web-URL: "/CarImages/1/frame_00.webp"
            return Directory.GetFiles(folder)
                .OrderBy(f => f) // sorgt dafür, dass frame_00 vor frame_01 kommt
                .Select(f => $"/CarImages/{carId}/Combos/{colorId}/{rimId}/{Path.GetFileName(f)}")
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


        public async Task Save360ImagesAsync(int carId, int colorId, int rimId, IReadOnlyList<IBrowserFile> files)
        {
            
                const int MinFrames = 4;
                if (files is null || files.Count == 0)
                    throw new ArgumentException("Keine Dateien hochgeladen.", nameof(files));

                if (files.Count < MinFrames)
                    throw new InvalidOperationException(
                        $"Mindestens {MinFrames} Frames nötig, aktuell: {files.Count}.");

                var targetFolder = Path.Combine(_env.WebRootPath, "CarImages", carId.ToString(), "Combos",
                    colorId.ToString(), rimId.ToString());

                var tempFolder = targetFolder + "__tmp_" + Guid.NewGuid().ToString("N");
                // Temp sauber machen
                if (Directory.Exists(tempFolder))
                    Directory.Delete(tempFolder, recursive: true);

                try
                {
                Directory.CreateDirectory(tempFolder);


                // 3) Neu speichern (geordnet)
                int index = 0;
                foreach (var file in files)
                {
                    var ext = Path.GetExtension(file.Name);
                    if (string.IsNullOrWhiteSpace(ext)) ext = ".webp"; // optional fallback

                    var fileName = $"frame_{index:D2}{ext}";
                    var fullPath = Path.Combine(tempFolder, fileName);

                    await using var fs = new FileStream(fullPath, FileMode.Create);
                    await file.OpenReadStream(maxAllowedSize: 25 * 1024 * 1024).CopyToAsync(fs);

                    index++;
                }

                if (Directory.Exists(targetFolder))
                    Directory.Delete(targetFolder, recursive: true);

                Directory.Move(tempFolder, targetFolder);
            }
            catch
            {
                if (Directory.Exists(tempFolder))
                    Directory.Delete(tempFolder, recursive: true);

                throw;
            }
        }
        

        public bool DeleteAllImages()
        {
            try
            {
                DeleteFolderIfExists(Path.Combine(_env.WebRootPath, "CarImages"));
                DeleteFolderIfExists(Path.Combine(_env.WebRootPath, "ProductImages"));
                return true;
            }
            catch
            {
                return false;
            }

        }

        private static void DeleteFolderIfExists(string folder)
        {
            if (!Directory.Exists(folder))
                return;

            Directory.Delete(folder, recursive: true);
        }

}