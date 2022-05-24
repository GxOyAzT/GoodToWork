namespace GoodToWork.TasksOrganizer.Infrastructure.Utilities.UrlDeserialization;

internal class UrlDeserializer : IUrlDeserializer
{
    public string GetUserIdFromUrl(string url)
    {
        return url.Substring(url.IndexOf("/api/") + 5, 36);
    }
}
