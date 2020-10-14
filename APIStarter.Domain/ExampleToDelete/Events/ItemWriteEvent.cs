using System;
using APIStarter.Domain.CQRS;

namespace APIStarter.Domain.ExampleToDelete.Events
{
    public class ItemWriteEvent : Event
    {
        public ItemWriteEvent(Guid itemId) => ItemId = itemId;

        public Guid ItemId { get; set; }
    }
}
