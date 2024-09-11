using Core.Interfaces;
using Core.MapperProfiles;
using Core.Services;
using Data.Data;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.EntityFrameworkCore;
using ShopWebApi_PV212.Middlewares;

var builder = WebApplication.CreateBuilder(args);

string? connectionString = builder.Configuration.GetConnectionString("LocalDb");

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<ShopDbContext>(options =>
    options.UseSqlServer(connectionString)
);

// fluent validators
builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddFluentValidationClientsideAdapters();
builder.Services.AddValidatorsFromAssemblies(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddAutoMapper(typeof(AppProfile));

// custom services
builder.Services.AddScoped<IProductsService, ProductsService>();

// exception handlers
builder.Services.AddExceptionHandler<HttpExceptionHandler>();
builder.Services.AddProblemDetails();

var app = builder.Build();

//app.Use(async (ctx, next) =>
//{
//    Console.WriteLine("Middleware is invoked!");
//    await next(ctx);
//});

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//app.UseMiddleware<ErrorHandlerMiddleware>();
app.UseExceptionHandler();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
