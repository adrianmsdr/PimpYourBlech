namespace PimpYourBlech_BlazorApp.Services.Images;
using Microsoft.AspNetCore.Components.Forms;

public interface IImageService
{
    // Getter für Vorschaubild - Pfad eines Fahrzeugs
    string GetCarImageUrl(int carId);
    
    // Speichern des Vorschaubilds eines Fahrzeugs
    Task<string> SaveCarImageAsync(int carId, IBrowserFile files);
    
    // Getter für Vorschaubild - Pfad eines Produkts
    String GetProductImageUrl(int productId);
    
    // Speichern des Vorschaubilds eines Produkts
    Task<string> SaveProductImageAsync(int productId, IBrowserFile file);
    
    // Speichern der 360 Grad Bilder einer Kombination aus Farbe + Felge
    Task Save360ImagesAsync(int carId, int colorId,int rimId, IReadOnlyList<IBrowserFile> files);

    // Löschen der 360 Grad Bilder einer Kombination aus Farbe + Felge
    bool Delete360Images(int carId, int colorId, int rimId);

    // Getter für 360 Grad Bilder einer Kombination aus Farbe + Felge
    List<string> Get360ImageUrls(int carId, int colorId, int rimId);
    
    // Löschen aller Bilder für ein Fahrzeug
    bool DeleteCarImages(int carId);
    
    // Löschen des Bildes eines Produkts
    bool DeleteProductImage(int productId);

    // Getter für Liste aller verfügbarer Profilbild - Pfade
    public List<string> GetCustomerImageUrl();

    // Löschen aller Bilder
    bool DeleteAllImages();
}
