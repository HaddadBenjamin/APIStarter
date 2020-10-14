using System;

namespace APIStarter.Domain.ExampleToDelete.Commands
{
    public class UpdateItem : CreateItem
    {
        public Guid Id { get; set; }
    }
}