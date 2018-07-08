using Microsoft.Azure.WebJobs;
using Newtonsoft.Json;
using Subscriber.AzureFunction.Infrastructure.DTO;
using System;

namespace Subscriber.AzureFunction
{
    public class MessageProcessor
    {
        [FunctionName("ProcessUserEvents")]
        public static void ProcessUserEvents([ServiceBusTrigger("users","tenanta")] string message)
        {
            var user = JsonConvert.DeserializeObject<User>(message);
            Console.WriteLine($"Event received for User with Id:{user.Id}, FirstName:{user.FirstName}, LastName: {user.LastName}");
        }
    }
}
