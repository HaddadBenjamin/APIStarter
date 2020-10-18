using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using ReadModel.Application.Filters;
using ReadModel.Domain.Configurations;
using ReadModel.Domain.WriteModel.Configurations;

namespace ReadModel.Application
{
    public class Startup
    {
        private readonly IConfiguration _configuration;

        public Startup(IConfiguration configuration) => _configuration = configuration;

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc(options =>
            {
                options.EnableEndpointRouting = false;
                options.Filters.Add(new ExceptionHandlerFilter());
            })// J'ai besoin d'appeler cette méthode pour fixer l'erreur JsonException: A possible object cycle was detected which is not supported. This can either be due t
            .AddNewtonsoftJson(options => options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore);

            services.AddSingleton(new WriteModelConfiguration { ConnectionString = _configuration.GetConnectionString("WriteModel") });
            services.AddSingleton(new AuditConfiguration { ConnectionString = _configuration.GetConnectionString("Audit") });
            services.AddSingleton(_configuration.GetSection("ReadModel").Get<ReadModelConfiguration>());
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
                app.UseDeveloperExceptionPage();

            app.UseMvc();
        }
    }
}
