using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using System;
using System.IO;
using System.Reflection;
using TheMonarchs.API.Common.Filters;
using TheMonarchs.API.Extensions;
using TheMonarchs.Infrastructure.DataProviders;

namespace TheMonarchs.API
{
    public class Startup
    {

        private readonly string APIName;

        public Startup(IConfiguration configuration, IWebHostEnvironment webHostEnvironment)
        {
            Configuration = configuration;
            APIName = $"The Monarchs API - {webHostEnvironment.EnvironmentName}";

        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            var dataFilePath = Path.Combine(System.IO.Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location),
                                            Configuration.GetConnectionString("JsonDataFile"));

            services.AddRepositoriesRegistrations(new MonarchJsonDataProvider(dataFilePath));
            services.AddAppServicesRegistration();



            services.AddControllers(cfg => cfg.Filters.Add<ApiExceptionFilterAttribute>());

            services.AddApiVersioning(o =>
            {
                o.ReportApiVersions = true;
                o.AssumeDefaultVersionWhenUnspecified = true;
                o.DefaultApiVersion = new ApiVersion(1, 0);
                o.ApiVersionReader = new HeaderApiVersionReader("api-version");
            }).AddSwaggerGen(setup =>
            {
                setup.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = APIName,
                    Contact = new OpenApiContact { Email = "mohsin85mehmood@gmail.com", Name = "Mohsin Mehmood" },
                    Description = "Monarch API for demo purpose"
                });

                var xmlCommentsFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlCommentsFullPath = Path.Combine(AppContext.BaseDirectory, xmlCommentsFile);

                setup.IncludeXmlComments(xmlCommentsFullPath);
            });

            services.AddHealthChecks();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger();

            app.UseSwaggerUI(s =>
            {
                s.SwaggerEndpoint("/swagger/v1/swagger.json", APIName);
                s.RoutePrefix = string.Empty;
            });

            app.UseHealthChecks("/health");
            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
