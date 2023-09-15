using BOD.IdentitySrv;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//builder.Services
//.AddIdentityServer()
//.AddDeveloperSigningCredential()
//.AddInMemoryApiResources(Config.GetAllAPIResources())
//.AddInMemoryClients(Config.GetClients())
//;

//1 hour expiry = 3600
builder.Services.AddIdentityServer()

     .AddInMemoryClients(Config2.GetClients())
    .AddInMemoryIdentityResources(Config2.GetIdentityResources())
    .AddInMemoryApiResources(Config2.GetApiResources())
    .AddInMemoryApiScopes(Config2.GetApiScopes())
    //.AddTestUsers(Config2.TestUsers)
    .AddDeveloperSigningCredential();

// // // builder.Services.AddIdentityServer()
// // //             .AddDeveloperSigningCredential()
// // //             //.AddOperationalStore(options =>
// // //             //{
// // //             //    options.EnableTokenCleanup = true;
// // //             //    options.TokenCleanupInterval = 30; // interval in seconds
// // //             //})
// // //             .AddInMemoryApiResources(Config2.GetApiResources())
// // //             .AddInMemoryClients(Config2.GetClients())
// // //             .AddInMemoryIdentityResources(Config2.GetIdentityResources())
// // //             //.AddTestUsers(IdentityConfiguration.TestUsers)
// // //             .AddInMemoryApiScopes(Config2.GetApiScopes())
// // //             ;



//https://www.red-gate.com/simple-talk/development/dotnet-development/working-with-identity-server-4/
//https://www.freecodespot.com/blog/secure-dot-net-core-using-identity-server

//https://github.com/damienbod/IdentityServer4AspNetCoreIdentityTemplate
//https://code-maze.com/identityserver4-integration-aspnetcore/

var app = builder.Build();

// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
app.UseSwagger();
app.UseSwaggerUI();
//}

//app.UseHttpsRedirection();

//app.UseAuthorization();

app.MapControllers();

app.UseIdentityServer();
app.Run();
