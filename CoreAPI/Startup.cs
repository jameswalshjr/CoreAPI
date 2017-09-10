using AutoMapper;
using CoreAPI.Data.Repository;
using CoreAPI.Data.Repository.Interface;
using CoreAPI.Data.Resource;
using CoreAPI.Domain.Dto;
using CoreAPI.Domain.Entity;
using CoreAPI.Domain.Mapping;
using CoreAPI.Engine.BillingItemEngine;
using CoreAPI.Engine.Engine.Interface;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using NLog.Extensions.Logging;
using NLog.Web;

namespace CoreAPI
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
            HostingEnvironment = env;
        }

        public IConfigurationRoot Configuration { get; }
        public IHostingEnvironment HostingEnvironment { get; set; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddAutoMapper();

            if(HostingEnvironment.IsDevelopment())
            {
                services.AddCors(options =>
                {
                    options.AddPolicy("LocalHostPolicy",
                        builder => builder.AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowCredentials());
                });
            }
           
            
            services.AddEntityFrameworkSqlServer();
            services.AddAntiforgery(options => options.HeaderName = "X-XSRF-TOKEN");
            services.AddDbContext<DevSandBoxContext>(options => options.UseSqlServer(Configuration.GetConnectionString("DevSandBox"))); 
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<BillingItem, BillingItemEntity>();
                cfg.AddProfile(new MappingProfile());
            });
            var mapper = config.CreateMapper();
            services.AddSingleton(mapper);

            // Add framework services.
            services.AddMvc().AddJsonOptions(options =>
            {
                // The commented code is the correct way to eliminate a REST exploit within JSON.net
                 options.SerializerSettings.TypeNameHandling = TypeNameHandling.None ;
                //options.SerializerSettings.TypeNameHandling = TypeNameHandling.All;
            })
            .AddFluentValidation();


            services.AddScoped<IBillingItemEngine, BillingItemEngine>();
            services.AddScoped<IBillingItemRepository, BillingItemRepository>();
           
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            // Add NLog
            loggerFactory.AddNLog();
            
            env.ConfigureNLog("nlog.config");

            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();
            

            app.UseMvc();
        }
    }
}
