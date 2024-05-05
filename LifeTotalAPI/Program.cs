using LifeTotalAPI.Data;
using LifeTotalAPI.Repository;
using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;
using LifeTotalAPI.Services;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Define the connection string directly here
        var connectionString = "Data Source=DESKTOP-P416;Initial Catalog=LifeTotalServer;Integrated Security=True;Encrypt=False";

        builder.Services.AddDbContext<DatabaseContext>(options => options.UseSqlServer(connectionString));


        // Add services to the container.
        builder.Services.AddControllers().AddJsonOptions(options =>
        {
            options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
        });

        builder.Services.AddScoped<GamematchRepository>();
        builder.Services.AddScoped<GamematchService>();

        builder.Services.AddScoped<PlayerRepository>();
        builder.Services.AddScoped<PlayerService>();

        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();
        builder.Services.AddCors();


        var app = builder.Build();
        app.UseCors(builder => builder.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin());


        // Configure the HTTP request pipeline.
        //if (app.Environment.IsDevelopment())
        //{
            app.UseSwagger();
            app.UseSwaggerUI();
        //}

        app.UseHttpsRedirection();

        app.UseAuthorization();

        app.MapControllers();

        app.Run();
    }
}