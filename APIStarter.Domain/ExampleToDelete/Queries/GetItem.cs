using System;
using APIStarter.Domain.CQRS.Interfaces;
using APIStarter.Domain.ExampleToDelete.Views;

namespace APIStarter.Domain.ExampleToDelete.Queries
{
    public class GetItem : IQuery<ItemView>
    {
        public Guid Id { get; set; }
    }
}