using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;
using PrimeiroProjeto.Auth.Config;
using System.Text;

namespace PrimeiroProjeto.Configurations
{
    public static class AuthConfig
    {
        public static IServiceCollection
            AddAuthConfiguration(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            var tokenConfiguration = new
                TokenConfiguration();
            configuration.GetSection
                ("TokenConfigurations")
                .Bind(tokenConfiguration);
            services.AddSingleton
                (tokenConfiguration);
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme =
                JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme =
                JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.TokenValidationParameters =
                new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = tokenConfiguration.Issuer,
                    ValidAudience = tokenConfiguration.Audience,
                    IssuerSigningKey = new
                    SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes
                        (tokenConfiguration.Secret))
                };
            });
            services.AddAuthorization(
            options =>
            {
                options.AddPolicy("Bearer",
                new AuthorizationPolicyBuilder()
                .AddAuthenticationSchemes(
                    JwtBearerDefaults.AuthenticationScheme)
                .RequireAuthenticatedUser().Build());
            }); 
        
            return services;
        
                

        }
    }
}
