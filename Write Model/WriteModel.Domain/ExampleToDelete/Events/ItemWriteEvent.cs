using System;
using WriteModel.Domain.CQRS;

namespace WriteModel.Domain.ExampleToDelete.Events
{
    public class ItemWriteEvent : Event
    {
        public ItemWriteEvent(Guid itemId) => ItemId = itemId;

        public Guid ItemId { get; set; }
    }
}
