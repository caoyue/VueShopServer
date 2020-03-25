using System;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using VueShopServer.Api.Utils;

namespace VueShopServer.Api
{
    public static class JwtAuthenticate
    {
        public static void Configure(IConfiguration configuration, IServiceCollection services)
        {
            var appSettingsSection = configuration.GetSection("AppSettings");

            services.Configure<AppSetting>(appSettingsSection);
            var appSettings = appSettingsSection.Get<AppSetting>();

            if (string.IsNullOrEmpty(appSettings.Secret)
                || Encoding.ASCII.GetByteCount(appSettings.Secret) < 16)
            {
                throw new ArgumentException("Secret key requires to be greater than 128 bits.");
            }
            var key = Encoding.ASCII.GetBytes(appSettings.Secret);
            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(x =>
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
#if debug
            IdentityModelEventSource.ShowPII = true;
#endif
        }
    }
}
