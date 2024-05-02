using Microsoft.EntityFrameworkCore;
using SistemaHospital.Models;
using SistemaHospital.Repository.Abstract;
using SistemaHospital.Repository.Implementation;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// Inyección de RazorRuntimeCompiler
builder.Services.AddRazorPages().AddRazorRuntimeCompilation();

// Conexión con la base de datos
builder.Services.AddDbContext<BdHospitalContext>(options => { // Básicamente, es una función anónima
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

// Servicio IUnidadTrabajo
builder.Services.AddScoped<IUnidadTrabajo, UnidadTrabajo>(); // Se agrega la interfaz y su implementación

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

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
