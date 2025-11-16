namespace TestAutoKonfigurator.Database;

public interface IJsonDatabase
{
   
        List<Customer> LoadCustomers();

       
   
        List<Product> LoadProducts();
        
        List<Car> LoadCars();
        
        void SaveCustomers(List<Customer> di);
        
    
        
        void SaveProducts(List<Product> di);
        
        void SaveCars(List<Car> di);
        
        
        
        
    }
