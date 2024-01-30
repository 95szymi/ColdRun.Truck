using ColdRun.Truck.Domain;
using ColdRun.Truck.Infrastructure;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<TruckDbContext>(options =>
   options.UseInMemoryDatabase("InMemoryDbForTesting"));

builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<TruckDbContext>();
    AddTestData(context);
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
       name: "default",
       pattern: "{controller=Home}/{action=Index}/{id?}");
    endpoints.MapRazorPages();
    endpoints.MapControllers();
});

app.Run();


void AddTestData(TruckDbContext context)
{
    var testTruck1 = new Truck("Test Truck 1", "Test Description");
    var testTruck2 = new Truck("Test Truck 2");
    var testTruck3 = new Truck("Test Truck 3");

    context.Trucks.AddRange(testTruck1, testTruck2, testTruck3);

    context.SaveChanges();
}