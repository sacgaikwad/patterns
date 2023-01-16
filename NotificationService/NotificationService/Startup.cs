using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.OpenApi.Models;
using System.IO;
using System.Text.Json.Serialization;
using NotificationService.Interface;
using NotificationService.Model;
using NotificationService.Implementation;

namespace NotificationService
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddScoped<EmailNotificationProvider>();
            services.AddScoped<SmsNotificationProvider>();

            services.AddScoped<Func<TokenType, INotificationProvider>>(ser => key =>
            {
                switch (key)
                {
                    case TokenType.Email:
                        return ser.GetService<EmailNotificationProvider>();
                    case TokenType.MobilePhone:
                        return ser.GetService<SmsNotificationProvider>();
                    default:
                        throw new NotSupportedException("provider not supported");
                }
            });

            services.AddScoped<INotificationService, NotiticationServiceImpl>();
            
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "NotificationService", Version = "v1" });
                var files = Directory.GetFiles(System.AppContext.BaseDirectory, "*.xml");
                foreach (var xmlDocs in files)
                {
                    c.IncludeXmlComments(xmlDocs);
                }
                c.DescribeAllParametersInCamelCase();
                c.UseInlineDefinitionsForEnums();
            });

            services.AddControllers().AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
                options.JsonSerializerOptions.IgnoreNullValues = true;
            });

            // services.AddSwaggerExamplesFromAssemblyOf<Startup>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseAuthorization();
            app.UseSwagger();
            app.UseSwaggerUI();
            app.UseStaticFiles();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
