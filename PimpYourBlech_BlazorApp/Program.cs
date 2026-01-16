using Blazored.Toast;
using Microsoft.EntityFrameworkCore;
using PimpYourBlech_BlazorApp.Components;
using PimpYourBlech_BlazorApp.Services;
using PimpYourBlech_BlazorApp.Services.Toasts;
using PimpYourBlech_ClassLibrary.Factories;
using PimpYourBlech_ClassLibrary.Services.Admin;
using PimpYourBlech_ClassLibrary.Services.Carts;
using PimpYourBlech_ClassLibrary.Services.Configurator;
using PimpYourBlech_ClassLibrary.Services.Configurator.Implementation;
using PimpYourBlech_ClassLibrary.Services.CustomerCommunication;
using PimpYourBlech_ClassLibrary.Services.CustomerCommunication.Implementation;
using PimpYourBlech_ClassLibrary.Services.FAQ;
using PimpYourBlech_ClassLibrary.Services.Shop;
using PimpYourBlech_ClassLibrary.Services.Shop.Implementation;
using PimpYourBlech_ClassLibrary.Session;
using PimpYourBlech_ClassLibrary.Session.Implementation;
using PimpYourBlech_ClassLibrary.Services.Comparator;
using PimpYourBlech_ClassLibrary.Services.Comparator.Implementation;
using PimpYourBlech_ClassLibrary.Services.Customers;
using PimpYourBlech_ClassLibrary.Services.Customers.Implementation;
using PimpYourBlech_ClassLibrary.Services.Orders;
using PimpYourBlech_ClassLibrary.Services.Products;
using PimpYourBlech_ClassLibrary.Services.Products.Implememtation;
using PimpYourBlech_Data.Inventories;
using PimpYourBlech_Data.Persistence;
using PimpYourBlech_Data.Persistence.EFDatabase;
using Serilog;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

// 1) DbContext (ConnectionString anpassen!)
builder.Services.AddDbContext<ConfiguratorContext>(options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"));
    // oder UseSqlServer(...) je nach DB
});

// 2) EF-Datenbankschicht
builder.Services.AddScoped<IDatabase, DatabaseEF>();

// 3) Email Service
builder.Services.AddScoped<IEmailService, EmailService>();

// 4) Factory
builder.Services.AddScoped<InventoryFactory>();

// 5) Inventories über Factory
builder.Services.AddScoped<ICustomerInventory>(sp =>
{
    var fac = sp.GetRequiredService<InventoryFactory>();
    return fac.GetCustomerRepository();
});

builder.Services.AddScoped<IProductInventory>(sp =>
{
    var fac = sp.GetRequiredService<InventoryFactory>();
    return fac.GetProductInventory();
});

builder.Services.AddScoped<ICarInventory>(sp =>
{
    var fac = sp.GetRequiredService<InventoryFactory>();
    return fac.GetCarInventory();
});

builder.Services.AddScoped<IConfigurationInventory>(sp =>
{
    var fac = sp.GetRequiredService<InventoryFactory>();
    return fac.GetConfigurationInventory();
});

builder.Services.AddScoped<IOrderInventory>(sp =>
{
    var fac = sp.GetRequiredService<InventoryFactory>();
    return fac.GetOrderInventory();
});

// 6) Services
builder.Services.AddScoped<IAdminService, AdminService>();
builder.Services.AddScoped<ICustomerService, CustomerService>();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IConfiguratorService, ConfiguratorService>();
builder.Services.AddScoped<IComparatorService, ComparatorService>();
builder.Services.AddScoped<IShopService, ShopService>();
builder.Services.AddScoped<IImageService, ImageService>();
builder.Services.AddScoped<ICartService, CartService>();
builder.Services.AddScoped<IFaqService, FaqService>();
builder.Services.AddScoped<IOrderService, OrderService>();
builder.Services.AddScoped<IToastExecutor, ToastExecutor>();

// 7) UserSession
builder.Services.AddScoped<IUserSession, UserSession>();

//8) ToastNotification
builder.Services.AddBlazoredToast();

//9) Serilog
// 9.1 Serilog Konfiguration laden (optional: über appsettings.json)
var configuration = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json")
    .Build();
// 9.2 Serilog Logger erstellen
Log.Logger = new LoggerConfiguration()
    .ReadFrom.Configuration(configuration) // Liest Konfiguration aus appsettings.json
    .Enrich.FromLogContext()
    .CreateLogger();
// 9.3 Serilog als Logging-Provider für den Host verwenden
builder.Logging.ClearProviders();
builder.Host.UseSerilog(); 

Log.Information("Application starting");

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseAntiforgery();

//app.MapStaticAssets();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
