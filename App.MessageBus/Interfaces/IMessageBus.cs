using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace App.MessageBus.Interfaces
{
    public interface IMessageBus<T>
    {
        Task PublishMessage(T message, string topic_queue_name);
    }
}