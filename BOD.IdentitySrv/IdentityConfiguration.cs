using System.Security.Claims;
using IdentityServer4.Models;
using IdentityServer4.Test;
using IdentityModel;

namespace BOD.IdentitySrv;

//https://github.com/innovativeinstitution/BankOfDotNet/blob/master/BankOfDotNet.ConsoleClient/Program.cs
public class IdentityConfiguration
{
    static string scope_1 = "api.read";
    static string scope_2 = "api.write";
    public static List<TestUser> TestUsers => new List<TestUser>
{
        new TestUser
        {
            SubjectId = "123",
            Username = "Gowtham",
            Password = "Test@123",
            Claims =
            {
                new Claim(JwtClaimTypes.Name, "Gowtham K"),
                new Claim(JwtClaimTypes.GivenName, "Gowtham"),
                new Claim(JwtClaimTypes.FamilyName, "Kumar"),
                new Claim(JwtClaimTypes.WebSite, "https://gowthamcbe.com/"),
            }
        },new TestUser
        {
            SubjectId = "2",
            Username = "Zeeshan",
            Password = "zeeshan@123",
            Claims =
            {
                new Claim(JwtClaimTypes.Name, "Zeeshan"),
                new Claim(JwtClaimTypes.GivenName, "Zee"),
                new Claim(JwtClaimTypes.FamilyName, "Asghar"),
                new Claim(JwtClaimTypes.WebSite, "https://XYZ.com/"),
            }
        }
};
    public static IEnumerable<IdentityResource> IdentityResources => new IdentityResource[]
    {
        new IdentityResources.OpenId(),
        new IdentityResources.Profile(),
    };
    public static IEnumerable<ApiScope> ApiScopes => new ApiScope[]
{
        new ApiScope(scope_1),
        new ApiScope(scope_2),
};

    public static IEnumerable<ApiResource> ApiResources => new ApiResource[]
{
        new ApiResource("myApi")
        {
            Scopes = new List<string>{ scope_1, scope_2 },
            ApiSecrets = new List<Secret>{ new Secret("supersecret".Sha256()) }
        },
         new ApiResource("ApiName")
        {
            Scopes = new List<string>{ scope_1, scope_2 },
            ApiSecrets = new List<Secret>{ new Secret("supersecret".Sha256()) }
        }
};

    public static IEnumerable<Client> Clients => new Client[]
{
        new Client
        {
            ClientId = "client",
            ClientName = "Client Credentials Client",
            AllowedGrantTypes = GrantTypes.ClientCredentials,
            ClientSecrets = { new Secret("secret".Sha256()) },
            AllowedScopes = { scope_1 }
        },
        new Client{
            ClientId="ro.client",
            AllowedGrantTypes= GrantTypes.ResourceOwnerPasswordAndClientCredentials,
             ClientSecrets = { new Secret("secret".Sha256()) },
            AllowedScopes={scope_1, scope_2}

        }
};
}