namespace JDev.Tuteee.Rest.Api.Tests;

using CustomTypes;

public class ClientTests
{
    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public void Test1()
    {
        var today = DateOnly.FromDateTime(DateTime.Today).ToString("D");
        // var now = TimeOnly.FromDateTime(DateTime.Now).ToString("t");
        var now = TimeOnly.FromDateTime(DateTime.Now).ToString("h:mm:ss tt zz");
        var x = new PhoneNumber { Raw = "07786 548 235" };
        var y = x.ToDisplay();

        
    }
}