using APIStarter.Application.Filters;
using APIStarter.Application.Middlewares;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using WriteModel.Domain.Audit.Configuration;
using WriteModel.Domain.Audit.Services;
using WriteModel.Domain.AuthentificationContext;
using WriteModel.Domain.CQRS.Interfaces;
using WriteModel.Domain.ExampleToDelete.Builders;
using WriteModel.Domain.ExampleToDelete.Repositories;
using WriteModel.Infrastructure.Audit;
using WriteModel.Infrastructure.Audit.DbContext;
using WriteModel.Infrastructure.Audit.Services;
using WriteModel.Infrastructure.AuthentificationContext;
using WriteModel.Infrastructure.CQRS;
using WriteModel.Infrastructure.DbContext;
using WriteModel.Infrastructure.ExampleToRedefine.AuthentificationContext;
using WriteModel.Infrastructure.ExampleToRemove.Mappers;
using WriteModel.Infrastructure.ExampleToRemove.Repositories;
using IMediator = WriteModel.Domain.CQRS.Interfaces.IMediator;
using Mediator = WriteModel.Infrastructure.CQRS.Mediator;

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
            })// J'ai besoin d'appeler cette méthode pour fixer l'erreur JsonException: A possible object cycle was detected which is not supported. This can either be due t
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
                // Ces injections sont juste là pour l'exemple, il faudra les supprimer
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
