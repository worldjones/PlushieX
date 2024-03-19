using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MassTransit;
using MassTransit.Definition;
using MassTransit.RabbitMqTransport.Hosting;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;
using MongoDB.Driver;
using Shop.Catalog.Service.Entities;
using Shop.Catalog.Service.Repositories;
using Shop.Catalog.Service.Settings;
using RabbitMQ;


namespace Shop.Catalog.Service
{
    public class Startup
    {

        private ServiceSettings serviceSettings;

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            serviceSettings = Configuration.GetSection(nameof(ServiceSettings)).Get<ServiceSettings>();

            services.AddMongo()
                    .AddMongoRepository<Plushie>("plushies");

 //           services.AddMassTransit(x =>
 //           {
 //               x.UsingRabbitMq((context, configurator) =>
 //               {
 //                   var RabbitMQSettings = Configuration.GetSection(nameof(RabbitMqSettings)).Get<RabbitMqSettings>();
 //                   configurator.Host(RabbitMQSettings.Host);
 //                   configurator.ConfigureEndpoints(context, new KebabCaseEndpointNameFormatter(serviceSettings.ServiceName, false));
 //               });
 //           });

          //  services.AddMassTransitHostedService();

            services.AddControllers(options =>
            {
                options.SuppressAsyncSuffixInActionNames = false;
            });
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Shop.Catalog.Service", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Shop.Catalog.Service v1"));
            }

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
