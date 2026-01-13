using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.WebUtilities;
using PimpYourBlech_ClassLibrary.Entities;
using PimpYourBlech_ClassLibrary.Enums;

namespace PimpYourBlech_BlazorApp.Components.Pages.Configurator;

public partial class ConfiguratorMenu : ComponentBase
{
     [Parameter] public int Id { get; set; }

    public Car Car { get; set; }

    private string name = null;
    private string configurationName;
    private string? CurrentImageUrl;

    // 360°-Frames
    private List<string> frameUrls = new();
    private int currentFrame = 0;

    private List<Product> availableColors = new();
    private List<Product> availableEngines = new();
    private List<Product> availableRims = new();
    private List<Product> availableLights = new();
    private List<Product> availableExtras = new();

    private Configuration? configuration;
    private decimal totalPrice;

    private int selectedColorId = 0;
    private int selectedRimId = 0;
    private Product? selectedEngine = new Product();

    private void SelectColor(int colorId)
    {
        if (configuration == null) return;

        selectedColorId = colorId;

        ConfiguratorService.AddProduct(configuration.Id, selectedColorId);
        LoadFrames();
        UpdatePrice();
    }

    protected override async Task OnInitializedAsync()
    {
        // 1) Auto laden (einmal)
        Car = await ConfiguratorService.GetCarByIdAsync(Id);
        if (Car == null) return;

        // 2) Farben laden
        availableColors = await ConfiguratorService.GetAvailableColorsAsync(Id) ?? new List<Product>();

        // 3) Produkte laden
        availableEngines = await ConfiguratorService.GetAvailableEnginesAsync(Id) ?? new List<Product>();
        availableRims = await ConfiguratorService.GetAvailableProductsAsync(Id,ProductType.Felge) ?? new List<Product>();
        availableExtras = ConfiguratorService.GetAvailableExtras(Id) ?? new List<Product>();

        // Default Farbe + Felge setzen (damit sofort 360° geht)
             if (availableColors.Count > 0)
            selectedColorId = availableColors[0].ProductId;

        if (availableRims.Count > 0)
            selectedRimId = availableRims[0].ProductId;

        if (availableEngines.Count > 0)
            selectedEngine = availableEngines[0];

        // Frames laden
        LoadFrames();


        // 4) Optional: configId aus QueryString laden
        var uri = Nav.ToAbsoluteUri(Nav.Uri);
        var qs = QueryHelpers.ParseQuery(uri.Query);

        if (qs.TryGetValue("configId", out var s) && int.TryParse(s, out var configId))
        {
            configuration = ConfiguratorService.GetConfigurationById(configId);
            name = configuration?.Name;
        }

        // 5) Preis einmal am Ende
        UpdatePrice();
    }


    private void RotateLeft()
    {
        if (frameUrls.Count == 0) return;
        currentFrame = (currentFrame - 1 + frameUrls.Count) % frameUrls.Count;
    }

    private void RotateRight()
    {
        if (frameUrls.Count == 0) return;
        currentFrame = (currentFrame + 1) % frameUrls.Count;
    }

    private void HandleProduct(Product product)
    {
        if (configuration == null) return;


        ConfiguratorService.AddProduct(configuration.Id, product.ProductId);

        if (product.ProductType == ProductType.Felge)
            selectedRimId = product.ProductId;

        if (product.ProductType == ProductType.Lack)
            selectedColorId = product.ProductId;

        if (product.ProductType == ProductType.Motor)
        {
            selectedEngine = product;
        }


        LoadFrames();
        UpdatePrice();
    }

    private void RemoveProduct(Product product)
    {
        if (configuration == null) return;
        configuration.Products.Remove(product);
        UpdatePrice();
    }

    private void UpdatePrice()
    {
        if (configuration == null) return;
        totalPrice = ConfiguratorService.CalculateTotalPrice(configuration);
    }

    private async Task StartConfiguration()
    {
        name = configurationName;
        configuration = ConfiguratorService.StartNewConfiguration(await AdminService.GetCustomerByIdAsync(UserSession.CurrentUserId), Car, name);

        // Defaultfarbe + Defaultfelge als Produkt setzen
        if (availableColors.Count > 0)
        {
            selectedColorId = availableColors[0].ProductId;
            ConfiguratorService.AddProduct(configuration.Id, availableColors[0].ProductId);
        }

        if (availableRims.Count > 0)
        {
            selectedRimId = availableRims[0].ProductId;
            ConfiguratorService.AddProduct(configuration.Id, availableRims[0].ProductId);
        }
        
        if (availableEngines.Count > 0)
        {
            selectedEngine = availableEngines[0];
            ConfiguratorService.AddProduct(configuration.Id, availableEngines[0].ProductId);
        }

        LoadFrames();
        UpdatePrice();

    }

    private void Cancel()
    {
        Nav.NavigateTo("/mainMenu");
    }

    private void DeleteConfiguration()
    {
        ConfiguratorService.DeleteConfiguration(configuration);
        Nav.NavigateTo("/mainMenu");
    }

    private void GoToOrderConfiguration()
    {
        if (configuration == null || Car == null) return;
        Nav.NavigateTo($"/configurator/{Car.Id}/order/{configuration.Id}", replace: true);

    }

    private void SaveConfiguration()
    {
        ConfiguratorService.SaveConfiguration(configuration);
    }

    private void LoadFrames()
    {
        if (Car == null) return;
        
        if (selectedColorId == 0 || selectedRimId == 0)
        {
            frameUrls = new List<string>();
            currentFrame = 0;
            return;
        }

        frameUrls = ImageService.Get360ImageUrls(Car.Id, selectedColorId, selectedRimId) ?? new List<string>();
        if (frameUrls.Count == 0)
        {
            CurrentImageUrl = ImageService.GetCarImageUrl(Car.Id);
        }

       
    }
}