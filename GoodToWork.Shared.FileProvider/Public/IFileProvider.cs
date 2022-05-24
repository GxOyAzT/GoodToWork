namespace GoodToWork.Shared.FileProvider;

public interface IFileProvider
{
    Task<string> SaveImage(string src, string fileName);
}
