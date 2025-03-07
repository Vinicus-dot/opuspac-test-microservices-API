using Microsoft.EntityFrameworkCore;
using ProductService.Business.Implements;
using ProductService.Business.Interfaces;
using ProductService.Helper;
using ProductService.Helper.Middleware;
using ProductService.Repository;
using ProductService.Repository.Implements;
using ProductService.Repository.Interfaces;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<IProductsRepository, ProductsRepository>();
builder.Services.AddScoped<IProductsBusiness, ProductsBusiness>();
builder.Services.AddDbContext<ProductServiceContext>(options =>
    options.UseNpgsql(Util.GetEnvironmentVariable("DEFAULT_CONNECTION")));

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();
app.UseMiddleware<ExceptionMiddleware>();

app.UseCors();
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
