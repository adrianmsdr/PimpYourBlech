namespace PimpYourBlech_BlazorApp.Services;
using Microsoft.AspNetCore.Components.Forms;

public interface IImageService
{
    List<string> GetFrameUrls(int carId);
    
    Task<string> SaveFramesAsync(int carId, IReadOnlyList<IBrowserFile> files);

    Task DeleteCarImagesAsync(int carId);
}