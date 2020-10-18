using System;
using System.Collections.Generic;
using System.Linq;
using APIStarter.Domain.ExampleToDelete.Commands;
using APIStarter.Domain.ExampleToDelete.Events;
using APIStarter.Domain.Extensions;

namespace APIStarter.Domain.ExampleToDelete.Aggregates
{
    public class Item : CQRS.AggregateRoot
    {
        public string Name { get; set; }

        public HashSet<ItemLocation> Locations { get; set; }

        public Item Create(CreateItem command)
        {
            Id = command.Id;
            Name = command.Name;
            Locations = command.Locations.Select(l => new ItemLocation
            {
                Id = Guid.NewGuid(),
                ItemId = Id,
                Name = l
            }).ToHashSet();

            RaiseEvent(new ItemWriteEvent(Id));

            return this;
        }

        public void Update(UpdateItem command)
        {
            Name = command.Name;
            Locations = command.Locations.Select(l => new ItemLocation
            {
                Id = Guid.NewGuid(),
                ItemId = Id,
                Name = l
            }).ToHashSet();

            RaiseEvent(new ItemWriteEvent(Id));
        }

        public void Deactivate(DeleteItem command)
        {
            IsActive = false;

            RaiseEvent(new ItemWriteEvent(Id));
        }
    }
}