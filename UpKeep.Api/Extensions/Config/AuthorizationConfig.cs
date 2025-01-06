using System.Reflection;
using Microsoft.OpenApi.Models;
using UpKeep.Data.Configuration;

namespace UpKeepApi.Extensions.Config;

public static class AuthorizationConfig
{
    public static void ConfigurarAuthorization(this IServiceCollection services)
    {
        services.AddAuthorization(option =>
        {
            //Utilizar Roles de Upkeep
            option.AddPolicy(IdentityData.AdminUserPolicyName,
                policy => policy.RequireClaim(IdentityData.AdminClaimName, IdentityData.AdminClaimValue)
            );
            option.AddPolicy(IdentityData.SuperAdminPolicyName,
                policy => policy.RequireClaim(IdentityData.SuperAdminClaimName, IdentityData.SuperAdminClaimValue));
        });

        var info = new OpenApiInfo()
        {
            Title = "UpKeep API",
            Version = "v1",
            Description = "Integration layer",
            Contact = new OpenApiContact()
            {
                Name = "UpKeep S.R.L.",
                Email = "intec@est.intec.edu.do",
            }
        };
        services.AddSwaggerGen(option =>
        {
            option.SwaggerDoc("v1", info);


            // Set the comments path for the Swagger JSON and UI.
            var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
            var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
            option.IncludeXmlComments(xmlPath);
            option.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                In = ParameterLocation.Header,
                Description = "Please enter a valid token",
                Name = "Authorization",
                Type = SecuritySchemeType.Http,
                BearerFormat = "JWT",
                Scheme = "Bearer"
            });
            option.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        }
                    },
                    new string[] { }
                }
            });
        });
    }
}