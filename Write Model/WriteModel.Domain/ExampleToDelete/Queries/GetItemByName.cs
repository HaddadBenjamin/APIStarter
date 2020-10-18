using WriteModel.Domain.CQRS.Interfaces;
using WriteModel.Domain.ExampleToDelete.Views;

namespace WriteModel.Domain.ExampleToDelete.Queries
{
    public class GetItemByName : IQuery<ItemView>
    {
        public string Name { get; set; }
    }
}