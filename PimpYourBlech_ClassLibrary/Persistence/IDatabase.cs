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
    DbSet<DeliveryAddress> DeliveryAddresses { get; }
    int SaveChanges();
    
    Task<int> SaveChangesAsync();
    
    Task<int> GetNextArticleNumberAsync();
 
    }

