using System.Threading.Tasks;

namespace Publisher.Infrastructure.Messaging
{
    public interface IMessagingClient
    {
        Task<bool> PublishMessage(string jsonData, string tenant);
    }
}
