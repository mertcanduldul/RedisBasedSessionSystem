using RedisBasedAuthSystem.Repositories;
using Microsoft.EntityFrameworkCore;
using RedisBasedAuthSystem.Services;
using RedisBasedSessionSystem.Context;
using RedisBasedSessionSystem.Entities;
using StackExchange.Redis;

var builder = WebApplication.CreateBuilder(args);

//Builder
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSingleton<IConnectionMultiplexer>(sp =>
{
    var config = builder.Configuration["Redis:Host"] + ":" + builder.Configuration["Redis:Port"];
    return ConnectionMultiplexer.Connect(config);
});

// Redis Repository
builder.Services.AddScoped<IRepository<MenuRecord>, RedisRepository<MenuRecord>>();
builder.Services.AddScoped<IRepository<SessionRecord>, RedisRepository<SessionRecord>>();


// MSSQL Repository
builder.Services.AddScoped<IRepository<MenuRecord>, SqlRepository<MenuRecord>>();
builder.Services.AddScoped<IRepository<SessionRecord>, SqlRepository<SessionRecord>>();

// Servisler
builder.Services.AddScoped<MenuService>();
builder.Services.AddScoped<SessionService>();

builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

var app = builder.Build();


//App
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

if (app.Environment.IsProduction())
{
    app.UseHttpsRedirection();
}

app.UseRouting();
app.UseEndpoints(p => { p.MapControllers(); });

app.Run();