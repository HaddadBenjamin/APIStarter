using APIStarter.Application.Filters;
using APIStarter.Application.Middlewares;
using APIStarter.Domain.Audit.Configuration;
using APIStarter.Domain.Audit.Services;
using APIStarter.Domain.AuthentificationContext;
using APIStarter.Domain.CQRS.Interfaces;
using APIStarter.Domain.ExampleToDelete.Builders;
using APIStarter.Domain.ExampleToDelete.Repositories;
using APIStarter.Infrastructure.Audit;
using APIStarter.Infrastructure.Audit.DbContext;
using APIStarter.Infrastructure.Audit.Services;
using APIStarter.Infrastructure.AuthentificationContext;
using APIStarter.Infrastructure.CQRS;
using APIStarter.Infrastructure.DbContext;
using APIStarter.Infrastructure.ExampleToRedefine.AuthentificationContext;
using APIStarter.Infrastructure.ExampleToRemove.Mappers;
using APIStarter.Infrastructure.ExampleToRemove.Repositories;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using IMediator = APIStarter.Domain.CQRS.Interfaces.IMediator;
using Mediator = APIStarter.Infrastructure.CQRS.Mediator;

namespace APIStarter.Application
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
            })// J'ai besoin d'appeler cette m�thode pour fixer l'erreur JsonException: A possible object cycle was detected which is not supported. This can either be due t
                .AddNewtonsoftJson(options => options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore);

            services
                // Register CQRS : mediator / session / repository / unit of work.
                .AddMediatR(typeof(Mediator))
                .AddScoped<IMediator, Mediator>()
                .AddScoped(typeof(ISession<>), typeof(Session<>))
                .AddScoped(typeof(ISession<,>), typeof(Session<,>))
                .AddScoped<IUnitOfWork, GenericUnitOfWork>()
                .AddScoped(typeof(IRepository<>), typeof(GenericRepository<>))
                // Register Audit & Authentification Context :
                .AddSingleton(_configuration.GetSection("Audit").Get<AuditConfiguration>())
                .AddScoped<IAuditSerializer, AuditSerializer>()
                .AddScoped<IDatabaseChangesAuditService, GenericsDatabaseChangesAuditService>()
                .AddSingleton<IHttpContextAccessor, HttpContextAccessor>()
                .AddScoped<IRequestHeaders, RequestHeaders>()
                .AddScoped<IAuthentificationContext, AuthentificationContext>()
                // Register Db context.
                .AddDbContextPool<AuditDbContext>(options => options.UseSqlServer(_configuration.GetConnectionString("Audit"), builder => builder.MigrationsHistoryTable("MigrationHistory", "dbo")))
                .AddDbContextPool<YourDbContext>(options => options.UseSqlServer(_configuration.GetConnectionString("WriteModel"), builder => builder.MigrationsHistoryTable("MigrationHistory", "dbo")))
                // Ces injections sont juste l� pour l'exemple, il faudra les supprimer
                .AddScoped<IAuthentificationContextUserProvider, FakeAuthentificationContextUserProvider>()
                .AddScoped<IItemViewMapper, ItemViewMapper>()
                .AddScoped<IItemRepository, ItemRepository>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
                app.UseDeveloperExceptionPage();

            using (var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
            {
                serviceScope.ServiceProvider.GetRequiredService<YourDbContext>().Database.Migrate();
                serviceScope.ServiceProvider.GetRequiredService<AuditDbContext>().Database.Migrate();
            }

            app.UseMvc();
            app.UseMiddleware<AuditRequestMiddleware>();
        }
    }
}
