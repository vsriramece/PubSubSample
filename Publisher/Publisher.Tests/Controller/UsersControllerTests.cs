using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Publisher.Infrastructure.DTO.Request;
using Publisher.Infrastructure.DTO.Response;
using Publisher.Infrastructure.Services;
using Publisher.Tests.Utilities;
using Publisher.WebService.Controllers;
using System;
using System.Threading.Tasks;
using Xunit;

namespace Publisher.Tests.Controller
{
    public class UsersControllerTests
    {
        [Theory, AutoMoqData]
        public async Task UsersController_POST_CreateUser_Success(Mock<IUsersCommandService> commandService,
            CreateUser request,
            Guid userId)
        {
            // Arrange
            commandService.Setup(o => o.Create(request)).Returns(Task.FromResult(userId));
            UsersController sut = new UsersController(commandService.Object);

            // Act
            var response = await sut.CreateUser(request) as OkObjectResult;

            // Assert
            response.StatusCode.Value.Should().Be(200, "because the request is successful");
            ((CreateUserResponse)response.Value).UserId.Should().Be(userId, "because id should match the command service return value");
        }

        [Theory, AutoMoqData]
        public async Task UsersController_POST_CreateUser_BadRequest(Mock<IUsersCommandService> commandService,
           CreateUser request)
        {
            // Arrange
            UsersController sut = new UsersController(commandService.Object);
            sut.ModelState.AddModelError("Test", "Mock error");

            // Act
            var response = await sut.CreateUser(request) as StatusCodeResult;

            // Assert
            response.StatusCode.Should().Be(400, "because the request model is invalid");
        }

        [Theory, AutoMoqData]
        public async Task UsersController_POST_CreateUser_InternalServerError(Mock<IUsersCommandService> commandService,
          CreateUser request)
        {
            // Arrange
            commandService.Setup(o => o.Create(request)).Throws(new Exception("Command Service exception"));
            UsersController sut = new UsersController(commandService.Object);

            // Act
            var response = await sut.CreateUser(request) as StatusCodeResult;

            // Assert
            response.StatusCode.Should().Be(500, "because the exception from command service should be an internal server error");
        }
    }
}
