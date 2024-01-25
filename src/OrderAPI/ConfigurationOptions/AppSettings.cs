using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Diagnostics.Internal;

namespace OrderAPI.ConfigurationOptions;
public class AppSettings
{
    public IdentityServerAuthentication IdentityServerAuthentication { get; set; }
}

public class IdentityServerAuthentication
{
    public string SecurityKey { get; set; }

    public string Authority { get; set; }

    public string ApiName { get; set; }

    public bool RequireHttpsMetadata { get; set; }
}
