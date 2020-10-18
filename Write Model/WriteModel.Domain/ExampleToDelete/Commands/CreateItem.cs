using System;
using System.Collections.Generic;
using WriteModel.Domain.CQRS.Interfaces;

namespace WriteModel.Domain.ExampleToDelete.Commands
{
    public class CreateItem : ICommand
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public IEnumerable<string> Locations { get; set; }
    }
}