using Asp.Versioning;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;
using BloGoes.Data.AutoFac;
using BloGoes.Domain.Admin;
using BloGoes.IOC;
using BloGoes.Services.Services.Interfaces;
using BloGoes.Services;
using Autofac.Core;
using Microsoft.Extensions.DependencyInjection;
using System.Net.WebSockets;
using System.Net;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;

//Factory Context 
builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());
builder.Host.ConfigureContainer<ContainerBuilder>(b => b.RegisterModule(new AutoFacModule()));

// Add services to the container.

//Add IOpption Pattern
builder.Services.Configure<AppSettings>(builder.Configuration.GetSection("BloGoes"));



// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddApiVersioning(options =>
{
    options.DefaultApiVersion = new ApiVersion(1, 0);
    options.AssumeDefaultVersionWhenUnspecified = true;
    options.ReportApiVersions = true;
}).AddApiExplorer(options =>
  {
        options.GroupNameFormat = "'v'VVV";
        options.SubstituteApiVersionInUrl = true;
 });

builder.Services.AddSwaggerGen(options => {
    options.AddSecurityDefinition("Key", new OpenApiSecurityScheme
    {
        Description = "ApiKey must appear in header",
        Type = SecuritySchemeType.ApiKey,
        Name = "ApiKey",
        In = ParameterLocation.Header,
        Scheme = "ApiKeyScheme"
    });

    // Include 'SecurityScheme' to use JWT Authentication
    var jwtSecurityScheme = new OpenApiSecurityScheme
    {
        BearerFormat = "JWT",
        Name = "JWT Authentication",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.Http,
        Scheme = JwtBearerDefaults.AuthenticationScheme,
        Description = "Put **_ONLY_** your JWT Bearer token on textbox below!",

        Reference = new OpenApiReference
        {
            Id = JwtBearerDefaults.AuthenticationScheme,
            Type = ReferenceType.SecurityScheme
        }
    };

    options.AddSecurityDefinition(jwtSecurityScheme.Reference.Id, jwtSecurityScheme);

    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        { jwtSecurityScheme, Array.Empty<string>() }
    });
});



var key = Encoding.UTF8.GetBytes(builder.Configuration.GetSection("BloGoes:Secret").Value!);
builder.Services.AddAuthentication(x =>
{
    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(x =>
{
    x.RequireHttpsMetadata = false;
    x.SaveToken = true;
    x.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(key),
        ValidateIssuer = false,
        ValidateAudience = false
    };
});

//AddSwaggerExtension(services);
builder.Services.RegisterDependencyInjection(configuration);




var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.UseWebSockets();
app.Map("/ws", async httpContext =>
{
    if (httpContext.WebSockets.IsWebSocketRequest is false)
    {
        httpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
    }
    else
    {
        using var webSocket = await httpContext.WebSockets.AcceptWebSocketAsync();

        while (true)
        {
            var data = Encoding.ASCII.GetBytes($"Data {DateTime.Now}");
            await webSocket.SendAsync(data, WebSocketMessageType.Text, false, CancellationToken.None);
            await Task.Delay(5000);
        }
    }
});


await app.RunAsync();


//Configure WebSocket 
var socktServer = app.Services.GetService<IWebSocketServer>();
await socktServer.StartAsync("http://localhost:7284/ws");


