using Microsoft.Azure.ServiceBus;
using Microsoft.Extensions.Configuration;
using Subscriber.ConsoleApp.Infrastructure.Exception;
using Subscriber.ConsoleApp.MessageProcessors;
using System;
using System.Threading.Tasks;

namespace Subscriber.ConsoleApp
{
    class Program
    {
        static async Task Main(string[] args)
        {
            IConfiguration config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();
            string serviceBusConnectionString = config["ServiceBusConnectionString"];
            string topicName = config["TopicName"];
            string subscriptionName = config["SubscriptionName"];
            var subscriptionClient = new SubscriptionClient(serviceBusConnectionString, topicName, subscriptionName);
            subscriptionClient.RegisterMessageHandler(UserProcessor.ProcessUserEvents, ServiceBusManagement.ExceptionReceivedHandler);

            Console.WriteLine($"Service bus client registered for subscription: {subscriptionName}. Waiting for a message. Press any key to exit");
            Console.ReadKey();
            await subscriptionClient.CloseAsync();
        }

        
    }
}
