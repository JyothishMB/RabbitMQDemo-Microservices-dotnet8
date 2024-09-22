// See https://aka.ms/new-console-template for more information
using App.MessageBus;
using App.MessageBus.Congigurations;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Configuration;
using App.MessageBus.Interfaces;

Console.WriteLine("Hello, World!");


//MessageBus messageBus = new App.MessageBus.MessageBus();
IHostApplicationBuilder builder = Host.CreateApplicationBuilder();

IConfigurationRoot config = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json")
    .Build();

builder.Services.Configure<RabbitMQConfig>(config.GetRequiredSection("RabbitMQ"));
builder.Services.AddScoped(typeof(IMessageBus<>), typeof(MessageBus<>));

