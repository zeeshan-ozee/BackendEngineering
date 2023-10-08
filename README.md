# BackendEngineering

 dotnet new search identity  
 
 dotnet new install identityserver4.templates

 https://github.com/dotnet/templating/wiki/Available-templates-for-dotnet-new
https://dotnetnew.azurewebsites.net/

 
//https://csharp.hotexamples.com/examples/System.Net.Http/HttpClient/RequestPasswordTokenAsync/php-httpclient-requestpasswordtokenasync-method-examples.html
//https://www.red-gate.com/simple-talk/development/dotnet-development/working-with-identity-server-4/
//https://www.freecodespot.com/blog/secure-dot-net-core-using-identity-server

//https://github.com/damienbod/IdentityServer4AspNetCoreIdentityTemplate
//https://code-maze.com/identityserver4-integration-aspnetcore/

--------------------------------------------------------------------------------------------------------------------------------------
https://gowthamcbe.com/2022/12/10/get-start-with-identity-server-4-with-asp-net-core-6/

Test User
Add the following code to the IdentityConfiguration class

Identity Resource
The data like UserId, phone number, email which has something unique to a particular identity/user are the Identity Resource. Add the following code to IdentityConfigration class

API Scopes
   Scopes defines the authorization level for the user. Let’s have two scopes for now name it as api.read and api.write. 

API Resources
  Let’s define the API Resource with Scopes and API Secrets. Ensure to hash this secret code

API Resources
  Let’s define the API Resource with Scopes and API Secrets. Ensure to hash this secret code
-------------------------------------------------------------------------------------------------------------------------------------------  
WebApi is a resource,

https://auth0.com/docs/get-started/authentication-and-authorization-flow

client gain access to resource using access token

Grant Types -> way to talk to auth server (OIDC and Oauth2)
  client credentails => user name and password based (server to server, with in company, highly trusted application)
        sending client and secret to call an api, no user involved
With machine-to-machine (M2M) applications, such as CLIs, daemons, or services running on your back-end, the system authenticates and authorizes the app rather than a user. For this scenario, typical authentication schemes like username + password or social logins don't make sense. Instead, M2M apps use the Client Credentials Flow 


-------------
  resource owner password -> resource owner is user
      user involved, trusted application,SPA , native 1st person apps

Though we do not recommend it, highly-trusted applications can use the Resource Owner Password Flow, which requests that users provide credentials (username and password), typically using an interactive form. The Resource Owner Password Flow should only be used when redirect-based flows (like the Authorization Code Flow) cannot be used.
--------------------
  Authorization code (google / facebook())
      authroization code send back, users are involved, web app, server side, 3rd party native app, 

-------------
Implicit
  user involved, redirect browser to indentity server 4, show login form, after login show concent page (approval before making call to resources),
    browser based or java script apps, server side web apps

--------
Hybrid
    combination of implicit and authroization code 
      get identity token, contain signature and artifacts, server side web app, naative mobile/desktop apps
-----------------
should write basic info related to all the termnology of the OAuth
Client
APi resournce
Api owner
Flows
grant type

Implicit flow => redirect to IDSERVER UI for login and redirect back to client