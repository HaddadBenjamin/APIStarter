using System.Collections.Generic;

namespace ReadModel.Domain.Index
{
    public class Item
    {
        public string Name { get; set; }

        public List<ItemLocation> Locations { get; set; }
    }
}