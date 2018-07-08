using AutoFixture.Xunit2;
using FluentAssertions;
using Moq;
using Publisher.Infrastructure.DTO.Request;
using Publisher.Infrastructure.Messaging;
using Publisher.Infrastructure.Services;
using Publisher.Tests.Utilities;
using System;
using System.Threading.Tasks;
using Xunit;

namespace Publisher.Tests.Infrastructure.Services
{
    public class UsersCommandServiceTests
    {
        [Theory, AutoMoqData]
        public async Task UserCommandService_CreateUser_Success([Frozen]Mock<IMessagingClient> messagingClient,
            CreateUser request,
            UsersCommandService sut)
        {
            // Arrange
            messagingClient.Setup(o => o.PublishMessage(It.IsAny<string>(), request.Tenant)).ReturnsAsync(true);
            // Act
            var response = await sut.Create(request);
            //Assert
            response.Should().NotBe(Guid.Empty, "because id should be returned after successful user creation");
            messagingClient.Verify(o=>o.PublishMessage(It.IsAny<string>(),request.Tenant),Times.Once,"because a domain event should be published as part of user creation");
        }
    }
}
