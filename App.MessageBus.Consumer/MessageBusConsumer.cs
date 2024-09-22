using App.MessageBus.Congigurations;
using App.MessageBus.Consumer.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;

namespace App.MessageBus.Consumer
{
    public class MessageBusConsumer : IMessageBusConsumer
    {
        private readonly RabbitMQConfig _rabbitMqconfig;
        private readonly IConfiguration configuration;
        private EventingBasicConsumer _consumer;
        private IModel _channel;
        private IConnection _connection;
        private string queuename;

        public MessageBusConsumer(IConfiguration configuration, IOptions<RabbitMQConfig> rabbitMqSetting)
        {
            this._rabbitMqconfig = rabbitMqSetting.Value;
            this.configuration = configuration;
            this.queuename = configuration.GetRequiredSection("NewUserQueue:NewUserQueue").Value;

            var factory = new ConnectionFactory
            {
                HostName = _rabbitMqconfig.HostName,
                UserName = _rabbitMqconfig.Username,
                Password = _rabbitMqconfig.Password
            };
            _connection = factory.CreateConnection();
            _channel = _connection.CreateModel();

            _channel.QueueDeclare(
                queue: queuename, 
                durable: false, 
                exclusive: false, 
                autoDelete: false, 
                arguments: null
             );

            _consumer = new EventingBasicConsumer(_channel);
            
        }
        public async Task Start()
        {
            _consumer.Received += OnNewUserRegistered;
            await Task.Run(() => {
                _channel.BasicConsume(queue: queuename, autoAck: false, consumer: _consumer);
            });
        }

        public Task Stop()
        {
            _consumer.Shutdown += onShutDown;
            throw new NotImplementedException();
        }

        private void onShutDown(object? sender, ShutdownEventArgs e)
        {
            _channel.Close();
            _connection.Close();
        }

        private void OnNewUserRegistered(object? sender, BasicDeliverEventArgs ea)
        {
            var body = ea.Body.ToArray();
            var message = Encoding.UTF8.GetString(body);

            bool processedSuccessfully = false;
            //try
            //{
            //    processedSuccessfully = await ProcessMessageAsync(message);
            //}
            //catch (Exception ex)
            //{
            //    _logger.LogError($"Exception occurred while processing message from queue {queueName}: {ex}");
            //}

            //if (processedSuccessfully)
            //{
            //    _channel.BasicAck(deliveryTag: ea.DeliveryTag, multiple: false);
            //}
            //else
            //{
            //    _channel.BasicReject(deliveryTag: ea.DeliveryTag, requeue: true);
            //}
        }
    }
}
