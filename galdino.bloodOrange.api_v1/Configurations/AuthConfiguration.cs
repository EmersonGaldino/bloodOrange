using galdino.bloodOrange.api_v1.Security;
using galdino.bloodOrange.application.shared.Confiugrations.Token;
using galdino.bloodOrange.application.shared.Constants;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Text;

namespace galdino.bloodOrange.api_v1.Configurations
{
    public class AuthConfiguration
    {
        public static void Register(IServiceCollection services, IConfiguration configuration)
        {
            var signConfiguration = new SignConfigurationToken();
            services.AddSingleton(signConfiguration);

            var tokenConfigure = new TokenConfigurations();

            new ConfigureFromConfigurationOptions<TokenConfigurations>(
                    configuration.GetSection(nameof(TokenConfigurations)))
                .Configure(tokenConfigure);

            services.AddSingleton(tokenConfigure);

            services
                .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(bearrerOptions =>
                {
                    var paramsValidation = bearrerOptions.TokenValidationParameters;
                    paramsValidation.ValidateIssuerSigningKey = true;
                    paramsValidation.ValidateLifetime = true;
                    paramsValidation.ValidateActor = true;
                    paramsValidation.ValidateAudience = true;
                    paramsValidation.ValidAudience = tokenConfigure.Audience;
                    paramsValidation.ValidIssuer = tokenConfigure.Issuer;
                    paramsValidation.ClockSkew = TimeSpan.Zero;

                    paramsValidation.IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(tokenConfigure.SigningKey));
                });

            services.AddAuthorization(auth =>
            {
                auth.AddPolicy("Bearer", new AuthorizationPolicyBuilder()
                    .AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme)
                    .RequireAuthenticatedUser().Build());

                auth.AddPolicy("Portal", policy =>
                {
                    policy.RequireAssertion(context =>
                        context.User.HasClaim(c => c.Type == Constant.EMPRESAID_CLAIM));
                });
            });
            services.AddMemoryCache();

        }
    }
}
