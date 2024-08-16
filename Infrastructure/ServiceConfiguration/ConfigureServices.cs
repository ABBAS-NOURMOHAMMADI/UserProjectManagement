using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Primitives;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using Microsoft.IdentityModel.Tokens;
using System.Net;

namespace Infrastructure.ServiceConfiguration
{
    public static class ConfigureServices
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddJWTAuthentication(configuration);

            services.AddAuthorization(options =>
            {
                options.AddPolicy("BearerScheme", policy =>
                {
                    policy.AddAuthenticationSchemes("Bearer")
                        .RequireAuthenticatedUser();
                });
                options.DefaultPolicy = options.GetPolicy("BearerScheme");
            });

            return services;
        }

        public static void AddJWTAuthentication(this IServiceCollection services, IConfiguration configuration)
        {
            var oidcConfig = new OidcConfig();
            configuration.GetSection("OidcConfig").Bind(oidcConfig);

            services.AddOptions<JwtBearerOptions>(JwtBearerDefaults.AuthenticationScheme)
               .Configure<IHttpContextAccessor>((options, contextAccessor) =>
               {
                   options.Authority = oidcConfig.Authority;
                   options.RequireHttpsMetadata = false;
                   options.SaveToken = true;
                   string jsonWebKey = null;
                   do
                   {
                       try
                       {
                           using (var wc = new HttpClient())
                               jsonWebKey = wc.GetStringAsync($"{oidcConfig.Authority}/.well-known/openid-configuration/jwks").Result;
                       }
                       catch (Exception ex)
                       {
                           Thread.Sleep(3000);
                       }
                   } while (string.IsNullOrEmpty(jsonWebKey));

                   options.Configuration = new OpenIdConnectConfiguration()
                   {
                       TokenEndpoint = $"{oidcConfig.Authority}/connect/token",
                       Issuer = oidcConfig.Issuer,
                       UserInfoEndpoint = $"{oidcConfig.Authority}/connect/userinfo",
                       RequestParameterSupported = true,
                       JsonWebKeySet = new JsonWebKeySet(jsonWebKey)
                   };
                   foreach (var sk in options.Configuration.JsonWebKeySet.GetSigningKeys())
                       options.Configuration.SigningKeys.Add(sk);
                   options.TokenValidationParameters = new TokenValidationParameters
                   {
                       ValidateLifetime = true,
                       ValidateAudience = false,
                   };
                   options.MapInboundClaims = false;
                   options.Events = new JwtBearerEvents
                   {
                       OnTokenValidated = (ctx) =>
                       {
                           return Task.CompletedTask;
                       },
                       OnAuthenticationFailed = (e) =>
                       {
                           return Task.CompletedTask;
                       },
                       OnMessageReceived = (context) =>
                       {
                           StringValues values;
                           if (!context.Request.Query.TryGetValue("access_token", out values))
                           {
                               return Task.CompletedTask;
                           }
                           if (values.Count > 1)
                           {
                               context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                               context.Fail(
                                   "Only one 'access_token' query string parameter can be defined. " +
                                   $"However, {values.Count:N0} were included in the request."
                               );
                               return Task.CompletedTask;
                           }

                           var token = values.Single();
                           if (String.IsNullOrWhiteSpace(token))
                           {
                               context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                               context.Fail(
                                   "The 'access_token' query string parameter was defined, " +
                                   "but a value to represent the token was not included."
                               );
                               return Task.CompletedTask;
                           }
                           context.Token = token;
                           return Task.CompletedTask;
                       }
                   };
               });

            services.AddAuthentication()
               .AddJwtBearer();
        }
    }
}
