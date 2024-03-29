using Infra;
using Core;
using Api.Filters;
using Shared.ValueObject.Exceptions;

namespace Api;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.

        builder.Services.AddControllers();
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        builder.Services.AddInfra(builder.Configuration);
        builder.Services.AddCoreApplication();

        builder.Services.AddMvc((opt) => opt.Filters.Add(typeof(ExceptionsFilter)));

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        if(!app.Environment.IsDevelopment() && !app.Environment.IsProduction())
        {
            Infra.Bootstrap.RunMigrations(app.Services);
        }

        app.UseHttpsRedirection();

        app.UseAuthorization();


        app.MapControllers();

        app.Run();
    }
}