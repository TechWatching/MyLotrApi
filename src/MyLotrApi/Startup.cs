using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using MyLotrApi.Configurations;
using MyLotrApi.Middlewares;
using MyLotrApi.Services;
using MyLotrApi.Services.HttpMessageHandlers;
using Refit;
using System;
using System.Net.Http.Headers;

namespace MyLotrApi
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
            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "MyLotrApi", Version = "v1" });
            });
            services.Configure<TheOneApiConfiguration>(Configuration.GetSection("TheOneApi"));
            services.AddTransient<TheOneApiErrorDelegatingHandler>();

            services.AddRefitClient<ITheOneApiService>()
                    .ConfigureHttpClient((serviceProvider, httpClient) =>
                    {
                        var apiConfiguration = serviceProvider.GetRequiredService<IOptions<TheOneApiConfiguration>>().Value;
                        httpClient.BaseAddress = new Uri(apiConfiguration.BaseUrl);
                        httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", apiConfiguration.ApiKey);
                    })
                    .AddHttpMessageHandler<TheOneApiErrorDelegatingHandler>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "MyLotrApi v1"));
            }

            app.UseMiddleware<ErrorHandlingMiddleware>();

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
