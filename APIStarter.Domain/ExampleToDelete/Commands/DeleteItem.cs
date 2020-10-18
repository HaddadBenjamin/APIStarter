using System;
using APIStarter.Domain.CQRS.Interfaces;

namespace APIStarter.Domain.ExampleToDelete.Commands
{
    public class DeleteItem : ICommand
    {
        public Guid Id { get; set; }
    }
}