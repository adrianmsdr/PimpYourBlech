using TestAutoKonfigurator.Inventories;
using TestAutoKonfigurator.Inventories.Implementation;
using TestAutoKonfigurator.Persistence;

namespace TestAutoKonfigurator.Factories;

public sealed class InventoryFactory(IDatabase database)
{
    private readonly CustomerInventory _customerInventory = new(database);
    private readonly ProductInventory _productInventory = new(database);
    private readonly CarInventory _carInventory = new(database);
    

    public ICustomerInventory GetCustomerRepository()
        => _customerInventory;
    
    public IProductInventory GetProductInventory()
    => _productInventory;
    
    public ICarInventory GetCarInventory()
    => _carInventory;

}