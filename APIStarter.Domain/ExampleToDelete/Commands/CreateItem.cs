using System;
using System.Collections.Generic;
using APIStarter.Domain.CQRS.Interfaces;

namespace APIStarter.Domain.ExampleToDelete.Commands
{
    public class CreateItem : ICommand
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public IEnumerable<string> Locations { get; set; }
    }
}