using System.Drawing;
using PimpYourBlech_Contracts.EntityDTOs;
using PimpYourBlech_Contracts.Enums;
using PimpYourBlech_Contracts.Query;
using PimpYourBlech_Data.Models;

namespace PimpYourBlech_ClassLibrary.Services.Admin;

public interface IAdminService
{
    
    
 public Task<List<CustomerDto>> GetListCustomersAsync();

    public Task RegisterCustomerAsync(string firstName, string lastName, string username, string password,
    string passwordConfirm, string telefon, string mailAddress, string mailAdressConfirm, string ImagePath);
    


    Task<CustomerDto> CustomerLoginAsync(string username, string password);

    Task DeleteCustomerAsync(CustomerDto c);

   

    public Task UpdateCustomerAsync(CustomerDto customer);
/*
    Task<CustomerDto> GetCustomerByIdIncludeAllAsync(int id);
    */
    Task<CustomerDto> GetCustomerByIdAsync(int id);
    
    Task<List<CustomerDto>> GetFilteredCustomersAsync(CustomerListQuery query);
     // ___________________________________Poducts____________________________________
    List<ProductDto> GetProducts();
     Task<int> CreateProduct(CarDto car, string name, string brand, int quantity, decimal price, ProductType productType,
      string description);




     public Task RegisterEngine(int productId, int ps, int kw, string displacement, Gear gear, Fuel fuel);


     public Task RegisterRim(int productId, decimal diameter, decimal width);


     public Task RegisterLights(int productId, int lumen, bool isLED);


     public Task RegisterColor(int productId, string colorName);


     public Task InsertProduct(Product p);
     

     
     Task<ProductDto> GetProductByIdAsync(int id);
   
     Task DeleteProductAsync(ProductDto p);
     
     Task UpdateProductAsync(ProductDto p);

     List<ProductType> GetProductTypes();
     
     Task<List<ProductDto>> ProductListQueryAsync(ProductListQuery query);
   // ___________________________________Cars____________________________________
  Task<List<CarDto>> GetCarsAsync();
   
   Task<CarDto> RegisterCarAsync(string name, string dateProduction, string datePermit, string brand, string model,
    string ps,
    string quantity, string price);
   
   Task<CarDto> GetCarByIdAsync(int id);
   
   Task DeleteCarAsync(CarDto car);
   
   public Task UpdateCarAsync(CarDto car);
   
   public  Task<List<ProductDto>> GetAvailableRims(int id);
   public Task<List<ProductDto>> GetAvailableColors(int id);
   
   Task<List<CarDto>> CarListQueryAsync(CarListQuery q);
   // __________________________________Orders____________________________________
   public Task<List<OrderDto>> GetOrdersAsync();
   
   public Task<OrderDto> GetOrderByIdAsync(int id);

   public Task<List<OrderPositionDto>> GetOrderItemsAsync(int id);
}