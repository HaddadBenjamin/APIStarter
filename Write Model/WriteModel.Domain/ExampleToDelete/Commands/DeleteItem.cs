using System;
using WriteModel.Domain.CQRS.Interfaces;

namespace WriteModel.Domain.ExampleToDelete.Commands
{
    public class DeleteItem : ICommand
    {
        public Guid Id { get; set; }
    }
}