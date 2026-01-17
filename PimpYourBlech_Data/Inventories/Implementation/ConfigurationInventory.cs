using Microsoft.EntityFrameworkCore;
using PimpYourBlech_Contracts.DTOs;
using PimpYourBlech_Contracts.Enums;
using PimpYourBlech_Data.Models;
using PimpYourBlech_Data.Persistence;

namespace PimpYourBlech_Data.Inventories.Implementation;

public class ConfigurationInventory(IDatabase database):IConfigurationInventory
{
    
    public async Task<Configuration?> GetConfigurationByIdAsync(int configurationId)
    {
        return await database.Configurations
            .Where(c=>c.Id == configurationId)
            .FirstOrDefaultAsync();
        
    }

    public async Task<ConfigurationProductInfo?> GetProductByType(
        int configurationId,
        ProductType productType)
    {
        return await database.Configurations
            .Where(c => c.Id == configurationId)
            .SelectMany(c => c.Products)
            .Where(p => p.ProductType == productType)
            .Select(p => new ConfigurationProductInfo(
                p.ProductId,
                p.ProductType))
            .FirstOrDefaultAsync();
    }
    
    public async Task AddProduct(int configurationId, int productId)
    {
        var configuration = await database.Configurations
            .FirstAsync(c => c.Id == configurationId);

        var product = await database.Products
            .FirstAsync(p => p.ProductId == productId);

        configuration.Products.Add(product);
        await database.SaveChangesAsync();
        Console.WriteLine($"Added product {productId}");
    }
    
    public async Task RemoveProduct(int configurationId, int productId)
    {
        var configuration = await database.Configurations
            .Include(c => c.Products)
            .FirstAsync(c => c.Id == configurationId);

        var product = configuration.Products
            .First(p => p.ProductId == productId);

        configuration.Products.Remove(product);
        await database.SaveChangesAsync();
    }

    public async Task<List<Product>> GetAllProductsAsync(int configurationId)
    {
        return await database.Configurations
            .Where(c => c.Id == configurationId)
            .SelectMany(c => c.Products).ToListAsync();
    }

    public async Task UpdateConfigurationAsync(int configurationId)
    {
        await database.SaveChangesAsync();
    }

    public async Task DeleteConfigurationAsync(int configurationId)
    {
        var configuration = await database.Configurations
            .FirstAsync(c => c.Id == configurationId);
        database.Configurations.Remove(configuration);
        await database.SaveChangesAsync();
    }

    public async Task<List<Configuration>> GetConfigurationsForCustomerAsync(int customerId)
    {
        return await database.Configurations
            .Where(c => c.CustomerId == customerId)
            .ToListAsync();
    }

    public async Task<List<Configuration>> GetConfigurationsIncludeCarProductsAsync(int customerId)
    {
        return await database.Configurations
            .Where(c => c.CustomerId == customerId)
            .Include(c => c.Products)
            .Include(c => c.Car)
            .ToListAsync();
    }

    public async Task<Product?> GetProductByIdAsync(int productId)
    {
        return await database.Products
            .Where(p => p.ProductId == productId)
            .FirstOrDefaultAsync();
    }

    public async Task RemoveProductAsync(int configurationId, int productId)
    {
        var config = await database.Configurations
            .FirstAsync(c => c.Id == configurationId);
        var product = await GetProductByIdAsync(productId);
        config.Products.Remove(product);
        await database.SaveChangesAsync();
        
    }
    
    public async Task AddConfigurationAsync(Configuration config)
    {
        database.Configurations.Add(config);
      await  database.SaveChangesAsync();
    }
}