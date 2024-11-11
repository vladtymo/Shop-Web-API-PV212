using Core.Interfaces;
using Core.MapperProfiles;
using Core.Models;
using Core.Services;
using Data.Data;
using Data.Entities;
using Data.Repositories;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using ShopWebApi_PV212.Middlewares;
using ShopWebApi_PV212.ServiceExtensions;
using System.Text;
using Core;
using Data;
using Hangfire;
using ShopWebApi_PV212;

var builder = WebApplication.CreateBuilder(args);

string connectionString = builder.Configuration.GetConnectionString("AzureDb")!;

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext(connectionString);
builder.Services.AddIdentity();
builder.Services.AddRepository();

// fluent validators
builder.Services.AddAutoMapper();
builder.Services.AddFluentValidators();

// custom services
builder.Services.AddCustomServices();

// exception handlers
builder.Services.AddExceptionHandler();

builder.Services.AddJWT(builder.Configuration);
builder.Services.AddSwaggerJWT();

builder.Services.AddHangfire(connectionString);

builder.Services.AddCorsPolicies();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
if (app.Environment.IsProduction())
{
    app.UseExceptionHandler();
}

app.UseHttpsRedirection();

app.UseCors("front-end-cors-policy");

app.UseAuthentication();
app.UseAuthorization();

app.UseHangfireDashboard("/dash");
JobConfigurator.AddJobs();

app.MapControllers();

app.Run();
