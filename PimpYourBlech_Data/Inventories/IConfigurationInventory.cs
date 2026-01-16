using PimpYourBlech_Contracts.DTOs;
using PimpYourBlech_Contracts.Enums;
using PimpYourBlech_Data.Models;

namespace PimpYourBlech_Data.Inventories;

public interface IConfigurationInventory
{

    Task<Configuration?> GetConfigurationByIdAsync(int configurationId);

    Task<ConfigurationProductInfo?> GetProductByType(
        int configurationId,
        ProductType productType);

    Task AddProduct(
        int configurationId,
        int productId);

    Task RemoveProduct(
        int configurationId,
        int productId);

    public Task<List<Product>> GetAllProductsAsync(int configurationId);

    Task UpdateConfigurationAsync(int configurationId);

    Task DeleteConfigurationAsync(int configurationId);
    
    Task<List<Configuration>> GetConfigurationsForCustomerAsync(int customerId);
    Task<List<Configuration>> GetConfigurationsIncludeCarProductsAsync(int customerId);
    
    Task RemoveProductAsync(
        int configurationId,
        int productId);

    public Task<Product?> GetProductByIdAsync(int productId);



    public Task AddConfigurationAsync(Configuration config);

}