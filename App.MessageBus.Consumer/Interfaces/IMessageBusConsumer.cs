﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.MessageBus.Consumer.Interfaces
{
    public interface IMessageBusConsumer
    {
        Task Start();
        Task Stop();
    }
}
