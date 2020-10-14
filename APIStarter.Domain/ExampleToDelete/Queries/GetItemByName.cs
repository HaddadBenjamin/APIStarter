using APIStarter.Domain.CQRS.Interfaces;
using APIStarter.Domain.ExampleToDelete.Views;

namespace APIStarter.Domain.ExampleToDelete.Queries
{
    public class GetItemByName : IQuery<ItemView>
    {
        public string Name { get; set; }
    }
}