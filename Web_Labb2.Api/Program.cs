using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;
using Web_Labb2.Data;
using Web_Labb2.Repositories;
using Web_Labb2.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var options = new ConfigurationBuilder().AddUserSecrets<Program>().Build();
var connectionString = options["connectionString"];
builder.Services.AddDbContext<APIDBContext>(options =>
    options.UseSqlServer(connectionString));


builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<CustomerService>();
builder.Services.AddScoped<ProductService>();



var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<APIDBContext>();
    dbContext.Database.EnsureCreated();
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
