using GoodToWork.Shared.FileProvider.Utilities;
using Imagekit;

namespace GoodToWork.Shared.FileProvider.Implementations;

internal class FileProvider : IFileProvider
{
    private readonly ServerImagekit _serverImagekit;

    public FileProvider(Configuration configuration)
    {
        _serverImagekit = ServerImagekitFactory.Create(configuration);
    }

    public async Task<string> SaveImage(string src, string fileName)
    {
        var response = await _serverImagekit
            .FileName(fileName)
            .UploadAsync(src);

        return response.FilePath;
    }
}
