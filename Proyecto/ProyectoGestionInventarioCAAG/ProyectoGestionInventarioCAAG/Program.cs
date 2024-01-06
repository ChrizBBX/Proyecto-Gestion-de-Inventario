using Farsiman.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using ProyectoGestionInventarioCAAG._Features.Empleados;
using ProyectoGestionInventarioCAAG._Features.Lotes;
using ProyectoGestionInventarioCAAG._Features.Salidas;
using ProyectoGestionInventarioCAAG._Features.Usuarios;
using ProyectoGestionInventarioCAAG.Infraestructure.Inventario;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSwaggerForFsIdentityServer(opt =>
{
    opt.Title = "Proyecto Gestion Salidas de Inventario";
    opt.Description = "creado por Christopher Aguilar";
    opt.Version = "v1.0";
});

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(builder =>
    {
        builder.AllowAnyOrigin()
               .AllowAnyMethod()
               .AllowAnyHeader();
    });
});

var connectionString = builder.Configuration.GetConnectionString("ConexionProyecto");
builder.Services.AddDbContext<ProyectoGestionInventarioCaagContext>(options => options.UseSqlServer(connectionString));
builder.Services.AddTransient<UnitOfWorkBuilder, UnitOfWorkBuilder>();
builder.Services.AddAutoMapper(typeof(MapProfile));
builder.Services.AddTransient<EmpleadoService>();
builder.Services.AddTransient<EmpleadoDomain>();
builder.Services.AddTransient<UsuarioService>();
builder.Services.AddTransient<UsuarioDomain>();
builder.Services.AddTransient<LoteService>();
builder.Services.AddTransient<LoteDomain>();
builder.Services.AddTransient<SalidaService>();
builder.Services.AddTransient<SalidaDomain>();

builder.Services.AddFsAuthService(configureOptions =>
{
    configureOptions.Username = builder.Configuration.GetFromENV("Configurations:FsIdentityServer:Username");
    configureOptions.Password = builder.Configuration.GetFromENV("Configurations:FsIdentityServer:Password");
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwaggerWithFsIdentityServer();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseFsAuthService();

app.MapControllers();

app.Run();
