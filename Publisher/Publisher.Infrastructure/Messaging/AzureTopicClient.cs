using Microsoft.Azure.ServiceBus;
using System;
using System.Text;
using System.Threading.Tasks;

namespace Publisher.Infrastructure.Messaging
{
    public class AzureTopicClient : IMessagingClient
    {
        private readonly TopicClient _topicClient;
        private const string TENANTIDENTIFIER = "Tenant";
        public AzureTopicClient(string connectionstring, string topicName)
        {
            _topicClient = new TopicClient(connectionstring, topicName, RetryPolicy.Default);
        }

        public async Task<bool> PublishMessage(string jsonData, string tenant)
        {
            if(string.IsNullOrEmpty(tenant))
            {
                // This can be modified to a custom exception
                throw new Exception("Tenant cannot be empty");
            }

            Message brokeredMsg = new Message(Encoding.UTF8.GetBytes(jsonData));
            // Add the custom property which can be used for filtering
            brokeredMsg.UserProperties.Add(TENANTIDENTIFIER, tenant);
            await _topicClient.SendAsync(brokeredMsg);
            return true;
        }
    }
}
