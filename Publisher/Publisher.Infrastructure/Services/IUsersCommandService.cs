using Publisher.Infrastructure.DTO.Request;
using System;
using System.Threading.Tasks;

namespace Publisher.Infrastructure.Services
{
    public interface IUsersCommandService
    {
        Task<Guid> Create(CreateUser request);
    }
}
