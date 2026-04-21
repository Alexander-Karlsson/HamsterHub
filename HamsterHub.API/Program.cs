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
builder.Services.AddScoped<IReviewRepository, ReviewRepository>();

// Services -----
builder.Services.AddScoped<IHamsterService, HamsterService>();
builder.Services.AddScoped<IBookingService, BookingService>();
builder.Services.AddScoped<IReviewService, ReviewService>();


builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        // ORSAK: Serialiserar enums som strängar i JSON istället för siffror.
        options.JsonSerializerOptions.Converters.Add(
            new System.Text.Json.Serialization.JsonStringEnumConverter());
    });

//ORSAK: Skapar app-delen och middleware

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseCors("AllowAll");
// app.UseHttpsRedirection();
// app.UseAuthorization();
app.MapControllers(); // UseRouting ingår här numera, därav ingen separat app.UseRouting.

app.Run();





















