using System.Text;
using Asp.Versioning;
using AutoMapper;
using Contracts;
using Contracts.RepositoryContracts;
using Contracts.ServiceContracts;
using Entities.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Repository;
using Services;
using Shared.Dto.ResponseDto;

namespace NutriTrackerAPI.Extensions;

public static class ServiceExtensions
{
    public static void ConfigureCors(this IServiceCollection services) =>
        services.AddCors(options =>
        {
            options.AddPolicy("CorsPolicy", builder => builder.AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader());
        });
    public static void ConfigureSqlContext(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<RepositoryContext>(opt => opt.UseSqlServer(configuration.GetConnectionString("sqlConnection")));
    }

    public static void ConfigureMapper(this IServiceCollection services)
    {
        services.AddAutoMapper(typeof(Mapper.AutoMapper));
    }
    public static void ConfigureIdentity(this IServiceCollection services)
    {
        services.AddIdentity<User, IdentityRole>(
                o =>
                {
                    o.Password.RequireDigit = false;
                    o.Password.RequiredLength = 6;
                    o.Password.RequireLowercase = false;
                    o.Password.RequireNonAlphanumeric = false;
                    o.Password.RequireUppercase = false;
                    o.User.RequireUniqueEmail = true;
                })
            .AddEntityFrameworkStores<RepositoryContext>().AddDefaultTokenProviders();
    }
    public static void ConfigureRepositoryManager(this IServiceCollection services) =>
        services.AddScoped<IRepositoryManager,RepositoryManager >();
    
    public static void ConfigureServiceManager(this IServiceCollection services) =>
        services.AddScoped<IServiceManager, ServiceManager>();
    public static void ConfigureJwtAuthentication(this IServiceCollection services, IConfiguration configuration)
    {
        var jwtSettings = configuration.GetSection("JwtSettings");
        var key = Environment.GetEnvironmentVariable("SECRET");
        services.AddAuthentication(
            opt =>
            {
                opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                
            }
            ).AddJwtBearer(
            opt =>
                {
                    opt.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidAudience = jwtSettings["validAudience"],
                        ValidIssuer = jwtSettings["validIssuer"],
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key))
                    };
                }
            );
    }

    public static void ConfigureDataShaper(this IServiceCollection services)
    {
        services.AddScoped<IDataShaper<ProductDto>, DataShaper<ProductDto>>();
        services.AddScoped<IDataShaper<WaterConsumptionDto>, DataShaper<WaterConsumptionDto>>();
        services.AddScoped<IDataShaper<ProductConsumptionDto>, DataShaper<ProductConsumptionDto>>();
    }

    public static void ConfigureVersioning(this IServiceCollection services)
    {
        services.AddApiVersioning(opt =>
        {
            opt.ReportApiVersions = true;
            opt.AssumeDefaultVersionWhenUnspecified = true;
            opt.DefaultApiVersion = new ApiVersion(1, 0);
            opt.ApiVersionReader = new HeaderApiVersionReader("api-version");
        });
    }
    public static void ConfigureSwagger(this IServiceCollection services)
    {
        services.AddSwaggerGen(s =>
        {
            s.SwaggerDoc("v1",new OpenApiInfo{Title = "NutriTrackerAPI", Version = "v1"});
            s.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                In = ParameterLocation.Header,
                Description = "Place to add JWT with Bearer",
                Name = "Authorization",
                Type = SecuritySchemeType.ApiKey,
                Scheme = "Bearer"
            });
            s.AddSecurityRequirement(new OpenApiSecurityRequirement()
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer",
                        },
                        Name ="Bearer"
                    },
                    new List<string>()
                }
            });
        });
    }
    
}
