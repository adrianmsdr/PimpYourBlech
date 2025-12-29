namespace PimpYourBlech_BlazorApp.Services;
using Microsoft.AspNetCore.Components.Forms;

public interface IImageService
{
    string GetCarImageUrl(int carId);
    
    Task<string> SaveCarImageAsync(int carId, IBrowserFile files);
    
    String GetProductImageUrl(int productId);
    
    List<string> GetColorFrameUrls(int carId, int productId);
    
    Task<string> SaveProductImagesAsync(int productId, IBrowserFile file);

    Task<string> SaveColorImagesAsync(int carId, int productId, IReadOnlyList<IBrowserFile> files);
    Task DeleteCarImagesAsync(int productId, int carId);
    
    Task DeleteProductImagesAsync(int productId);

    public List<string> GetCustomerImageUrl();
}
