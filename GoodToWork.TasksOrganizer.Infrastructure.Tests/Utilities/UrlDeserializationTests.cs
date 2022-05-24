using GoodToWork.TasksOrganizer.Infrastructure.Utilities.UrlDeserialization;
using Xunit;

namespace GoodToWork.TasksOrganizer.Infrastructure.Tests.Utilities;

public class UrlDeserializationTests
{
    [Fact]
    public void TestA()
    {
        var urlDeserializer = new UrlDeserializer();

        var userId = urlDeserializer.GetUserIdFromUrl("https://localhost:5000/api/105e9e91-21c8-43a8-ab51-20ec7e158e0c");

        Assert.Equal("105e9e91-21c8-43a8-ab51-20ec7e158e0c", userId);
    }

    [Fact]
    public void TestB()
    {
        var urlDeserializer = new UrlDeserializer();

        var userId = urlDeserializer.GetUserIdFromUrl("https://localhost:5000/api/105e9e91-21c8-43a8-ab51-20ec7e158e0c/12b3537a-dea4-45f1-91ee-8fbcf1b3775f");

        Assert.Equal("105e9e91-21c8-43a8-ab51-20ec7e158e0c", userId);
    }

    [Fact]
    public void TestC()
    {
        var urlDeserializer = new UrlDeserializer();

        var userId = urlDeserializer.GetUserIdFromUrl("https://localhost:5000/api/105e9e91-21c8-43a8-ab51-20ec7e158e0c/");

        Assert.Equal("105e9e91-21c8-43a8-ab51-20ec7e158e0c", userId);
    }
}
