using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Diagnostics;
using System;

var builder = WebApplication.CreateBuilder(args);



var Configbuilder = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
IConfiguration config = Configbuilder.Build();
string databaseConnectionString = Environment.GetEnvironmentVariable("Connection");

//string databaseConnectionString = config.GetValue<string>("OrdersDatabase");
builder.Services.AddDbContext<OrdersDbContext>(options =>
    options.UseSqlServer(databaseConnectionString));

//Data Source=database-1.c41enn7adu8i.us-east-2.rds.amazonaws.com;Initial Catalog=Orders;User ID=admin;Password=12345678;Encrypt=False

// Add services to the container.

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
}



app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();


app.Run();
