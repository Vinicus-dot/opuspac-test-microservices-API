using AuthenticationService.Business.Interfaces;
using Business.Implements;
using Helper.Middleware;
using Microsoft.EntityFrameworkCore;
using Repository;
using Repository.Implements;
using Repository.Interfaces;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(g => g.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, $"{Assembly.GetExecutingAssembly().GetName().Name}.xml")));
builder.Services.AddScoped<IAuthRepository,AuthRepository>();
builder.Services.AddScoped<IAuthBusiness,AuthBusiness>();
//builder.Services.AddDbContext<MicroServiceContext>();
builder.Services.AddDbContextFactory<MicroServiceContext>(options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString("DEFAULT_CONNECTION"));
});
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(builder =>
    {
        builder.AllowAnyOrigin()
        .AllowAnyMethod()
        .AllowAnyHeader();
    });
});
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
