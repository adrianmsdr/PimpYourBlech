using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.WebUtilities;
using PimpYourBlech_Contracts.EntityDTOs;
using PimpYourBlech_Contracts.Enums;

namespace PimpYourBlech_BlazorApp.Components.Pages.Configurator;

public partial class ConfiguratorMenu : ComponentBase
{
     [Parameter] public int Id { get; set; }
    public CarDto Car { get; set; }

    private string name = null;
    private string configurationName;
    private string? CurrentImageUrl;
private CustomerDto? CurrentCustomer;
    // 360°-Frames
    private List<string> frameUrls = new();
    private int currentFrame = 0;
    private List<ProductDto> registeredProducts = new();

    private List<ProductDto> availableColors = new();
    private List<ProductDto> availableEngines = new();
    private List<ProductDto> availableRims = new();
    private List<ProductDto> availableExtras = new();
    private ConfigurationDto? configuration;
    private decimal totalPrice;
    private int selectedColorId = 0;
    private int selectedRimId = 0;
    private ProductDto? selectedEngine = new ProductDto();

    private async Task SelectColor(int colorId)
    {
        if (configuration == null) return;

        selectedColorId = colorId;

       await ConfiguratorService.AddProduct(configuration.Id, selectedColorId);
        LoadFrames();
        await UpdatePrice();
    }

    protected override async Task OnInitializedAsync()
    {
        CurrentCustomer = await AdminService.GetCustomerByIdAsync(UserSession.CurrentUserId);
        
        // 1) Auto laden (einmal)
        Car = await ConfiguratorService.GetCarByIdAsync(Id);
        if (Car == null) return;

        // 2) Farben laden
        availableColors = await ConfiguratorService.GetAvailableColorsAsync(Id) ?? new List<ProductDto>();

        // 3) Produkte laden
        availableEngines = await ConfiguratorService.GetAvailableEnginesAsync(Id) ?? new List<ProductDto>();
        availableRims = await ConfiguratorService.GetAvailableProductsAsync(Id,ProductType.Felge) ?? new List<ProductDto>();
        availableExtras = await ConfiguratorService.GetAvailableExtras(Id) ?? new List<ProductDto>();

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
            configuration = await ConfiguratorService.GetConfigurationByIdAsync(configId);
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

    private async Task HandleProduct(ProductDto product)
    {
        if (configuration == null)
        {
            return;
        }


       await ConfiguratorService.AddProduct(configuration.Id, product.ProductId);
       

       if (product.ProductType == ProductType.Felge)
       {
           selectedRimId = product.ProductId;
       }

       if (product.ProductType == ProductType.Lack)
       {
           selectedColorId = product.ProductId;
       }


       if (product.ProductType == ProductType.Motor)
        {
            selectedEngine = product;
        }

registeredProducts = await ConfiguratorService.GetRegisteredProductsAsync(configuration.Id);
        LoadFrames();
        await UpdatePrice();
    }
    

    private async Task UpdatePrice()
    {
        if (configuration == null) return;
        totalPrice = await ConfiguratorService.CalculateTotalPriceAsync(configuration);
    }

    private async Task StartConfiguration()
    {
        name = configurationName;
        configuration = await ConfiguratorService.StartNewConfiguration(await AdminService.GetCustomerByIdAsync(UserSession.CurrentUserId), Car, name);

        // Defaultfarbe + Defaultfelge als Produkt setzen
        if (availableColors.Count > 0)
        {
            selectedColorId = availableColors[0].ProductId;
            await ConfiguratorService.AddProduct(configuration.Id, availableColors[0].ProductId);
        }

        if (availableRims.Count > 0)
        {
            selectedRimId = availableRims[0].ProductId;
            await ConfiguratorService.AddProduct(configuration.Id, availableRims[0].ProductId);
        }
        
        if (availableEngines.Count > 0)
        {
            selectedEngine = availableEngines[0];
           await  ConfiguratorService.AddProduct(configuration.Id, availableEngines[0].ProductId);
        }

        LoadFrames();
        registeredProducts = await ConfiguratorService.GetRegisteredProductsAsync(configuration.Id);

       await UpdatePrice();
        

    }

    private void Cancel()
    {
        Nav.NavigateTo("/mainMenu");
    }

    private async Task DeleteConfiguration()
    {
        await ConfiguratorService.DeleteConfiguration(configuration);
        Nav.NavigateTo("/mainMenu");
    }

    private void GoToOrderConfiguration()
    {
        if (configuration == null || Car == null) return;
        Nav.NavigateTo($"/configurator/{Car.Id}/order/{configuration.Id}", replace: true);

    }

    private async Task SaveConfiguration()
    {
        await ConfiguratorService.SaveConfigurationAsync(configuration,CurrentCustomer);
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