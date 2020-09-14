using AutoMapper;
using Library.API.Entities;
using Library.API.Extensions;
using Library.API.Filters;
using Library.API.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NLog.Extensions.Logging;
using NLog.Web;

namespace Library.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app,
            IHostingEnvironment env,
            IMapper Mapper,
            ILoggerFactory loggerFactory)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            loggerFactory.AddNLog();
            env.ConfigureNLog("nlog.config");
            app.UseHttpsRedirection();

            app.UseResponseCaching();
            app.UseMvc();
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc(config =>
            {
                config.EnableEndpointRouting = false;
                config.Filters.Add<JsonExceptionFilter>();

                config.ReturnHttpNotAcceptable = true;
                config.OutputFormatters.Add(new XmlSerializerOutputFormatter());

                config.CacheProfiles.Add("Default",
                    new CacheProfile()
                    {
                        Duration = 60
                    });

                config.CacheProfiles.Add("Never",
                    new CacheProfile()
                    {
                        Location = ResponseCacheLocation.None,
                        NoStore = true
                    });
            }).SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
            services.AddAutoMapper();
            services.AddDbContext<LibraryDbContext>(config => config.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
            services.AddScoped<IRepositoryWrapper, RepositoryWrapper>();
            services.AddScoped<CheckAuthorExistFilterAttribute>();
            services.AddGraphQLSchemaAndTypes();
            services.AddSingleton<IHashFactory, HashFactory>();
            services.AddResponseCaching();
            services.AddMemoryCache();
            services.AddDistributedRedisCache(options =>
            {
                options.Configuration = Configuration["Caching:Host"];
                options.InstanceName = Configuration["Caching:Instance"];
            });

            //services.AddApiVersioning(options =>
            //{
            //    options.ReportApiVersions = true;
            //    options.DefaultApiVersion = new ApiVersion(1, 0);
            //    options.AssumeDefaultVersionWhenUnspecified = true;

            //    // options.ApiVersionReader = new QueryStringApiVersionReader("ver");
            //    // options.ApiVersionReader = new HeaderApiVersionReader("api-version");
            //    //options.ApiVersionReader = new MediaTypeApiVersionReader();

            //    options.ApiVersionReader = ApiVersionReader.Combine(
            //        new MediaTypeApiVersionReader(),
            //        new QueryStringApiVersionReader("api-version"));

            //    options.Conventions.Controller<Controllers.V1.ProjectController>()
            //        .HasApiVersion(new ApiVersion(1, 0))
            //        .HasDeprecatedApiVersion(new ApiVersion(1, 0));

            //    options.Conventions.Controller<Controllers.V2.ProjectController>()
            //        .HasApiVersion(new ApiVersion(2, 0));
            //});
        }
    }
}