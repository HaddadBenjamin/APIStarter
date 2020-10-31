using System;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using ReadModel.Domain.Aliases;
using ReadModel.Domain.Clients;
using ReadModel.Domain.Configurations;
using ReadModel.Domain.Indexes;
using ReadModel.Domain.Readers;
using ReadModel.Domain.WriteModel.Configurations;
using ReadModel.Domain.WriteModel.SqlConnections;
using ReadModel.Infrastructure.Aliases;
using ReadModel.Infrastructure.Clients;
using ReadModel.Infrastructure.Indexes;
using ReadModel.Infrastructure.Readers;
using ReadModel.Infrastructure.WriteModel.Readers;
using ReadModel.Infrastructure.WriteModel.SqlConnections;

namespace ReadModel.Application
{
    public class Startup
    {
        private static readonly Type InfrastructureType = typeof(IndexRefresher);
        private static readonly Type InfrastructureWriteModelType = typeof(ItemReader);

        private readonly IConfiguration _configuration;

        public Startup(IConfiguration configuration) => _configuration = configuration;

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc(options => options.EnableEndpointRouting = false)
            // J'ai besoin d'appeler cette méthode pour fixer l'erreur JsonException: A possible object cycle was detected which is not supported.
            .AddNewtonsoftJson(options => options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore);

            services.AddAutoMapper(InfrastructureType, InfrastructureWriteModelType);

            services
                .AddSwaggerGen(options =>
                {
                    options.SwaggerDoc("v1", new OpenApiInfo
                    {
                        Version = "v1",
                        Title = "CQRS - Read Model",
                        Contact = new OpenApiContact
                        {
                            Name = "Un passionné dans la foule (alias Firefouks)",
                            Url = new Uri("https://github.com/HaddadBenjamin")
                        }
                    });

                    options.DescribeAllEnumsAsStrings();
                });

            // Configurations.
            services
                .AddSingleton(new WriteModelConfiguration { ConnectionString = _configuration.GetConnectionString("WriteModel") })
                .AddSingleton(new AuditConfiguration { ConnectionString = _configuration.GetConnectionString("Audit") })
                .AddSingleton(_configuration.GetSection("ReadModel").Get<ReadModelConfiguration>());

            // Infrastructure.
            services
                .AddSingleton<IReadModelClient, ReadModelClient>()
                // Index.
                .AddSingleton<IIndexNameWithAlias, IndexNameWithAlias>()
                .AddSingleton<IIndexCleaner, IndexCleaner>()
                .AddSingleton<IIndexMapper, IndexMapper>()
                .AddSingleton<IIndexRebuilder, IndexRebuilder>()
                .AddSingleton<IIndexRefresher, IndexRefresher>()
                .AddSingleton<IIndexDocumentInserter, IndexDocumentInserter>()
                // Alias.
                .AddSingleton<IAliasSwapper, AliasSwapper>()
                .AddSingleton<IAliasContainsWithoutIndex, AliasContainsWithoutIndex>()
                .AddSingleton<IAliasAdder, AliasAdder>()
                .AddSingleton<IAliasRemoval, AliasRemoval>()
                // Mapper.
                .AddSingleton<IViewToDocumentMapper, ViewToDocumentMapper>()
                .AddSingleton<IWriteModelReader, WriteModelReader>();

            // Infrastructure.WriteModel.
            services
                // Reader.
                .AddSingleton<HttpRequestReader>()
                .AddSingleton<ItemReader>()
                // Sql Connection.
                .AddSingleton<IAuditSqlConnection, AuditSqlConnection>()
                .AddSingleton<IWriteModelSqlConnection, WriteModelSqlConnection>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
                app.UseDeveloperExceptionPage();

            app.UseMvc();

            app
                .UseSwagger()
                .UseSwaggerUI(options =>
                {
                    options.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
                    options.RoutePrefix = string.Empty;
                });
        }
    }
}
