using System;
using System.IO;
using System.Reflection;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Purchases.Data;
using Purchases.Services;

namespace Purchases
{
    public class Startup
    {
        private readonly IConfiguration _configuration;

        public Startup(IConfiguration configuration) => _configuration = configuration;

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            services.AddDbContext<ApplicationContext>(options =>
            {
                options.UseNpgsql(_configuration["DB_CONNECTION_STRING"]);
            });

            services.AddSwaggerGen(options =>
            {
                options.EnableAnnotations();

                options.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "PurchasesService",
                    Description = "Service for managing users and purchases",
                    Version = "v1"
                });

                string xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                string filePath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                options.IncludeXmlComments(filePath);
            });

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
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

            services.AddAutoMapper(typeof(UserProfile));

            services.AddScoped<IUserRepository, PostgreUsersRepository>();
            services.AddScoped<IUserService, UserService>();

            services.AddScoped<IPurchasesRepository, PostgrePurchasesRepository>();

            services.AddTransient<DataSeeder>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, DataSeeder seeder)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            seeder.EnsureUsersAdded();

            app.UseSwagger();
            app.UseSwaggerUI(options =>
            {
                options.RoutePrefix = "";
                options.SwaggerEndpoint("/swagger/v1/swagger.json", "PurchasesService");
            });

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}