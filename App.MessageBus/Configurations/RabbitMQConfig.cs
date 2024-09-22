using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace App.MessageBus.Configurations
{
    public class RabbitMQConfig
    {
        public string? HostName { get; set; }
        public string? Username { get; set; }
        public string? Password { get; set; }
    }
}