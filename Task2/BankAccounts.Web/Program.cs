using UniCabinet.Infrastructure;

var builder = WebApplication.CreateBuilder(args);
ApplicationsServicesExtensions.AddApplicationLayer(builder.Services);

builder.Services.AddControllersWithViews();

var app = builder.Build();

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
    pattern: "{controller=Bank}/{action=Index}/{id?}");

app.Run();
