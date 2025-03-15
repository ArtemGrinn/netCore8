using System.Text;
using Asp.Versioning;
using Domain.DataAccess;
using Domain.Models;
using Domain.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

namespace Api;

public class Program
{
    public static async Task Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ??
                               throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
        builder.Services.AddDbContext<ApplicationDbContext>(o => o.UseSqlite(connectionString));

        // Add services to the container.
        builder.Services.AddControllers();
        builder.Services.AddSingleton<IToDoService, ToDoService>();
        builder.Services.AddScoped<ILoggerService, ConsoleLoggerService>();
        
        //аутентификация
        var tokenSettings = builder.Configuration.GetSection("Token");
        builder.Services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(options =>
        {
            options.TokenValidationParameters = new TokenValidationParameters()
            {
                ValidateAudience = false,
                ValidateIssuer = false,
                ValidateLifetime = false,
                ValidateIssuerSigningKey = true,
                ValidIssuer = tokenSettings.GetSection("Issuer").Value,
                ValidAudience = tokenSettings.GetSection("Audience").Value,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(tokenSettings.GetSection("Secret").Value))
            };
        });
        
        builder.Services.AddIdentity<User, IdentityRole>(opts => {
                opts.Password.RequiredLength = 6;   
                opts.Password.RequireNonAlphanumeric = false;   
                opts.Password.RequireLowercase = false; 
                opts.Password.RequireUppercase = false; 
                opts.Password.RequireDigit = false; 
            })
            .AddEntityFrameworkStores<ApplicationDbContext>();
        
        //версионирование
        builder.Services.AddApiVersioning(setup =>
        {
            setup.DefaultApiVersion = new ApiVersion(1, 0);
            setup.AssumeDefaultVersionWhenUnspecified = true;
            setup.ReportApiVersions = true; 
        })
            .AddApiExplorer(setup =>
        {
            setup.GroupNameFormat = "'v'VVV";
            setup.SubstituteApiVersionInUrl = true;
        });
        builder.Services.AddEndpointsApiExplorer();
        
        //swagger
        builder.Services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "1.0" }); 
            c.SwaggerDoc("v2", new OpenApiInfo { Title = "My API", Version = "2.0" }); 
            c.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, "Api.xml")); 
            c.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, "Domain.xml"));
            
            c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                In = ParameterLocation.Header,
                Description = "Введите авторизационный Bearer токен",
                Name = "Authorization",
                Type = SecuritySchemeType.Http,
                BearerFormat = "JWT",
                Scheme = "bearer"
            });
            c.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type=ReferenceType.SecurityScheme,
                            Id="Bearer"
                        }
                    },
                    []
                }
            });
        });

        var app = builder.Build();

        using (var serviceScope = app.Services.CreateScope())
        {
            var roleManager = serviceScope
                .ServiceProvider
                .GetRequiredService<RoleManager<IdentityRole>>();
            // Создание ролей "Admin", "User"
            await roleManager.CreateAsync(new IdentityRole("Admin"));
            await roleManager.CreateAsync(new IdentityRole("User"));
        }

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
                c.SwaggerEndpoint("/swagger/v2/swagger.json", "My API V2");
            });
        }

        app.UseHttpsRedirection();

        app.UseAuthentication();
        app.UseAuthorization();

        app.MapControllers();
        app.UseRequestLogger();
        await app.RunAsync();
    }
}