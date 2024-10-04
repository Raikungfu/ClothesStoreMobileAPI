using ClothesStoreMobileApplication.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System;
using ClothesStoreMobileApplication.Service;
using ClothesStoreMobileApplication.Repository.IRepository;
using ClothesStoreMobileApplication.Repository;
using ClothesStoreMobileApplication.AutoMapper;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using ClothesStoreMobileApplication.Hubs;
using ClothesStoreMobileApplication.ViewModels.ChatMessage;
using ClothesStoreMobileApplication.Service.IService;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddDbContext<ClothesStoreContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddSingleton<IConfiguration>(builder.Configuration);
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();


var environment = builder.Environment.EnvironmentName;

var allowedOrigins = builder.Configuration.GetSection("AllowedOrigins").GetChildren().FirstOrDefault(x => x.Key == environment)?.Value?.Split(',');

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.PropertyNamingPolicy = null;
        options.JsonSerializerOptions.IgnoreNullValues = true;
    });

builder.Services.AddSignalR()
    .AddJsonProtocol(options =>
    {
        options.PayloadSerializerOptions.PropertyNamingPolicy = null;
        options.PayloadSerializerOptions.DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull;
    });

builder.Services.AddDistributedMemoryCache();

builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(500);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigin", policy =>
    {
        // policy.WithOrigins(allowedOrigins)
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader()
              // .AllowCredentials()
              .SetIsOriginAllowed(_ => true)
              .WithExposedHeaders("Authorization");
    });
});
builder.Services.AddSingleton<ConnectionMappingService>();
builder.Services.AddAutoMapper(typeof(ClothesStoreMapper));

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = "RaiYugi",
        ValidAudience = "Saint",
        IssuerSigningKey = new RsaSecurityKey(KeyHelper.GetPublicKey()),
        ClockSkew = TimeSpan.Zero
    };

    options.Events = new JwtBearerEvents
    {
        OnMessageReceived = context =>
         {
             var authorizationHeader = context.Request.Headers["Authorization"].ToString();

             if (!string.IsNullOrEmpty(authorizationHeader) &&
                 authorizationHeader.StartsWith("Bearer "))
             {
                 context.Token = authorizationHeader.Substring("Bearer ".Length).Trim();
             }

             return Task.CompletedTask;
         },

        OnTokenValidated = context =>
        {
            Console.WriteLine("Authorize token successfull: " + context.SecurityToken);
            var claimsIdentity = context.Principal.Identity as ClaimsIdentity;

            var userIdClaim = claimsIdentity?.FindFirst(ClaimTypes.NameIdentifier);

            if (userIdClaim == null)
            {
                context.Fail("Token không chứa thông tin người dùng hợp lệ");
                return Task.CompletedTask;
            }

            return Task.CompletedTask;
        },

        OnAuthenticationFailed = context =>
        {
            Console.WriteLine("Authorize token failed: " + context.Exception.Message);
            return Task.CompletedTask;
        },

        OnChallenge = context =>
        {
            Console.WriteLine("Authorize token failded! Not findout token...");
            return Task.CompletedTask;
        }
    };
});

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("AdminPolicy", policy =>
        policy.RequireClaim(ClaimTypes.Role, "Admin"));
    options.AddPolicy("SellerPolicy", policy =>
        policy.RequireClaim(ClaimTypes.Role, "Seller"));
    options.AddPolicy("CustomerPolicy", policy =>
            policy.RequireClaim(ClaimTypes.Role, "Customer"));
});

builder.Services.AddSignalR(hubOptions =>
{
    hubOptions.EnableDetailedErrors = true;
});

builder.Services.AddSingleton<IConnectionMappingService, ConnectionMappingService>();

var app = builder.Build();

app.UseCors("AllowSpecificOrigin");

app.UseSwagger();
app.UseSwaggerUI();

// app.UseHttpsRedirection();
app.UseRouting();

app.UseSession();

app.UseMiddleware<JwtMiddleware>();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.MapHub<HubClothesStore<ChatMessageViewModel>>("/hubClothesStore/ChatMessage");

app.Run();