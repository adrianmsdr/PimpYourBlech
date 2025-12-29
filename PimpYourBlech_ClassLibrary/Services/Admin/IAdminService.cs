using System.Drawing;
using PimpYourBlech_ClassLibrary.Entities;
using PimpYourBlech_ClassLibrary.Enums;

namespace PimpYourBlech_ClassLibrary.Services.Admin;

public interface IAdminService
{
    
    
    public List<Customer> GetListCustomers();

    Customer Register(string firstName, string lastName, string username, string passwordHash, string telefon,
        string mailAddress, string ImagePath);
    Customer Login(string username, string passwordHash);
    bool isUsernameAvailable(string username);
    
    bool LoginAccepted(string username, string passwordHash);
    
    Customer GetCustomer(string username, string passwordHash);
    
    void DeleteAllCustomers();
    
    void DeleteCustomer(Customer c);
    
    public Customer GetCustomerByUsername(String username);
    
    public Customer GetCustomerByTelefon(String telefone);
    
    public Customer GetCustomerByNames(String firstName, String lastName);

    public void UpdateCustomer(Customer customer);

    void UpdateCustomers();
    
    Customer GetCustomerById(int id);
     // ___________________________________Poducts____________________________________
    List<Product> GetProducts();
     Product CreateProduct(Car car, string name, string articleNumber, string brand, int quantity, double price,ProductType productType);

     public Product RegisterEngine(Product p, int ps, int kw, string displacement, Gear gear);
     public Product RegisterRim(Product p, decimal diameter, decimal width);
     
     public Product RegisterLights(Product p, int lumen, bool isLED);
     
     public Product RegisterColor(Product p, Car c,string colorName);
     Product GetProductById(int id);
   
     void DeleteProduct(Product p);
     
     void UpdateProduct(Product p);
   // ___________________________________Cars____________________________________
  List<Car> GetCars();
   
   Car RegisterCar(string name,string dateProduction,string datePermit,string brand,string model, int ps,int quantity,double price);
   
   Car GetCarById(int id);
   
   void DeleteCar(Car car);
   
   public void UpdateCar(Car car);
   
   public List<Product> GetAvailableRims(int id);
   public List<Product> GetAvailableColors(int id);
}