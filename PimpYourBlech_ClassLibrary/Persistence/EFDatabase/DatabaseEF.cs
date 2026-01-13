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

public DbSet<EngineDetail> Engines => _ctx.Engines;
public DbSet<RimDetail> Rims => _ctx.Rims;
public DbSet<LightsDetail> Lights => _ctx.Lights;
public DbSet<ColorDetail> Colors => _ctx.Colors;
public DbSet<Order> Orders => _ctx.Orders;

public DbSet<PaymentValue> PaymentValues => _ctx.PaymentValues;

public DbSet<CommunityQuestion> CommunityQuestions => _ctx.CommunityQuestions;
public DbSet<CommunityAnswer> CommunityAnswers => _ctx.CommunityAnswers;
public DbSet<DeliveryAddress> DeliveryAddresses => _ctx.DeliveryAddresses;
public DbSet<OrderPosition> OrderPositions => _ctx.OrderPositions;

public int SaveChanges() => _ctx.SaveChanges();

public Task<int> SaveChangesAsync() => _ctx.SaveChangesAsync();

public async Task<int> GetNextArticleNumberAsync() => await _ctx.GetNextArticleNumberAsync();
}
