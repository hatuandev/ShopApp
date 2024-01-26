namespace OpenIddictAPI.ConfigurationOptions;

public class AppSettings
{
    public OpenIddictServer OpenIddictServer { get; set; }
    public GatewayApi GatewayApi { get; set; }
}

public class OpenIddictServer
{
    public string ClientId { get; set; }

    public string ClientSecret { get; set; }

    public string Host { get; set; }

    public string SecurityKey { get; set; }
}

public class GatewayApi
{
    public string Host { get; set; }
}