using PimpYourBlech_Contracts.DTOs;
using PimpYourBlech_Contracts.EntityDTOs;
using PimpYourBlech_Contracts.Enums;


namespace PimpYourBlech_ClassLibrary.Services.Configurator;

public interface IConfiguratorService
{

    public Task<ConfigurationDto> StartNewConfiguration(CustomerDto customer, CarDto car, string name);

    Task<List<ConfigurationDto>> GetAllConfigurationsForCustomerAsync(int customerId);
    public  Task<CarDto> GetCarByIdAsync(int carId);
   public  Task AddProduct(int configurationId, int productId);

    //Preisberechnung
    public  Task<decimal> CalculateTotalPriceAsync(ConfigurationDto configuration);

    public  Task SaveConfigurationAsync(ConfigurationDto configuration, CustomerDto customer);
  

    public  Task DeleteConfiguration(ConfigurationDto configuration);

    public  Task<List<ConfigurationDto>> GetAllConfigurations(CustomerDto customer);





    public Task<List<ProductDto>> ListEnginesAsync();

    public  Task<List<ProductDto>> ListRimsAsync();

    public  Task<List<CarDto>> ListCarsAsync();
    public Task<ConfigurationDto> GetConfigurationByIdAsync(int configurationId);

    public  Task<List<ProductDto>> GetAvailableColorsAsync(int Id);

    public Task<List<ProductDto>> GetAvailableEnginesAsync(int carId);

    public Task<List<ProductDto>> GetAvailableRims(int carId);


    public string GetGearDisplayName(Gear gear);

    public Task<List<ProductDto>> GetAvailableExtras(int carId);

    public Task<List<ProductDto>> GetAvailableProductsAsync(int carId, ProductType type);

    public Task<bool> ConfigurationAvailable(int carId);

    
    public Task<List<CarDto>> GetAvailableCarsAsync();
    
    public Task<List<ConfigurationCarItemsDto>> GetConfigurationBasicsAsync();
    
    Task<List<ConfigurationCarItemsDto>> GetConfigurationsBasicsCustomerAsync(int customerId);

    Task<List<ProductDto>> GetRegisteredProductsAsync(int configId);
}