using Microsoft.EntityFrameworkCore;
using WorkOrderService.Helper;
using WorkOrderService.HostRabbitMQ;
using WorkOrderService.Repository;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddHostedService<OrdersConsumer>();
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<OrdersServiceContext>(options =>
    options.UseNpgsql(Util.GetEnvironmentVariable("DEFAULT_CONNECTION")));

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
