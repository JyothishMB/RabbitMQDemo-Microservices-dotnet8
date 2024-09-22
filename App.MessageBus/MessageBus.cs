using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using App.MessageBus.Configurations;
using App.MessageBus.Interfaces;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using RabbitMQ.Client;

namespace App.MessageBus
{
    public class MessageBus<T> : IMessageBus<T>
    {
        private readonly RabbitMQConfig _rabbitMqSetting;
        public MessageBus(IOptions<RabbitMQConfig> rabbitMqSetting)
        {
            _rabbitMqSetting = rabbitMqSetting.Value;
        }

        public async Task PublishMessage(T message, string topic_queue_name)
        {
            var factory = new ConnectionFactory { 
                HostName = _rabbitMqSetting.HostName,
                UserName = _rabbitMqSetting.Username,
                Password = _rabbitMqSetting.Password
             };
            using var connection = factory.CreateConnection();
            using var channel = connection.CreateModel();

            channel.QueueDeclare(queue: topic_queue_name,
                     durable: false,
                     exclusive: false,
                     autoDelete: false,
                     arguments: null);

            var jsonMessage = JsonConvert.SerializeObject(message);
            var body = Encoding.UTF8.GetBytes(jsonMessage);
            await Task.Run(() => channel.BasicPublish(exchange: string.Empty,
                                routingKey: topic_queue_name,
                                basicProperties: null,
                                body: body));
            Console.WriteLine($" [x] Sent {message}"); 
        }
    }
}