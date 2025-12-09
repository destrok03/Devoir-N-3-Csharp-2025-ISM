using GesBanqueAspNet.Data;
using GesBanqueAspNet.Services;
using GesBanqueAspNet.Services.Impl;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<BanqueDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<ICompteService, CompteService>();
builder.Services.AddScoped<ITransactionService, TransactionService>();

builder.Services.AddControllersWithViews();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<BanqueDbContext>();
    try
    {
        db.EnsureTablesCreated();

        db.Transactions.RemoveRange(db.Transactions);
        db.Comptes.RemoveRange(db.Comptes);
        db.SaveChanges();

        DataSeeder.Seed(db);
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Erreur lors de l'initialisation de la base de données: {ex.Message}");
    }
}

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Compte}/{action=Details}/{id?}");

app.Run();
