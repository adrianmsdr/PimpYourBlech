using Microsoft.EntityFrameworkCore;
using PimpYourBlech_BlazorApp.Components;
using PimpYourBlech_ClassLibrary.Factories;
using PimpYourBlech_ClassLibrary.Inventories;
using PimpYourBlech_ClassLibrary.Persistence;
using PimpYourBlech_ClassLibrary.Persistence.EFDatabase;
using PimpYourBlech_ClassLibrary.Services.Admin;
using PimpYourBlech_ClassLibrary.Services.Configurator;
using PimpYourBlech_ClassLibrary.Services.Configurator.Implementation;
using TestAutoKonfigurator.Session;
using TestAutoKonfigurator.Session.Implementation;

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

// 3) Factory
builder.Services.AddScoped<InventoryFactory>();

// 4) Inventories über Factory
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

// 5) Services
builder.Services.AddScoped<IAdminService, AdminService>();
builder.Services.AddScoped<IConfiguratorService, ConfiguratorService>();

// 6) UserSession
builder.Services.AddSingleton<IUserSession, UserSession>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();


app.UseAntiforgery();

app.MapStaticAssets();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
