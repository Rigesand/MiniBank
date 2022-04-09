using FluentValidation;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MiniBank.Core;
using MiniBank.Data;
using MiniBank.Web;
using MiniBank.Web.Controllers.Accounts.Dto;
using MiniBank.Web.Controllers.Accounts.Validators;
using MiniBank.Web.Controllers.Users.Dto;
using MiniBank.Web.Controllers.Users.Validators;
using MiniBank.Web.Middlewares;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services
    .AddCore()
    .AddData(builder.Configuration);

builder.Services.AddAutoMapper(cfg =>
{
    cfg.AddProfile<ApiMappingProfile>();
    cfg.AddProfile<DataMappingProfile>();
});
//Регистрация валидаторов
builder.Services.AddScoped<IValidator<CreateUserDto>, CreateUserValidator>();
builder.Services.AddScoped<IValidator<UpdateUserDto>, UpdateUserValidator>();
builder.Services.AddScoped<IValidator<CreateAccountDto>, CreateAccountValidator>();
var app = builder.Build();

//app.UseException();
app.UseValidationException();


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