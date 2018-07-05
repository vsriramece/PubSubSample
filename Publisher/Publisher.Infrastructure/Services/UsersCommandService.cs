using System;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Publisher.Infrastructure.DTO.Events;
using Publisher.Infrastructure.DTO.Request;
using Publisher.Infrastructure.Messaging;

namespace Publisher.Infrastructure.Services
{
    public class UsersCommandService : IUsersCommandService
    {
        private readonly IMessagingClient MessagingClient;
        public UsersCommandService(IMessagingClient messagingClient)
        {
            MessagingClient = messagingClient;
        }

        public async Task<Guid> Create(CreateUser request)
        {
            // To do - Make the necessary repository/domain behavior calls
            // Publish the event
            UserCreated domainEvent = new UserCreated() {
                Id = Guid.NewGuid(),
                FirstName = request.FirstName,
                LastName = request.LastName,
                Tenant = request.Tenant};
            await MessagingClient.PublishMessage(JsonConvert.SerializeObject(domainEvent), request.Tenant);
            return domainEvent.Id;
        }
    }
}
