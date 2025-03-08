using Authentication.Business;
using Authentication.Business.Implements;
using Authentication.Business.Interfaces;
using Authentication.Helper;
using Authentication.Helper.Middleware;
using Authentication.Repository.Implements;
using Authentication.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(g => g.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, $"{Assembly.GetExecutingAssembly().GetName().Name}.xml")));
builder.Services.AddScoped<IAuthRepository,AuthRepository>();
builder.Services.AddScoped<IAuthBusiness,AuthBusiness>();
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
