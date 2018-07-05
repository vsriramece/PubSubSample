using System;

namespace Publisher.Infrastructure.DTO.Events
{
    public class UserCreated
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Tenant { get; set; }
    }
}
