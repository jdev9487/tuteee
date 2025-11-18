namespace JDev.Tuteee.Rest.Api.Tests;

using System.Security.Cryptography;
using System.Text;
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
        using var sha512 = SHA512.Create();
        var bytes = Encoding.UTF8.GetBytes("Matthew Lay");
        var hash = sha512.ComputeHash(bytes);
        var red = (int)hash[0];
        var green = (int)hash[1];
        var blue = (int)hash[2];
        var today = DateOnly.FromDateTime(DateTime.Today).ToString("yyyy-MM-dd");
    }
}