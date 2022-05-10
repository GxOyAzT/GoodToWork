namespace GoodToWork.Shared.FileProvider.Utilities;

public class Configuration :
    IWithPrivateKey,
    IWithUrlEndPoint,
    IBuild
{
    public string PublicKey { get; set; }
    public string PrivateKey { get; set; }
    public string UrlEndPoint { get; set; }

    private Configuration(string publicKey) => PublicKey = publicKey;

    public static IWithPrivateKey CreateWithPublicKey(string publicKey)
    {
        return new Configuration(publicKey);
    }

    public IWithUrlEndPoint WithPrivateKey(string privateKey)
    {
        PrivateKey = privateKey;
        return this;
    }

    public IBuild WithUrlEndPoint(string url)
    {
        UrlEndPoint = url;
        return this;
    }

    public Configuration Build()
    {
        return this;
    }
}

public interface IWithPrivateKey
{
    IWithUrlEndPoint WithPrivateKey(string privateKey);
}

public interface IWithUrlEndPoint
{
    IBuild WithUrlEndPoint(string url);
}

public interface IBuild
{
    Configuration Build();
}