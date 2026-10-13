
using StargateAPI.Business.Commands;
using Xunit;
namespace api.Tests;

public class UnitTest1
{
    [Fact]
    public void Test1()
    {
        var sut = new CreateAstronautDuty()
        {
            Name = "Neil Armstrong",
            Rank = "Commander",
            DutyTitle = "Moon Landing Duty"
        };

        Assert.NotNull(sut);
    }
}
