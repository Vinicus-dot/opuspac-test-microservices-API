using AuthenticationService.Business;
using AuthenticationService.Business.Implements;
using AuthenticationService.Business.Interfaces;
using AuthenticationService.Helper;
using AuthenticationService.Helper.Middleware;
using AuthenticationService.Repository.Implements;
using AuthenticationService.Repository.Interfaces;
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
builder.Services.AddDbContext<AuthenticationServiceContext>();

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
