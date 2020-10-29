using System.Collections.Generic;

namespace ReadModel.Domain.Index.Item
{
    public class Item
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public List<ItemLocation> Locations { get; set; }
    }
}