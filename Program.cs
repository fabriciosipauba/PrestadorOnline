using System.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.OpenApi.Models;
using Pomelo.EntityFrameworkCore.MySql;
using PrestadorOnline.Data;
using PrestadorOnline.Models;
using PrestadorOnline.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddDbContext<PrestadoresDbContext>(options =>
    options.UseMySQL(builder.Configuration.GetConnectionString("PrestadorConnection")));

builder.Services.AddDbContext<EspecialidadesDbContext>(options =>
    options.UseMySql(builder.Configuration.GetConnectionString("EspecialidadesConnection"),
    new MySqlServerVersion(new Version(8, 0, 26))));

builder.Services.AddDbContext<ServicosDbContext>(options =>
    options.UseMySql(builder.Configuration.GetConnectionString("ServicoConnection"),
    new MySqlServerVersion(new Version(8, 0, 26))));

builder.Services.AddScoped<IConsultaEspecialidade, ConsultaEspecialidade>();

builder.Services.AddScoped<IConsultaServico, ConsultaServico>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();

    app.UseRouting();

    app.UseHttpsRedirection();

    app.UseAuthorization();

    app.UseEndpoints(endpoints =>
    {
        endpoints.MapControllers();
    });

    app.MapControllers();

    app.Run();

}
