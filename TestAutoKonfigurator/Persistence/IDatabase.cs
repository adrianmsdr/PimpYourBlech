namespace TestAutoKonfigurator.Persistence;

public interface IDatabase
{
    public void CreateCustomer(Customer customer);   
    
        List<Customer> LoadCustomers();

        public void DeleteCustomer(Customer customer);

        void DeleteCustomers();
        
    void UpdateCustomer(Customer customer);
    
    void UpdateCustomers();
   
        List<Product> LoadProducts();
        
        List<Car> LoadCars();
        
        void SaveCustomers(List<Customer> di);
        
    
        
        void SaveProducts(List<Product> di);
        
        void SaveCars(List<Car> di);
        
        void CreateCar(Car car);
        
        void CreateProduct(Product product);
        
        
        
    }
