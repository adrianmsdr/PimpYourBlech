namespace PimpYourBlech_BlazorApp.Services;
using Microsoft.AspNetCore.Components.Forms;

public interface IImageService
{
    string GetCarImageUrl(int carId);
    
    Task<string> SaveCarImageAsync(int carId, IBrowserFile files);
    
    String GetProductImageUrl(int productId);
    
    
    Task<string> SaveProductImagesAsync(int productId, IBrowserFile file);
    
    Task Save360ImagesAsync(int carId, int colorId,int rimId, IReadOnlyList<IBrowserFile> files);

    Task Delete360ImagesAsync(int carId, int colorId, int rimId);

    List<string> Get360ImageUrls(int carId, int colorId, int rimId);
    
    Task DeleteCarImagesAsync(int carId);
    
    Task DeleteProductImagesAsync(int productId);

    public List<string> GetCustomerImageUrl();

    public void DeleteAllImages();
}
