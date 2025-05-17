using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using APIV22.Data;
using APIV22.Service; // ✅ NUEVO: agregas esta línea para registrar el servicio

var builder = WebApplication.CreateBuilder(args);

// ✅ Conexión a PostgreSQL usando el contexto de la app
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

// ✅ Registrar ZapatillaService como servicio inyectable (para API y MVC)
builder.Services.AddScoped<ZapatillaService>();

// Habilita controladores con vistas Razor
builder.Services.AddControllersWithViews();

// 👉 Swagger: agregar servicios de documentación para API REST
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "API",
        Version = "v1",
        Description = "Descripción de la API"
    });
});

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts(); // Política de seguridad HTTPS
}

// 👉 Swagger: habilitar para ver y probar API REST desde el navegador
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "API v1");
});

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();

// Ruta principal de la app (controlador Razor por defecto)
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Zapatilla}/{action=Index}/{id?}");

app.Run();
