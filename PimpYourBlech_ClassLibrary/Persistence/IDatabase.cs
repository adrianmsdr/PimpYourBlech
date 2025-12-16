using Microsoft.EntityFrameworkCore;
using PimpYourBlech_ClassLibrary.Entities;

namespace PimpYourBlech_ClassLibrary.Persistence;

public interface IDatabase
{
    DbSet<Customer> Customers { get; }
    DbSet<Product> Products { get; }
    DbSet<Car> Cars { get; }
    DbSet<Configuration> Configurations { get; }
    
    DbSet<Order> Orders { get; }

    DbSet<LightsDetail> Lights { get; }
    DbSet<ColorDetail> Colors { get; }

    DbSet<EngineDetail>  Engines { get; }

    DbSet<RimDetail>  Rims { get; }
    
    DbSet<CommunityQuestion> CommunityQuestions { get; }
    DbSet<CommunityAnswer> CommunityAnswers { get; }

    int SaveChanges();
    /*public void CreateCustomer(Customer customer);   
    
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
        
        void CreateProduct(Product product);*/
        
        
        
    }
