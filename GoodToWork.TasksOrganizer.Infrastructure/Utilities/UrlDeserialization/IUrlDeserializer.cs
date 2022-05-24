namespace GoodToWork.TasksOrganizer.Infrastructure.Utilities.UrlDeserialization;

public interface IUrlDeserializer
{
    string GetUserIdFromUrl(string url);
}
