using LanchesMac.Context;
using LanchesMac.Repository.Interface;
using LanchesMac.Repository;
using Microsoft.EntityFrameworkCore;
using LanchesMac.Models;
using LanchesMac.Models.Usuarios;
using Microsoft.AspNetCore.Identity;
using LanchesMac.Extensions;
using LanchesMac.Services.Interfaces;
using LanchesMac.Services;
using LanchesMac.Settings;
using System.Globalization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

var cultureInfo = new CultureInfo("pt-BR");
cultureInfo.NumberFormat.NumberDecimalSeparator = ".";
cultureInfo.NumberFormat.CurrencyDecimalSeparator = ".";
cultureInfo.NumberFormat.NumberDecimalDigits = 2;
cultureInfo.NumberFormat.CurrencyDecimalDigits = 2;
CultureInfo.DefaultThreadCurrentCulture = cultureInfo;
CultureInfo.DefaultThreadCurrentUICulture = cultureInfo;

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddIdentity<Usuario, Funcao>()
                       .AddEntityFrameworkStores<AppDbContext>()
                       .AddDefaultUI()
                       .AddErrorDescriber<PortugueseIdentityErrorDescriber>()
                       .AddDefaultTokenProviders();

builder.Services.ConfigureApplicationCookie(options =>
{
    options.Cookie.HttpOnly = true;
    options.ExpireTimeSpan = TimeSpan.FromMinutes(20);

    options.LoginPath = "/Account/Login";
    options.AccessDeniedPath = "/Account/AccessDenied";
    options.LogoutPath = "/Account/Logout";
    options.SlidingExpiration = true;
});

builder.Services.Configure<ImagesSettings>(builder.Configuration.GetSection("ConfigurationImagens"));

builder.Services.AddScoped<IPhotoService, PhotoService>();
builder.Services.AddScoped<IReportService, ReportService>();
builder.Services.AddScoped<ISeedInitial, SeedInitialService>();
builder.Services.AddTransient<ILancheRepository, LancheRepository>();
builder.Services.AddTransient<ICategoriaRepository, CategoriaRepository>();
builder.Services.AddTransient<IPedidoRepository, PedidoRepository>();
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddScoped(sp => CarrinhoCompra.GetCarrinho(sp));

builder.Services.AddControllersWithViews();

builder.Services.AddMemoryCache();
builder.Services.AddSession();

var app = builder.Build();

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
app.UseSession();
app.UseAuthentication();
app.UseAuthorization();

using (var scope = app.Services.CreateScope())
{
    var seedUserService = scope.ServiceProvider.GetRequiredService<ISeedInitial>();
    seedUserService.Seed();
}

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
    name: "areas",
    pattern: "{area:exists}/{controller=Admin}/{action=Index}/{id?}");

    endpoints.MapControllerRoute(
    name: "categoriaFiltro",
    pattern: "Lanche/{action}/{categoria?}",
    defaults: new { controller = "Lanche", action = "List" });

    endpoints.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");   
});

app.Run();
