using AutoMapper;                    // <-- este sí
using Microsoft.EntityFrameworkCore;
using SGC.Data;
using SGC.Business.Services;
using SGC.Business.Profiles;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews().AddRazorRuntimeCompilation();

// DB
builder.Services.AddDbContext<SGCDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// AutoMapper (registro manual, SIN AddAutoMapper)
var mapperConfig = new MapperConfiguration(cfg =>
{
    // puedes cargar por perfil explícito…
    cfg.AddProfile<MappingProfile>();
    // …o por ensamblados, si prefieres:
    // cfg.AddMaps(typeof(MappingProfile).Assembly);
});

IMapper mapper = mapperConfig.CreateMapper();
builder.Services.AddSingleton(mapper);

// Servicios
builder.Services.AddScoped<LoginService>();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Login}/{action=Index}/{id?}");

app.Run();
