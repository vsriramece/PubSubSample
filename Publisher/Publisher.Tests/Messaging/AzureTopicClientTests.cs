using FluentAssertions;
using Publisher.Infrastructure.Messaging;
using Publisher.Tests.Utilities;
using System;
using Xunit;

namespace Publisher.Tests.Messaging
{
    public class AzureTopicClientTests
    {
        [Theory]
        [InlineAutoMoqData(null)]
        [InlineAutoMoqData("")]
        [InlineAutoMoqData("   ")]
        public void AzureTopicClient_ThrowsException_EmptyJsonData(string jsonData, AzureTopicClient sut)
        {
            // Act
            Action act = () => sut.PublishMessage(jsonData, "tenant").GetAwaiter().GetResult();
            //Assert
            act.Should().Throw<Exception>($"because json data field is invalid").And.Message.Contains("Message cannot be empty");
        }

        [Theory]
        [InlineAutoMoqData(null)]
        [InlineAutoMoqData("")]
        [InlineAutoMoqData("   ")]
        public void AzureTopicClient_ThrowsException_EmptyTenant(string tenant,AzureTopicClient sut)
        {
            // Act
            Action act = () => sut.PublishMessage("MessageData", tenant).GetAwaiter().GetResult() ;
            //Assert
            act.Should().Throw<Exception>($"because tenant field is invalid").And.Message.Contains("Tenant cannot be empty");
        }
    }
}
