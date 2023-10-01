

using System.Net.Http.Headers;
using System.Text;
using System.Text.Json.Nodes;
using IdentityModel.Client;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;


ResourceOwnerFlow_Main().GetAwaiter().GetResult();

//MainAsync().GetAwaiter().GetResult();
Console.ReadLine();
static async Task ImplicitFlow_Main(){

    
}

static async Task ResourceOwnerFlow_Main(){


using (HttpClient _httpClient = new())
    {
        _httpClient.DefaultRequestHeaders.Accept.Clear();
   
        _httpClient.DefaultRequestHeaders.Add("User-Agent", ".NET Foundation Repository Reporter");

        DiscoveryDocumentResponse? _discoRO = await _httpClient.GetDiscoveryDocumentAsync("http://localhost:5147");

         if (_discoRO.IsError){
            throw new Exception(_discoRO.Error);
            return;
         }
         else
         {
             var _tokenClientOptions = new TokenClientOptions
            {
                Address = "http://localhost:5147/connect/token",
                AuthorizationHeaderStyle = BasicAuthenticationHeaderStyle.Rfc6749,
                ClientId = "ro.client",
                ClientSecret = "secret"
            };

            TokenClient? _tokenClient = new TokenClient(_httpClient, _tokenClientOptions); 
            TokenResponse? _tokenResponse = await _tokenClient.RequestPasswordTokenAsync("Zeeshan","zeeshan@123", "api.read" );

                // var request = new PasswordTokenRequest();
                // request.Address = "";
                // request.ClientCredentialStyle = ClientCredentialStyle.PostBody;
                // request.Scope        = "data";
                // request.ClientId     = "ro.client";
                // request.ClientSecret = "secret";
                // request.UserName     ="Zeeshan";
                // request.Password     = "zeeshan@123";

                // TokenResponse? _tokenResponse = await _tokenClient.RequestPasswordTokenAsync(request);

            System.Console.WriteLine("XXXX=================================");
            if (_tokenResponse.IsError)
            {
                 System.Console.WriteLine(_tokenResponse.ToString());
                System.Console.WriteLine(_tokenResponse.Error);
                return;
            }
            System.Console.WriteLine(_tokenResponse.Json);
            System.Console.WriteLine("XXXXXX=================================");
         }

    }

}
static async Task MainAsync()
{
    


    // var disco = await DiscoveryClient.GetAsync("http://localhost:5463");
    // if (disco.IsError)
    // {
    //     System.Console.WriteLine(disco.Error);
    //     return;
    // }


    using HttpClient httpClient = new();
    httpClient.DefaultRequestHeaders.Accept.Clear();
    // client.DefaultRequestHeaders.Accept.Add(
    //     new MediaTypeWithQualityHeaderValue("application/vnd.github.v3+json"));
    httpClient.DefaultRequestHeaders.Add("User-Agent", ".NET Foundation Repository Reporter");

    DiscoveryDocumentResponse? disco = await httpClient.GetDiscoveryDocumentAsync("http://localhost:5147");
    if (disco.IsError)
        throw new Exception(disco.Error);
    else
    {
        var tokenClientOptions = new TokenClientOptions
        {
            Address = "http://localhost:5147/connect/token",
            AuthorizationHeaderStyle = BasicAuthenticationHeaderStyle.Rfc6749,
            ClientId = "client",
            ClientSecret = "secret"
        };

        TokenClient? tokenClient = new TokenClient(httpClient, tokenClientOptions);

        TokenResponse? tokenResponse = await tokenClient.RequestClientCredentialsTokenAsync("api.read");
        System.Console.WriteLine("1=================================");
        if (tokenResponse.IsError)
        {
            System.Console.WriteLine(tokenResponse.Error);
            return;
        }
        System.Console.WriteLine(tokenResponse.Json);
        System.Console.WriteLine("1=================================");

        HttpClient? _httpClient = new HttpClient();
        _httpClient.SetBearerToken(tokenResponse.AccessToken);

        var customerInfo = new StringContent(JsonConvert.SerializeObject(
            new { Id = 3, FirstName = "test", LastName = "test222" }
        ), Encoding.UTF8, "application/json");

        HttpResponseMessage? createCustomerResponse = await _httpClient.PostAsync("http://localhost:5128/api/Customers", customerInfo);
        System.Console.WriteLine("2=================================");
        if (!createCustomerResponse.IsSuccessStatusCode)
            System.Console.WriteLine(createCustomerResponse.StatusCode);
        else
            System.Console.WriteLine(createCustomerResponse.Content.ToString());
        System.Console.WriteLine("2=================================");

        HttpResponseMessage? getCustomerResponse = await _httpClient.GetAsync("http://localhost:5128/api/Customers");
        System.Console.WriteLine("3=================================");
        if (!getCustomerResponse.IsSuccessStatusCode)
        {
            System.Console.WriteLine(getCustomerResponse.StatusCode);
        }
        else
        {
            string? getCustomerResponseContent = await getCustomerResponse.Content.ReadAsStringAsync();
            System.Console.WriteLine(JArray.Parse(getCustomerResponseContent));
        }
        System.Console.WriteLine("3=================================");
        System.Console.WriteLine("i am done");


    }

}


public static class HttpExtensions
{
    public static async Task HandleToken(this HttpClient client, string authority, string clientId, string secret, string apiName)
    {
        var accessToken = await client.GetRefreshTokenAsync(authority, clientId, secret, apiName);
        client.SetBearerToken(accessToken);
    }

    private static async Task<string> GetRefreshTokenAsync(this HttpClient client, string authority, string clientId, string secret, string apiName)
    {
        DiscoveryDocumentResponse? disco = await client.GetDiscoveryDocumentAsync(authority);
        if (disco.IsError) throw new Exception(disco.Error);

        TokenResponse? tokenResponse = await client.RequestClientCredentialsTokenAsync(new ClientCredentialsTokenRequest
        {
            Address = disco.TokenEndpoint,
            ClientId = clientId,
            ClientSecret = secret,
            Scope = apiName
        });

        if (!tokenResponse.IsError) return tokenResponse.AccessToken;
        return null;
    }
}