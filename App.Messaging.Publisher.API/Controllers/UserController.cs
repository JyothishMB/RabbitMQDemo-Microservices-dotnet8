using App.MessageBus.Interfaces;
using App.Messaging.Publisher.API.Helpers.Messaging.MessageTypes;
using App.Messaging.Publisher.API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace App.Messaging.Publisher.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IMessageBus<UserRegistationNotification> messageBus;

        public UserController(IMessageBus<UserRegistationNotification> messageBus) 
        {
            this.messageBus = messageBus;
        }

        [HttpPost]
        public async Task<ActionResult> CreateUser([FromBody] NewUserDto newUserDto)
        {
            /*
             * User registration logic here.
             * Preferably do with a service.
             * Its not a good practice to do the business logic in controller.
             * This is done just for quick explanation.
             */
            

            //After user registration logic we need to write the message to the RabbiMQ Queue
            var message= new UserRegistationNotification() { 
                Email = newUserDto.Email, 
                Username = newUserDto.Name 
            };
            messageBus.PublishMessage(message, "NewUserQueue");
            return Ok(newUserDto);
        }
    }
}
