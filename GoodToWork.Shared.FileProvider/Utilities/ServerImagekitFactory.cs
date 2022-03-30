using Imagekit;

namespace GoodToWork.Shared.FileProvider.Utilities;

internal class ServerImagekitFactory
{
    public static ServerImagekit Create(Configuration configuration) =>
        new ServerImagekit(configuration.PublicKey, configuration.PrivateKey, configuration.UrlEndPoint);
}
