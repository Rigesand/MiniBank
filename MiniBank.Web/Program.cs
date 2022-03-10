using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MiniBank.Core.Interfaces;
using MiniBank.Core.Services;
using MiniBank.Data.Services;
using MiniBank.Web.ExstensionsMiddlewares;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<ICurrencyConverter, CurrencyConverterService>();
builder.Services.AddScoped<ICurrencyCode, CurrencyCodeService>();

var app = builder.Build();

app.UseException();
app.UserFriendlyException();


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