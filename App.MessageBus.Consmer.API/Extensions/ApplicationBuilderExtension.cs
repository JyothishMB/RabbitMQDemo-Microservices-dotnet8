using App.MessageBus.Consumer.API.Interfaces;
using System.Reflection.Metadata;

namespace App.MessageBus.Consumer.API.Extensions
{
    public static class ApplicationBuilderExtension
    {
        private static IMessageBusConsumer MessageBusConsumer { get; set; }
        public static IApplicationBuilder UseRabbitMQMessageBusConsumer(this IApplicationBuilder app)
        {
            MessageBusConsumer = app.ApplicationServices.GetRequiredService<IMessageBusConsumer>();
            var hostApplicationLife = app.ApplicationServices.GetService<IHostApplicationLifetime>();

            hostApplicationLife.ApplicationStarted.Register(OnStart);
            hostApplicationLife.ApplicationStopping.Register(OnStop);

            return app;
        }
        private static void OnStop()
        {
            MessageBusConsumer.Stop();
        }

        private static void OnStart()
        {
            MessageBusConsumer.Start();
        }
    }
}
