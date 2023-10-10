using IdentityServer4;
using IdentityServer4.Models;
using IdentityServer4.Test;
namespace BankOfDotNet.IdentitySvr;

public class MyConfig
{
    public static IEnumerable<ApiResource> GetAllApiResources()
    {
        return new List<ApiResource>
            {
                new ApiResource("bankOfDotNetApi", "Customer Api for BankOfDotNet")
            };
    }

    public static IEnumerable<Client> GetClients()
    {
        return new List<Client>
            {
                new Client
                {
                    ClientId = "client",
                    AllowedGrantTypes = GrantTypes.ClientCredentials,
                    ClientSecrets =
                    {
                        new Secret("secret".Sha256())
                    },
                    AllowedScopes = { "bankOfDotNetApi" }
                },
            //Implicit flow grant type
                new Client 
                {
                    ClientId="mvc",
                    ClientName = "MVC client",
                    AllowedGrantTypes= GrantTypes.Implicit,

                    RedirectUris ={ "http://localhost:5085/signin-oidc" },
                    PostLogoutRedirectUris ={"http://localhost:5085/singout-callback-oidc"},

                    AllowedScopes= new List<string>{
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile
                    }


                }
            };
    }

    public static List<TestUser> GetUsers()
    {
        return new List<TestUser>
            {
                new TestUser
                {
                    SubjectId = "1",
                    Username = "Manish",
                    Password = "password"
                },
                new TestUser
                {
                    SubjectId = "2",
                    Username = "Bob",
                    Password = "password"
                }
            };
    }

    public static IEnumerable<IdentityResource> GetIdentityResources()
    {
        return new List<IdentityResource>
        {
            new IdentityResources.OpenId(),
            new IdentityResources.Profile()
        };
    }
}
