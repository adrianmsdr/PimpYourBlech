using PimpYourBlech_Data.Inventories;
using PimpYourBlech_Data.Inventories.Implementation;
using PimpYourBlech_Data.Persistence;


namespace PimpYourBlech_ClassLibrary.Factories;

public sealed class InventoryFactory(IDatabase database)
{
    private readonly CustomerInventory _customerInventory = new(database);
    private readonly ProductInventory _productInventory = new(database);
    private readonly CarInventory _carInventory = new(database);
    private readonly ConfigurationInventory _configurationInventory = new(database);
    private readonly OrderInventory _orderInventory = new(database);
    

    public ICustomerInventory GetCustomerRepository()
        => _customerInventory;
    
    public IProductInventory GetProductInventory()
    => _productInventory;
    
    public ICarInventory GetCarInventory()
    => _carInventory;
 
    public IConfigurationInventory GetConfigurationInventory()
    => _configurationInventory;
 
    public IOrderInventory GetOrderInventory()
    => _orderInventory;

}