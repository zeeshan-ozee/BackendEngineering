using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using BankOfDotNet.IdentitySvr;

var builder = WebApplication.CreateBuilder(args);
//builder.Configuration.get;

builder.Services.AddMvc(options => {

    options.EnableEndpointRouting = false;
});

builder.Services.AddIdentityServer() 
     .AddInMemoryClients(MyConfig.GetClients())
    .AddInMemoryIdentityResources(MyConfig.GetIdentityResources())
    .AddInMemoryApiResources(MyConfig.GetAllApiResources())
    //.AddInMemoryApiScopes(MyConfig.get
    .AddTestUsers(MyConfig.GetUsers())
    .AddDeveloperSigningCredential()
    ;

var app = builder.Build();

app.MapGet("/", () => "Bank of dotnet identity server is running successfully!");



app.UseRouting();
//app.MapControllers();
app.UseIdentityServer();

app.UseStaticFiles();
app.UseMvcWithDefaultRoute();
//app.UseEndpoints(endpoints =>
//{
//    endpoints.MapGet("/", async context =>
//    {
//        await context.Response.WriteAsync("Hello World!");
//    });
//});

app.Run();

/*
 
Endpoint Routing does not support 'IApplicationBuilder.UseMvc(...)'. 
To use 'IApplicationBuilder.UseMvc' set 'MvcOptions.EnableEndpointRouting = false'
inside 'ConfigureServices(...).
 
 */
