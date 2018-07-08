
using Microsoft.Azure.ServiceBus;
using Newtonsoft.Json;
using Subscriber.ConsoleApp.Infrastructure.DTO;
using System;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Subscriber.ConsoleApp.MessageProcessors
{
    public class UserProcessor
    {
        public static Task ProcessUserEvents(Message message, CancellationToken token)
        {
            string msgData = Encoding.UTF8.GetString(message.Body);
            var user = JsonConvert.DeserializeObject<User>(msgData);
            Console.WriteLine($"Event received for User with Id:{user.Id}, FirstName:{user.FirstName}, LastName: {user.LastName}");
            return Task.CompletedTask;
        }
    }
}
