using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Publisher.Infrastructure.DTO.Request;
using Publisher.Infrastructure.DTO.Response;
using Publisher.Infrastructure.Services;

namespace Publisher.WebService.Controllers
{
    [Route("api/[controller]")]
    public class UsersController : Controller
    {
        private readonly IUsersCommandService CommandService;
        public UsersController(IUsersCommandService commandService)
        {
            CommandService = commandService;
        }

        // POST api/users
        [HttpPost]
        public async Task<IActionResult> CreateUser([FromBody]CreateUser request)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return StatusCode(StatusCodes.Status400BadRequest);
                }
                var userId = await CommandService.Create(request) ;
                return Ok(new CreateUserResponse { UserId = userId});
            }
            catch(Exception ex)
            {
                // To do - Handle exceptions, log it and throw appropriate errors
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}
