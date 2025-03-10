using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Confluent.Kafka;
using Kafka.SignalR;
using Kafka.SignalR.Extensions;
using Kafka.SignalR.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace KafkaConsumerService
{
    public class Startup
    {
        private readonly string _corsPolicy = "AllowOrigin";
        private readonly IConfiguration Configuration;
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            
            services.Configure<CookiePolicyOptions>(options =>
            {
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });
            services.AddSingleton<IKafkaMessageProcessService, KafkaMessageProcessService>();
            services.AddCors(options =>
            {
                var allowedOrigins = Configuration["AllowOrigins"].Split(",");
                options.AddPolicy(name: _corsPolicy,
                                  builder =>
                                  {
                                      builder
                                        .AllowAnyMethod()
                                        .AllowAnyHeader()
                                        .AllowCredentials()
                                        .WithOrigins(allowedOrigins);

                                  });
            });
            services.AddKafkaSignalR(Configuration.GetSection("Kafka"));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();
            app.UseCors(_corsPolicy);
            app.UseEndpoints(endpoints =>
            {
                endpoints
                .MapHub<KafkaSignalRHub>(Configuration["Kafka:Hub"])
                .RequireCors(_corsPolicy);
                endpoints
                .MapGet("/", async context => { 
                    await context.Response.WriteAsync("Running Kafka Service...."); 
                })
                .RequireCors(_corsPolicy);
            });
        }
    }
}
