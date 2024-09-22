using App.MessageBus.Configurations;
using App.MessageBus.Consumer.API.Extensions;
using App.MessageBus.Consumer.API;
using App.MessageBus.Consumer.API.Interfaces;
using Microsoft.Extensions.Configuration;

var builder = WebApplication.CreateBuilder(args);
var Configuration = builder.Configuration;

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.Configure<RabbitMQConfig>(Configuration.GetRequiredSection("RabbitMQ"));
builder.Services.AddSingleton<IMessageBusConsumer, MessageBusConsumer>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();
app.UseRabbitMQMessageBusConsumer();

app.Run();
