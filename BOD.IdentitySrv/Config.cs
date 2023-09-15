using System.Security.Claims;
using IdentityServer4;
using IdentityServer4.Models;
using IdentityServer4.Test;
using IdentityModel;

namespace BOD.IdentitySrv;

public class Config
{

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
    public static IEnumerable<ApiResource> GetAllAPIResources()
    {

        return new List<ApiResource>
        {
            new ApiResource("WebApi","Customer APi"),
            new ApiResource("myApi")
                {
                    Scopes = new List<string>{ "api.read", "api.write" },
                    ApiSecrets = new List<Secret>{ new Secret("supersecret".Sha256()) }
                }
        };
    }
    public static IEnumerable<Client> GetClients()
    {
        //return new List<Client> { 
        // new Client{ 
        //    ClientId ="client"
        //    ,AllowedGrantTypes= GrantTypes.ClientCredentials
        //    ,ClientSecrets= {new Secret("secret".Sha256()) }
        //     ,AllowedScopes= { "WebApi" } //same scope as of API resource
        // },

        return new List<Client>
        {
            new Client
            {
                ClientId = "client",

                // no interactive user, use the clientid/secret for authentication
                AllowedGrantTypes = GrantTypes.ClientCredentials,

                // secret for authentication
                ClientSecrets =
                {
                    new Secret("secret".Sha256())
                },

                // scopes that client has access to
                AllowedScopes = { "WebApi" }
            },
            //new Client
            //{
            //    ClientId = "client2",

            //    // no interactive user, use the clientid/secret for authentication
            //    AllowedGrantTypes = GrantTypes.HybridAndClientCredentials,

            //    // secret for authentication
            //    ClientSecrets =
            //    {
            //        new Secret("secret".Sha256())
            //    },

            //    // scopes that client has access to
            //    AllowedScopes = { "sup"}
            //}
        };



    }

}

public class Config2
{
    public static IEnumerable<ApiResource> GetApiResources()
    {
        return new List<ApiResource>
            {
                new ApiResource("ApiName", "ApiDisplayName")
            };
    }

    public static List<IdentityResource> GetIdentityResources()
    {
        return new List<IdentityResource>
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile() // <-- usefull
            };
    }

    public static IEnumerable<Client> GetClients()
    {
        return new[]
        {
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
                    "ApiName"
                }
            }
        };
    }


    public static IEnumerable<ApiScope> GetApiScopes()
    {
        return new List<ApiScope>
             {
                 new ApiScope(name: "read",   displayName: "Read your data."),
                 new ApiScope(name: "write",  displayName: "Write your data."),
                 new ApiScope(name: "delete", displayName: "Delete your data."),
                 new ApiScope(name: "identityserverapi", displayName: "manage identityserver api endpoints."),
                   new ApiScope(name: "ApiName", displayName: "ApiName.")
             };
    }
}
