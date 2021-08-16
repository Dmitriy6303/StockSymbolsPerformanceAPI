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
using StockSymbolsPerformanceAPI.ApplicationServices;
using StockSymbolsPerformanceAPI.AutoMapper;
using StockSymbolsPerformanceAPI.DAL;
using StockSymbolsPerformanceAPI.Helpers;

namespace StockSymbolsPerformanceAPI
{
    /// <summary>
    /// Start
    /// </summary>
    public class Startup
    {
        private static readonly string Version = typeof(Startup).Assembly.GetCustomAttribute<AssemblyFileVersionAttribute>()?.Version;
        private const string ServiceName = "StockSymbolsPerformance";

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="configuration"></param>
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        /// <summary>
        /// Configuration
        /// </summary>
        public IConfiguration Configuration { get; }

        /// <summary>
        /// This method gets called by the runtime. Use this method to add services to the container.
        /// </summary>
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            services.AddDataAccessLayer();
            services.AddApplicationLayer();

            services.AddLogging();

            services.AddAutoMapper(typeof(MappingProfile).Assembly);

            var alphaVantageSettingsSection = Configuration.GetSection(nameof(AlphaVantageSettings));
            services.Configure<AlphaVantageSettings>(alphaVantageSettingsSection);

            services.AddDbContext<StockSymbolsDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DatabaseConnection")));

            var pathToDoc = Path.Combine(AppContext.BaseDirectory, $"{typeof(Startup).Namespace}.xml");
            services.AddSwaggerGen(x =>
            {
                x.SwaggerDoc("v1",
                    new OpenApiInfo
                    {
                        Version = Version,
                        Title = ServiceName,
                        Description = $"WebAPI {ServiceName}.",
                    });
                x.IncludeXmlComments(pathToDoc);
            });
        }

        /// <summary>
        /// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        /// </summary>
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            using (var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>()?.CreateScope())
            {
                if (serviceScope != null)
                {
                    var context = serviceScope.ServiceProvider.GetRequiredService<StockSymbolsDbContext>();
                    context.Database.Migrate();
                }
            }

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseCors(builder => builder
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader()
            );

            app.UseSwagger(x => x.RouteTemplate = "help/{documentName}/swagger.json");
            app.UseSwaggerUI(x =>
            {
                x.RoutePrefix = "help";
                x.SwaggerEndpoint("v1/swagger.json", $"{ServiceName} API {Version}");
            });

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
