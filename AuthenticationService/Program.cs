using Authentication.Data;
using Authentication.Data.Implements;
using Authentication.Data.Interfaces;
using Authentication.Helper;
using Authentication.Helper.Middleware;
using Microsoft.EntityFrameworkCore;
using System;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IAuthService,AuthService>();
builder.Services.AddDbContext<AuthenticationServiceContext>(options =>
    options.UseNpgsql(Util.GetEnvironmentVariable("DEFAULT_CONNECTION")));  

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
