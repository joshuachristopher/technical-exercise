using Microsoft.EntityFrameworkCore;
using MockQueryable;
using MockQueryable.Moq;
using Moq;
using Moq.EntityFrameworkCore;
using StargateAPI.Business.Interfaces;
using StargateAPI.Business.Services;
using StargateAPI.Data;
using StargateAPI.Shared.Dtos;
using System.Threading.Tasks;
using System.Xml;
using Xunit;

namespace api.Tests.Business
{
    public class PersonServiceTest
    {
        [Fact]
        public void SmokeTest()
        {
            var mockContext = new Mock<StargateContext>();
            var actual = new PersonService(mockContext.Object);
            var expected = typeof(IPersonService);

            Assert.IsAssignableFrom(expected, actual);
        }

        [Fact]
        public async Task PersonIsUniquelyIdentifiedByTheirName_ReturnsTrue()
        {
            // Arrange
            IList<Person> people = new List<Person>();
            var options = new DbContextOptions<StargateContext>();
            var mockContext = new Mock<StargateContext>(options);
            mockContext.SetupGet(x => x.People).ReturnsDbSet(people);
            var SUT = new PersonService(mockContext.Object);

            var name = "John Doe";

            // Act
            var result = await SUT.IsPersonUnique(name);

            // Assert
            Assert.True(result);

        }

        [Fact]
        public async Task PersonIsUniquelyIdentifiedByTheirName_ReturnsFalse()
        {
            // Arrange
            IList<Person> people = new List<Person> { new Person { Name = "John Doe" } };
            var options = new DbContextOptions<StargateContext>();
            var mockContext = new Mock<StargateContext>(options);
            mockContext.SetupGet(x => x.People).ReturnsDbSet(people);
            var SUT = new PersonService(mockContext.Object);

            var name = "John Doe";

            // Act
            var result = await SUT.IsPersonUnique(name);

            // Assert
            Assert.False(result);
        }
    }
}
