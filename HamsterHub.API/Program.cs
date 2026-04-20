using HamsterHub.Application.Services;
using HamsterHub.Domain.Interfaces;
using HamsterHub.Infrastructure;
using HamsterHub.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();


// ORSAK: Registrerar min DbContext i DI-containern (scoped livslängd)
builder.Services.AddDbContext<HamsterDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// ORSAK: Skapar scope för mina Repositories och services

// Repositories -----
builder.Services.AddScoped<IHamsterRepository, HamsterRepository>();
builder.Services.AddScoped<IBookingRepository, BookingRepository>();

// Services -----
builder.Services.AddScoped<IHamsterService, HamsterService>();
builder.Services.AddScoped<IBookingService, BookingService>();

// ORSAK: Cors för att anväda React 
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowReactApp", policy =>
    {
        policy.WithOrigins("http://localhost:5173")
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});

//ORSAK: Skapar app-delen och middleware

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseCors("AllowReactApp");
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers(); // UseRouting ingår här numera, därav ingen separat app.UseRouting.

app.Run();



















// // Add services to the container.
// // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
// builder.Services.AddOpenApi();
//
//
//
// var app = builder.Build();
//
// // Configure the HTTP request pipeline.
// if (app.Environment.IsDevelopment())
// {
//     app.MapOpenApi();
// }
//
// app.UseHttpsRedirection();
//
//
//
// app.Run();


