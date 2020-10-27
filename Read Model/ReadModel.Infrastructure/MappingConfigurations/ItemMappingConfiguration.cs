using AutoMapper;
using ReadModel.Domain.Index.Item;
using ReadModel.Domain.WriteModel.Views;

namespace ReadModel.Infrastructure.MappingConfigurations
{
    public class ItemMappingConfiguration : Profile
    {
        public ItemMappingConfiguration()
        {
            CreateMap<ItemView, Item>();
            CreateMap<ItemLocationView, ItemLocation>();
        }
    }
}
