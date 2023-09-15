using IdentityServer4.AccessTokenValidation;
using Microsoft.EntityFrameworkCore;
using WebAPI.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


// // builder.Services
// //     .AddAuthentication("Bearer")
// //     .AddIdentityServerAuthentication(
// //         options =>
// //         {
// //             options.Authority = "http://localhost:5147";
// //             options.RequireHttpsMetadata = false;
// //             options.ApiName = "ApiName"; //APIResource Name
// //         });

builder.Services.AddAuthentication("Bearer")
.AddIdentityServerAuthentication("Bearer", options =>
{
    options.Authority = "http://localhost:5147";
    options.RequireHttpsMetadata = false;
    options.ApiName = "ApiName"; //APIResource Name
});

//NOTE: for inmemory no need to add AddDbContext
builder.Services.AddScoped<BankContext>();
// builder.Services.AddDbContext<BankContext>(options =>
//        options.UseInMemoryDatabase("AuthorDb"));


// // // builder.Services
// // //     .AddDbContext<BankContext>((sp, options) =>
// // //     {
// // //         options
// // //         .UseInMemoryDatabase(Guid.NewGuid().ToString())
// // //         .UseInternalServiceProvider(sp);
// // //     });


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
