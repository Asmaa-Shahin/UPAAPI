using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Collections.Generic;
using System.Text;
using UPA.DAl;
using UPA.DAL.Models;

namespace UPA.Extensions
{
    public static class IdentityServiceExtension
    {
        public static IServiceCollection AddIdentityServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddIdentity<AppUser, IdentityRole>(option =>
            {

                option.Password.RequireDigit = true;
                option.Password.RequireLowercase = true;
                option.Password.RequireNonAlphanumeric = true;
                option.Password.RequireUppercase = true;
                option.Password.RequiredLength = 5;
                option.SignIn.RequireConfirmedAccount = false;
            })
     .AddEntityFrameworkStores<UPAModel>();
            //.AddDefaultTokenProviders()
            //services.AddIdentity<AppUser, IdentityRole>(options =>
            //{
            //    //options.Password.RequireDigit = true;
            //    //options.Password.RequireLowercase = true;
            //    //options.Password.RequireNonAlphanumeric = true;
            //    //options.Password.RequireUppercase = true;
            //    //options.Password.RequiredLength = 5;
            //    //options.SignIn.RequireConfirmedAccount = false;
            //})
            //.AddEntityFrameworkStores<>(UPAModel);
            //.AddDefaultTokenProviders();
           
            services.AddAuthentication(/*JwtBearerDefaults.AuthenticationScheme)*/
               options =>
               {
                   options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                   options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
               })
                .AddJwtBearer(options =>
                {
                    options.SaveToken = true;
                    //options.RequireHttpsMetadata = true;

                    options.TokenValidationParameters = new TokenValidationParameters()
                    {
                        ValidateAudience = true,
                        ValidAudience = configuration["JWT:ValidAudience"],
                        ValidateIssuer = true,
                        ValidIssuer = configuration["JWT:ValidIssuer"],
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:Key"])),
                        ValidateLifetime = true,

                    };
                });
            return services;
        }
    }
}
