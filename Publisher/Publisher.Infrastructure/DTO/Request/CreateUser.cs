using System.ComponentModel.DataAnnotations;

namespace Publisher.Infrastructure.DTO.Request
{
    public class CreateUser
    {
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string Tenant { get; set; }
    }
}
