using System.Drawing;
using PimpYourBlech_ClassLibrary.Entities;
using PimpYourBlech_ClassLibrary.Enums;

namespace PimpYourBlech_ClassLibrary.Services.Admin;

public interface IAdminService
{
    
    
 public Task<List<Customer>> GetListCustomersAsync();

    public Task RegisterCustomerAsync(string firstName, string lastName, string username, string password,
    string passwordConfirm, string telefon, string mailAddress, string mailAdressConfirm, string ImagePath);
    Customer Login(string username, string passwordHash);
    Task EnsureUsernameAvailableAsync(string username);


    Task<Customer> CustomerLoginAsync(string username, string password);

    Task DeleteCustomerAsync(Customer c);

    public Task<Customer> GetCustomerByUsernameAsync(string username);
    
    public Customer GetCustomerByTelefon(String telefone);
    
    public Customer GetCustomerByNames(String firstName, String lastName);

    public Task UpdateCustomerAsync(Customer customer);

    Task<Customer> GetCustomerByIdIncludeAllAsync(int id);
    
    Task<Customer> GetCustomerByIdAsync(int id);
     // ___________________________________Poducts____________________________________
    List<Product> GetProducts();
     Product CreateProduct(Car car, string name, string brand, int quantity, double price,ProductType productType, string description);




     public void RegisterEngine(Product p, int ps, int kw, string displacement, Gear gear, Fuel fuel);


     public void RegisterRim(Product p, decimal diameter, decimal width);


     public void RegisterLights(Product p, int lumen, bool isLED);


     public void RegisterColor(Product p, string colorName);


     public Task<Product> InsertProduct(Product p);
     

     
     Task<Product> GetProductByIdAsync(int id);
   
     Task DeleteProductAsync(Product p);
     
     Task UpdateProductAsync(Product p);

     List<ProductType> GetProductTypes();
   // ___________________________________Cars____________________________________
  List<Car> GetCars();
   
   Task<Car> RegisterCarAsync(string name,string dateProduction,string datePermit,string brand,string model, int ps,int quantity,double price);
   
   Car GetCarById(int id);
   
   Task DeleteCarAsync(Car car);
   
   public Task UpdateCarAsync(Car car);
   
   public List<Product> GetAvailableRims(int id);
   public List<Product> GetAvailableColors(int id);
   // __________________________________Orders____________________________________
   public Task<List<Order>> GetOrdersAsync();
   
   public Task<Order> GetOrderByIdAsync(int id);
}