using PimpYourBlech_ClassLibrary.Entities;
using PimpYourBlech_ClassLibrary.Enums;

namespace PimpYourBlech_ClassLibrary.Services.Admin;

public interface IAdminService
{
    
    
    public List<Customer> GetListCustomers();

    Customer Register(string firstName, string lastName, string username, string passwordHash, string telefon,
        string mailAddress);
    Customer Login(string username, string passwordHash);
    bool isUsernameAvailable(string username);
    
    bool LoginAccepted(string username, string passwordHash);
    
    Customer GetCustomer(string username, string passwordHash);
    
    void DeleteAllCustomers();
    
    void DeleteCustomer(Customer c);
    
    public Customer GetCustomerByUsername(String username);
    
    public Customer GetCustomerByTelefon(String telefone);
    
    public Customer GetCustomerByNames(String firstName, String lastName);

    public void UpdateCustomer(Customer customer, String username, String passwordHash, String telefon);

    void UpdateCustomers();
     // ___________________________________Poducts____________________________________
    List<Product> GetProducts();
     Product RegisterProduct(Product product);
    Engine RegisterEngine(string name,
        string articleNumber,
        string brand,
        string description,
        int quantity,
        double price,
        int _ps,
        int _kw,
        string _displacement,
        Gear _gear);

   
   // ___________________________________Cars____________________________________
  List<Car> GetCars();
   
   Car RegisterCar(string name,string dateProduction,string datePermit,string brand,string model, int ps,int quantity,double price);
}