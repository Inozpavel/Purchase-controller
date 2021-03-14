using System;
using System.IO;
using System.Reflection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
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
            services.AddControllers();

            services.AddAutoMapper(mapperConfiguration =>
            {
                mapperConfiguration.AddProfile<StoreProfile>();
                mapperConfiguration.AddProfile<ProductProfile>();
                mapperConfiguration.AddProfile<CategoryProfile>();
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

            services.AddTransient<DatabaseInitializer>();
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

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}