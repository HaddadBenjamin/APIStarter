﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using APIStarter.Domain.Audit.Attributes;
using APIStarter.Domain.Audit.Commands;
using APIStarter.Domain.Audit.Configuration;
using APIStarter.Domain.CQRS.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace APIStarter.Infrastructure.CQRS
{
    /// <summary>
    /// Mediator is a behavioral design pattern that lets you reduce chaotic dependencies between objects. The pattern restricts direct communications between the objects and forces them to collaborate only via a mediator object.
    /// </summary>
    public class Mediator : IMediator
    {
        private readonly AuditConfiguration _auditConfiguration;
        private readonly MediatR.IMediator _mediator;
        private readonly IServiceScope _serviceScope;

        public Mediator(IServiceScopeFactory serviceScopeFactory, AuditConfiguration auditConfiguration)
        {
            _auditConfiguration = auditConfiguration;
            _serviceScope = serviceScopeFactory.CreateScope();
            _mediator = _serviceScope.ServiceProvider.GetRequiredService<MediatR.IMediator>();
        }

        public async Task SendCommand(ICommand command)
        {
            if (command is null)
                throw new ArgumentNullException(nameof(command));

            await _mediator.Send(command);

            if (_auditConfiguration.AuditCommands && command.GetType().ShouldAudit())
                await _mediator.Send(new CreateAuditCommand { Command = command });
        }

        public async Task<TQueryResult> SendQuery<TQueryResult>(IQuery<TQueryResult> query)
        {
            if (query is null)
                throw new ArgumentNullException(nameof(query));

            var queryResult = await _mediator.Send(query);

            if (_auditConfiguration.AuditQueries && query.GetType().ShouldAudit())
                await _mediator.Send(new CreateAuditQuery
                {
                    Query = query,
                    QueryResult = queryResult
                });

            return queryResult;
        }

        public async Task PublishEvents(IReadOnlyCollection<IEvent> events)
        {
            if (events is null)
                throw new ArgumentNullException(nameof(events));

            await Task.WhenAll(events.Select(@event => _mediator.Publish(@event)));

            if (_auditConfiguration.AuditEvents)
            {
                var eventsToAudit = events.Where(@event => @event.GetType().ShouldAudit()).ToList();

                await _mediator.Send(new CreateAuditEvents {Events = eventsToAudit });
            }
        }
    }
}