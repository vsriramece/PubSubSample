using FluentAssertions;
using Publisher.Infrastructure.DTO.Request;
using Publisher.Tests.Utilities;
using System.Linq;
using Xunit;

namespace Publisher.Tests.Controller.RequestModel
{
    public class CreateUserTests
    {
        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("   ")]
        public void CreateUserModel_Invalid_FirstName(string firstName)
        {
            // Arrange
            var model = new CreateUser();
            model.FirstName = firstName;
            // Act
            var results = ModelValidator.Validate(model).ToList();
            // Assert
            results.Count(o => o.MemberNames.Contains(nameof(CreateUser.FirstName))).Should().Be(1,"because Firstname field is invalid");
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("   ")]
        public void CreateUserModel_Invalid_LastName(string lastName)
        {
            // Arrange
            var model = new CreateUser();
            model.LastName = lastName;
            // Act
            var results = ModelValidator.Validate(model).ToList();
            // Assert
            results.Count(o => o.MemberNames.Contains(nameof(CreateUser.LastName))).Should().Be(1, "because Lastname field is invalid");
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("   ")]
        public void CreateUserModel_Invalid_Tenant(string tenant)
        {
            // Arrange
            var model = new CreateUser();
            model.Tenant = tenant;
            // Act
            var results = ModelValidator.Validate(model).ToList();
            // Assert
            results.Count(o => o.MemberNames.Contains(nameof(CreateUser.Tenant))).Should().Be(1, "because tenant field is invalid");
        }
    }
}
