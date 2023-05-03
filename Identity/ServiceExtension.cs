using Application.Interfaces;
using Application.Wrappres;
using Domain.Entities;
using Domain.Settings;
using Identity.Context;
using Identity.Model;
using Identity.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System.Text;

namespace Identity
{
    /// <ServiceExtensions>
    /// In this class we will create the connection with the database, which will be our data persistence
    /// for any call or flow that we want to make in the database.
    /// </ServiceExtensions>
    public static class ServiceExtension
    {
        /// <AddIdentityInfraestructure>
        /// Create the database connection and configure the JWT key.
        /// </AddIdentityInfraestructure>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        public static void AddIdentityInfraestructure(this IServiceCollection services, IConfiguration configuration)
        {
            //Connection DataBase
            services.AddDbContext<IdentityContext>(options => options.UseNpgsql(
                configuration.GetConnectionString("IdentityConnection"),
                b => b.MigrationsAssembly(typeof(IdentityContext).Assembly.FullName)));
            
            services.AddIdentity<ApplicationUser,IdentityRole>().AddEntityFrameworkStores<IdentityContext>().AddDefaultTokenProviders();
            //
            services.AddTransient<IAccountService, AccountService>();
            //we bring the JWT configuration of the appsettings
            services.Configure<JWTSettings>(configuration.GetSection("JWTSettings"));
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

            }).AddJwtBearer(o =>
            {
                //Authenticator configurations and how we will handle
                o.RequireHttpsMetadata = false;
                o.SaveToken = false;
                o.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.Zero,
                    ValidIssuer = configuration["JWTSettings:Issuer"],
                    ValidAudience = configuration["JWTSettings:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWTSettings:Key"])),
                };
                //Responses to authenticator failures
                o.Events = new JwtBearerEvents()
                {
                    OnAuthenticationFailed = c =>
                    {
                        c.NoResult();
                        c.Response.StatusCode= 500;
                        c.Response.ContentType = "text/plain";
                        return c.Response.WriteAsync(c.Exception.ToString());
                    },
                    OnChallenge = context =>
                    {
                        context.HandleResponse();
                        context.Response.StatusCode = 401;
                        context.Response.ContentType= "application/json";
                        var result = JsonConvert.SerializeObject(new Response<string>("Unauthorized user"));
                        return context.Response.WriteAsync(result);
                    },
                    OnForbidden = context =>
                    {
                        context.Response.StatusCode = 400;
                        context.Response.ContentType = "application/json";
                        var result = JsonConvert.SerializeObject(new Response<string>("You do not have permissions on this resource"));
                        return context.Response.WriteAsync(result);
                    }
                };
            });
        }
    }
}
