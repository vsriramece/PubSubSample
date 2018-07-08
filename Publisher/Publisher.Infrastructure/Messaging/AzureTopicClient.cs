using Microsoft.Azure.ServiceBus;
using System;
using System.Text;
using System.Threading.Tasks;

namespace Publisher.Infrastructure.Messaging
{
    public class AzureTopicClient : IMessagingClient
    {
        private readonly ITopicClient _topicClient;
        private const string TENANTIDENTIFIER = "Tenant";
        public AzureTopicClient(ITopicClient topicClient)
        {
            _topicClient = topicClient;
        }

        public async Task<bool> PublishMessage(string jsonData, string tenant)
        {
            if (string.IsNullOrWhiteSpace(jsonData))
            {
                // This can be modified to a custom exception
                throw new Exception("Message cannot be empty");
            }

            if (string.IsNullOrWhiteSpace(tenant))
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
