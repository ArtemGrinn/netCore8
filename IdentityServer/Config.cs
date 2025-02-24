using Duende.IdentityModel;
using Duende.IdentityServer.Models;

namespace IdentityServer;

public static class Config
{
    public static IEnumerable<IdentityResource> IdentityResources =>
        new IdentityResource[]
        {
            new IdentityResources.OpenId(),
            new IdentityResources.Profile().AddToProfile(),
        };

    public static IEnumerable<ApiScope> ApiScopes =>
        new ApiScope[]
        {
            new ApiScope("scope1"),
            new ApiScope("scope2"),
        };

    public static IEnumerable<Client> Clients =>
        new Client[]
        {
            new Client
            {
                ClientId = "toDoApp",
                ClientSecrets = { new Secret("secret".Sha256()) }, 
                AllowedGrantTypes = GrantTypes.Code,
                RedirectUris = { "https://localhost:7088/signin-oidc" },
                FrontChannelLogoutUri = "https://localhost:7088/signout-oidc",
                PostLogoutRedirectUris = { "https://localhost:7088/signout-callback-oidc" },
                AllowOfflineAccess = true,
                AllowedScopes = { "openid", "profile" }
            }
        };
    
    private static IdentityResource AddToProfile(this IdentityResource identityResource)
    {
        identityResource.UserClaims.Add(JwtClaimTypes.Email);
        return identityResource;
    }
}