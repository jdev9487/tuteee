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
        var x = new PhoneNumber { Raw = "07786 548 235" };
        var y = x.ToDisplay();
    }
}