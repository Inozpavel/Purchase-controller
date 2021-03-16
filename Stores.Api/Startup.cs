using System;
using System.IO;
using System.Reflection;
using System.Text;
using MassTransit;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Stores.Api.Data;
using Stores.Api.Mapper;
using Stores.Api.Services;

namespace Stores.Api
{
    public class Startup
    {
        private readonly IConfiguration _configuration;

        public Startup(IConfiguration configuration) => _configuration = configuration;

        public void ConfigureServices(IServiceCollection services)
        {
            IdentityModelEventSource.ShowPII = true;
            services.AddControllers();

            services.AddAutoMapper(mapperConfiguration =>
            {
                mapperConfiguration.AddProfile<StoreProfile>();
                mapperConfiguration.AddProfile<ProductProfile>();
                mapperConfiguration.AddProfile<CategoryProfile>();
                mapperConfiguration.AddProfile<PurchaseProfile>();
            });

            services.AddSwaggerGen(options =>
            {
                options.EnableAnnotations();
                options.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "StoresService",
                    Description = "Service for managing stores and purchases",
                    Version = "v1",
                    Contact = new OpenApiContact
                    {
                        Email = "inozpavel@mail.ru",
                        Name = "Inozemtsev Pavel"
                    }
                });

                options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    BearerFormat = "JWT",
                    Description =
                        "JWT authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
                    Scheme = JwtBearerDefaults.AuthenticationScheme
                });

                options.AddSecurityRequirement(new OpenApiSecurityRequirement
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
                        Array.Empty<string>()
                    }
                });

                string xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                string filePath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                options.IncludeXmlComments(filePath);
            });

            services.AddDbContext<ApplicationContext>(options =>
            {
                options.UseNpgsql(_configuration["DB_CONNECTION_STRING"]);
            });

            services.AddScoped<IStoreRepository, StoreRepository>();
            services.AddScoped<IStoreService, StoreService>();

            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IProductService, ProductService>();

            services.AddScoped<IStoreCategoryRepository, PostgreStoreCategoryRepository>();
            services.AddScoped<IStoreCategoryService, StoreCategoryService>();

            services.AddScoped<IPurchaseRepository, PurchaseRepository>();
            services.AddScoped<IPurchaseService, PurchaseService>();

            services.AddTransient<DatabaseInitializer>();

            services.AddAuthentication(x =>
            {
                x.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = false,
                    ValidateAudience = false,

                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.Zero,

                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT_KEY"]))
                };
            });

            services.AddMassTransit(x =>
            {
                x.SetKebabCaseEndpointNameFormatter();

                x.UsingRabbitMq((context, configurator) =>
                {
                    configurator.Host(_configuration["RABBITMQ_HOST"], options =>
                    {
                        options.Username(_configuration["RABBITMQ_USER"]);
                        options.Password(_configuration["RABBITMQ_PASSWORD"]);
                    });

                    configurator.ConfigureEndpoints(context);
                });
            });
            services.AddMassTransitHostedService();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, DatabaseInitializer initializer)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            initializer.Initialize();

            app.UseSwagger();
            app.UseSwaggerUI(options =>
            {
                options.RoutePrefix = "";
                options.DocumentTitle = "Documentation for stores service";
                options.SwaggerEndpoint("/swagger/v1/swagger.json", "StoresService");
            });


            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}