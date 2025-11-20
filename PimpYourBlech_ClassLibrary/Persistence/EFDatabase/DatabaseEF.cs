using Microsoft.EntityFrameworkCore;
using PimpYourBlech_ClassLibrary.Entities;

namespace PimpYourBlech_ClassLibrary.Persistence.EFDatabase;

public sealed class DatabaseEF : IDatabase
{


private readonly ConfiguratorContext _ctx;

public DatabaseEF(ConfiguratorContext ctx)
{
    _ctx = ctx;
}

public DbSet<Customer> Customers => _ctx.Customers;
public DbSet<Product> Products => _ctx.Products;
public DbSet<Car> Cars => _ctx.Cars;
public DbSet<Configuration> Configurations => _ctx.Configurations;

public int SaveChanges() => _ctx.SaveChanges();
}
/*
public DatabaseEF(ConfiguratorContext context)
{
    db = context;
}
    public void CreateCustomer(Customer customer)
    {
     //   using var db = new ConfiguratorContext();
        db.Customers.Add(customer);
        db.SaveChanges();
    }
    
    public void DeleteCustomer(Customer customer)
    {
     //   using var db = new ConfiguratorContext();
        db.Customers.Remove(customer);
        db.SaveChanges();
    }

    public void DeleteCustomers()
    {
     //   using var db = new ConfiguratorContext();
        db.Customers.RemoveRange(db.Customers);
        db.SaveChanges();
    }

    public void UpdateCustomer(Customer customer)
    {
     //   using var db = new ConfiguratorContext();
        db.Customers.Update(customer);
        db.SaveChanges();
    }

    public void UpdateCustomers()
    {
     //   using var db = new ConfiguratorContext();
        db.Customers.UpdateRange(db.Customers);
        db.SaveChanges();
    }

    public List<Customer> LoadCustomers()
    {
    //    using var db = new ConfiguratorContext();
        return db.Customers.ToList();
    }

    public List<Product> LoadProducts()
    {
      //  using var db = new ConfiguratorContext();
        return db.Products.ToList();
    }

    public List<Car> LoadCars()
    {
     //   using var db = new ConfiguratorContext();
        return db.Cars.ToList();
    }

    public void SaveCustomers(List<Customer> di)
    {
     //   using var db = new ConfiguratorContext();
        db.Customers.AddRange(di);
    }

    public void SaveProducts(List<Product> di)
    {
     //   using var db = new ConfiguratorContext();
        db.Products.AddRange(di);
    }

    public void SaveCars(List<Car> di)
    {
    //   using var db = new ConfiguratorContext();
       db.Cars.AddRange(di);
       
    }

    public void CreateCar(Car car)
    {
     //   using var db = new ConfiguratorContext();
        db.Cars.Add(car);
        db.SaveChanges();
    }

    public void CreateProduct(Product product)
    {
   ///     using var db = new ConfiguratorContext();
        db.Products.Add(product);
        db.SaveChanges();
    }*/
