using Microsoft.EntityFrameworkCore;
using ProyectoGestionInventarioCAAG._Features.Empleados;
using ProyectoGestionInventarioCAAG.Infraestructure.Inventario;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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

//builder.Services.AddFsAuthService(configureOptions =>
//{
//    configureOptions.Username = builder.Configuration.GetFromENV("Configurations:FsIdentityServer:Username");
//    configureOptions.Password = builder.Configuration.GetFromENV("Configurations:FsIdentityServer:Password");
//});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    //app.UseSwaggerWithFsIdentityServer();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

//app.UseFsAuthService();

app.MapControllers();

app.Run();
