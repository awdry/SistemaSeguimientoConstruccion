using Microsoft.EntityFrameworkCore;
using SeguimientoConstruccion.Application.Services;
using SeguimientoConstruccion.Infrastructure.Context;
using SeguimientoConstruccion.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Add services to the container.
builder.Services.AddScoped<ObraRepository>();
builder.Services.AddScoped<TareaRepository>();
builder.Services.AddScoped<UnitOfWork>();
builder.Services.AddScoped<ObraService>();
builder.Services.AddScoped<TareaService>();
builder.Services.AddScoped<ResponsableRepository>();
builder.Services.AddScoped<ResponsableService>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowReact", policy =>
    {
        policy.WithOrigins("http://localhost:5173", "http://localhost:5174")
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

var app = builder.Build();

app.UseCors("AllowReact");

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
