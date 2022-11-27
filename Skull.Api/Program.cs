using Microsoft.AspNetCore.SignalR;
using Skull;
using Skull.Api;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSingleton<IGameStateRepository, SingleGameInMemoryGameStateRepository>();
builder.Services.AddSingleton<ITableRepository, SingleTableInMemoryTableRepository>();
builder.Services.AddSingleton<ITableFactory>((ctx) => new TableFactory(File.ReadAllLines(".\\Lists\\adjectives.txt"), File.ReadAllLines(".\\Lists\\occupations.txt")));
builder.Services.AddTransient<ISkullGame, SkullGame>();
builder.Services.AddSignalR();
builder.Services.AddSingleton<ISkullHub, SkullHub>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseCors(c => {
        c.AllowAnyOrigin();
        c.WithHeaders("content-type");
    });
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.MapHub<SkullHub>("/gameHub").RequireCors(builder => builder.AllowAnyOrigin().WithHeaders("x-requested-with", "x-signalr-user-agent"));

app.Run();
