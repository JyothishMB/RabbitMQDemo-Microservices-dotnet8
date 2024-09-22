using App.MessageBus;
using App.MessageBus.Configurations;
using App.MessageBus.Interfaces;
using App.Messaging.Publisher.API.Helpers.Messaging.MessageTypes;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var Configuration = builder.Configuration;

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.Configure<RabbitMQConfig>(Configuration.GetRequiredSection("RabbitMQ"));
builder.Services.AddScoped<IMessageBus<UserRegistationNotification>, MessageBus<UserRegistationNotification>>();

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

app.Run();
