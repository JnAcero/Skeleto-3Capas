using System.Reflection;
using System.Text.Json.Serialization;
using AspNetCoreRateLimit;
using Infrastructure;
using Microsoft.EntityFrameworkCore;
using WebApi.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.ConfigCores();
builder.Services.AddAplicationServices();
builder.Services.AddJwt(builder.Configuration);
builder.Services.ConfigureRateLimiting();
builder.Services.ConfigureApiVersioning();
builder.Services.AddControllers()
    .AddJsonOptions(options => 
    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddAutoMapper(Assembly.GetEntryAssembly());
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<AplicationDbContext>(options =>{
    string connectionString = builder.Configuration.GetConnectionString("ConnectionCasa");
    options.UseMySql(connectionString,ServerVersion.AutoDetect(connectionString));

});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors("CorsPolicy");
app.UseHttpsRedirection();

app.UseIpRateLimiting()
;
app.UseApiVersioning();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
