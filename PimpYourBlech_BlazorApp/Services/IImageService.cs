namespace PimpYourBlech_BlazorApp.Services;
using Microsoft.AspNetCore.Components.Forms;

public interface IImageService
{
    List<string> GetCarFrameUrls(int carId);
    
    Task<string> SaveCarFramesAsync(int carId, IReadOnlyList<IBrowserFile> files);
    
    String GetProductImageUrl(int productId);
    
    Task<string> SaveProductImagesAsync(int productId, IBrowserFile file);

    Task DeleteCarImagesAsync(int carId);
}