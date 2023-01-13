using MekashronTest.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<IMekashronClient, MekashronClient>();
builder.Services.AddScoped<MekashronService.IICUTech, MekashronService.ICUTechClient>();


// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();


app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
