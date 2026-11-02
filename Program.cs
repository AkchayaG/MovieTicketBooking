using Microsoft.EntityFrameworkCore;
using MovieTicketBooking.Data;
using MovieTicketBooking.Interfaces;
using MovieTicketBooking.Repositories;
using MovieTicketBooking.Repositories.Interfaces;
using MovieTicketBooking.Services;
using MovieTicketBooking.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// DbContext Registration
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Repositories Injection
builder.Services.AddScoped<IMovieRepository, MovieRepository>();

// Services Injection
builder.Services.AddScoped<IMovieService, MovieService>();

builder.Services.AddScoped<ITheaterRepository, TheaterRepository>();
builder.Services.AddScoped<ITheaterService, TheaterService>();

builder.Services.AddScoped<IScreenRepository, ScreenRepository>();
builder.Services.AddScoped<IScreenService, ScreenService>();

builder.Services.AddScoped<ISeatRepository, SeatRepository>();
builder.Services.AddScoped<ISeatService, SeatService>();

builder.Services.AddScoped<IShowRepository, ShowRepository>();
builder.Services.AddScoped<IShowService, ShowService>();

builder.Services.AddScoped<IBookingRepository, BookingRepository>();
builder.Services.AddScoped<IBookingService, BookingService>();


builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();