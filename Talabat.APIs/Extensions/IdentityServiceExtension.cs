﻿using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Talabat.Core.Identity;
using Talabat.Core.Services;
using Talabat.Repository.Identity;
using Talabat.Services;

namespace Talabat.APIs.Extensions
{
    public static class IdentityServiceExtension
    {
        public static IServiceCollection AddIdentityServices(this IServiceCollection services,IConfiguration configuration)
        {
            services.AddIdentity<AppUser, IdentityRole>()
                         .AddEntityFrameworkStores<AppIdentityDbContext>();
            //Security Schema
            services.AddAuthentication(Options =>
            {
                Options.DefaultAuthenticateScheme=JwtBearerDefaults.AuthenticationScheme;
                Options.DefaultChallengeScheme=JwtBearerDefaults.AuthenticationScheme;
            })
                    .AddJwtBearer(Options =>
                    {
                        Options.TokenValidationParameters = new TokenValidationParameters()
                        {
                            ValidateIssuer= true,
                            ValidIssuer = configuration["JWT:ValidIssuer"],
                            ValidateAudience= true,
                            ValidAudience = configuration["JWT:ValidAudience"],
                            ValidateLifetime=true,
                            ValidateIssuerSigningKey=true,
                            IssuerSigningKey=new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:Key"]))
                        };
                    });

            services.AddScoped<ITokenService, TokenService>();
            return services;
        }
    }
}
