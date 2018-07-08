using Microsoft.Azure.ServiceBus;
using System;
using System.Threading.Tasks;

namespace Subscriber.ConsoleApp.Infrastructure.Exception
{
    public class ServiceBusManagement
    {
        public static Task ExceptionReceivedHandler(ExceptionReceivedEventArgs exceptionReceivedEventArgs)
        {
            Console.WriteLine($"Message handler encountered an exception {exceptionReceivedEventArgs.Exception}.");
            return Task.CompletedTask;
        }
    }
}
