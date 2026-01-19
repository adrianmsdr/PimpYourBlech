using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.WebUtilities;
using PimpYourBlech_BlazorApp.Services.Toasts.Implementation;
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

    public List<ProductDto> availableLights = new();


    private List<ProductDto> availableExtras = new();
    private ConfigurationDto? configuration;
    private decimal totalPrice;
    private int selectedColorId = 0;
    private int selectedRimId = 0;
    private int selectedLightId = 0;
    private ProductDto? selectedEngine = new ProductDto();

    private async Task SelectColor(int colorId)
    {
        if (configuration == null) return;

        selectedColorId = colorId;

        await ConfiguratorService.HandleProductAsync(configuration.Id, selectedColorId);
        LoadFrames();
        await UpdatePrice();
    }

    protected override async Task OnInitializedAsync()
    {
        CurrentCustomer = await CustomerService.GetCustomerByIdAsync(UserSession.CurrentUserId);

        // 1) Auto laden (einmal)
        Car = await CarService.GetCarByIdAsync(Id);
        if (Car == null) return;

        // 2) Farben laden
        availableColors = await CarService.GetAvailableColorsAsync(Id) ?? new List<ProductDto>();

        // 3) Produkte laden
        availableEngines = await ConfiguratorService.GetAvailableEnginesAsync(Id) ?? new List<ProductDto>();
        availableRims = await ConfiguratorService.GetAvailableRims(Id) ?? new List<ProductDto>();
        availableExtras = await ConfiguratorService.GetAvailableExtras(Id) ?? new List<ProductDto>();
        availableLights = await ConfiguratorService.GetAvailableLightsAsync(Id) ?? new List<ProductDto>();

        // Default Farbe + Felge setzen (damit sofort 360° geht)
        if (availableColors.Count > 0)
            selectedColorId = availableColors[0].ProductId;

        if (availableRims.Count > 0)
            selectedRimId = availableRims[0].ProductId;

        if (availableEngines.Count > 0)
            selectedEngine = availableEngines[0];

        if (availableLights.Count > 0)
            selectedLightId = availableLights[0].ProductId;

        // Frames laden
        LoadFrames();


        // 4) Optional: configId aus QueryString laden
        var uri = Nav.ToAbsoluteUri(Nav.Uri);
        var qs = QueryHelpers.ParseQuery(uri.Query);

        if (qs.TryGetValue("configId", out var s) && int.TryParse(s, out var configId))
        {
            configuration = await ConfiguratorService.GetConfigurationByIdAsync(configId);
            name = configuration?.Name;

            if (configuration != null)
            {
                await ApplyLoadedConfigurationAsync(configuration.Id);
            }
        }
        else
        {
            // Keine bestehende Konfiguration -> Defaults (für 360°-Preview)
            LoadFrames();
        }

        // 5) Preis einmal am Ende
       await UpdatePrice();
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


        await ConfiguratorService.HandleProductAsync(configuration.Id, product.ProductId);


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

        if (product.ProductType == ProductType.Lichter)
        {
            selectedLightId = product.ProductId;
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
        if (string.IsNullOrWhiteSpace(configurationName))
        {
            ToastService.ShowError(
                "Bitte gib einen gültigen Konfigurationsnamen ein."
            );
            return;

        }
        name = configurationName;
        configuration =
            await ConfiguratorService.StartNewConfiguration(
                await CustomerService.GetCustomerByIdAsync(UserSession.CurrentUserId), Car, name);

        // Defaultfarbe + Defaultfelge als Produkt setzen
        if (availableColors.Count > 0)
        {
            selectedColorId = availableColors[0].ProductId;
            await ConfiguratorService.HandleProductAsync(configuration.Id, availableColors[0].ProductId);
        }

        if (availableRims.Count > 0)
        {
            selectedRimId = availableRims[0].ProductId;
            await ConfiguratorService.HandleProductAsync(configuration.Id, availableRims[0].ProductId);
        }

        if (availableEngines.Count > 0)
        {
            selectedEngine = availableEngines[0];
            await ConfiguratorService.HandleProductAsync(configuration.Id, availableEngines[0].ProductId);
        }

        if (availableLights.Count > 0)
        {
            selectedLightId = availableLights[0].ProductId;
            await ConfiguratorService.HandleProductAsync(configuration.Id, availableLights[0].ProductId);
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
        OpenDeleteModal();
    }

    private void GoToOrderConfiguration()
    {
        if (configuration == null || Car == null) return;
        Nav.NavigateTo($"/configurator/{Car.Id}/order/{configuration.Id}", replace: true);
    }

    private async Task SaveConfiguration()
    {
        await ConfiguratorService.SaveConfigurationAsync(configuration, CurrentCustomer);
        ToastService.ShowSuccess("Konfiguration erfolgreich gespeichert.");
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
    
    private async Task ApplyLoadedConfigurationAsync(int configurationId)
    {
        // Produkte der bestehenden Konfiguration laden 
        registeredProducts = await ConfiguratorService.GetRegisteredProductsAsync(configurationId) 
                             ?? new List<ProductDto>();

        // Auswahl-IDs aus den gespeicherten Produkten ableiten
        var savedColor  = registeredProducts.FirstOrDefault(p => p.ProductType == ProductType.Lack);
        var savedRim    = registeredProducts.FirstOrDefault(p => p.ProductType == ProductType.Felge);
        var savedLight  = registeredProducts.FirstOrDefault(p => p.ProductType == ProductType.Lichter);
        var savedEngine = registeredProducts.FirstOrDefault(p => p.ProductType == ProductType.Motor);

        selectedColorId = savedColor?.ProductId ?? (availableColors.FirstOrDefault()?.ProductId ?? 0);
        selectedRimId   = savedRim?.ProductId   ?? (availableRims.FirstOrDefault()?.ProductId ?? 0);
        selectedLightId = savedLight?.ProductId ?? (availableLights.FirstOrDefault()?.ProductId ?? 0);

        
        selectedEngine  = savedEngine ?? availableEngines.FirstOrDefault() ?? new ProductDto();

        // 360° Frames + Preis aktualisieren
        LoadFrames();
        await UpdatePrice();

        
    }
    
    private bool showDeleteModal;

    void OpenDeleteModal() => showDeleteModal = true;
    void CloseDeleteModal() => showDeleteModal = false;

    async Task ConfirmDelete()
    {
        await ConfiguratorService.DeleteConfiguration(configuration);
        showDeleteModal = false;
        Nav.NavigateTo("/mainMenu");

    }
}