using HexDemoSite.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

var myDocs = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
var dbSource = Path.Join(myDocs, "HexDemoSite.db");
builder.Services.AddDbContext<DataContext>(options =>
    options.UseSqlite($"Data Source={dbSource}"));

var app = builder.Build();

// Ensure db is up to date and has our seeded data
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<DataContext>();
    DataContext.MigrateAndSeed(context);
}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();