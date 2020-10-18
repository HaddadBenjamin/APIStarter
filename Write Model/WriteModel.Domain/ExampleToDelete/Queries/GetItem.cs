using System;
using WriteModel.Domain.CQRS.Interfaces;
using WriteModel.Domain.ExampleToDelete.Views;

namespace WriteModel.Domain.ExampleToDelete.Queries
{
    public class GetItem : IQuery<ItemView>
    {
        public Guid Id { get; set; }
    }
}