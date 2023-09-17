using System.Security.Claims;
using IdentityServer4;
using IdentityServer4.Models;
using IdentityServer4.Test;
using IdentityModel;

namespace BOD.IdentitySrv;

public static class Config2
{
    static string scope_1 = "read";
    static string scope_2 = "ApiName";

    static string scope_3 = "write";
    static string scope_4 = "delete";
    static string scope_5 = "identityserverapi";

    public static List<TestUser> TestUsers =>
  new List<TestUser>
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
        }
    };

    public static List<IdentityResource> GetIdentityResources()
    {
        return new List<IdentityResource>
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile() // <-- usefull
            };
    }


    public static IEnumerable<ApiScope> GetApiScopes()
    {
        return new List<ApiScope>
             {
                 new ApiScope(name: scope_1,   displayName: "Read your data."),
                 new ApiScope(name: scope_3,  displayName: "Write your data."),
                 new ApiScope(name: scope_4, displayName: "Delete your data."),
                 new ApiScope(name: scope_5, displayName: "manage identityserver api endpoints."),
                   new ApiScope(name: scope_2, displayName: "ApiName.")
             };
    }


    public static IEnumerable<ApiResource> GetApiResources()
    {
        return new List<ApiResource>
            {
               // new ApiResource(scope_2, "ApiDisplayName"),
                 new ApiResource("myApi")
                {
                    Scopes = new List<string>{ scope_1, scope_2,scope_3, scope_4, scope_5 },
                    ApiSecrets = new List<Secret>{ new Secret("supersecret".Sha256()) }
                }
            };
    }

    public static IEnumerable<Client> GetClients()
    {
        return new[]
        {
            new Client
            {
            ClientId = "client",
            ClientName = "Client Credentials Client",
            AllowedGrantTypes = GrantTypes.ClientCredentials,
            ClientSecrets = {
                new Secret("secret".Sha256())
                },
            AllowedScopes = {
                  IdentityServerConstants.StandardScopes.OpenId,
                    IdentityServerConstants.StandardScopes.Profile,
                scope_1, scope_2,scope_3, scope_4, scope_5
                }
            },
                // for public api
            new Client
            {
                ClientId = "secret_client_id",
                AllowedGrantTypes = GrantTypes.ClientCredentials,
                ClientSecrets =
                {
                    new Secret("secret".Sha256())
                },

                AllowedScopes =
                {
                    IdentityServerConstants.StandardScopes.OpenId,
                    IdentityServerConstants.StandardScopes.Profile,
                    scope_1, scope_2,scope_3, scope_4, scope_5
                }
            }
        };
    }

}
