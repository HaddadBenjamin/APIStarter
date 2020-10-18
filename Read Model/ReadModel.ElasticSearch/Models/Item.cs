using System.Collections.Generic;

namespace ReadModel.ElasticSearch
{
    public class Item
    {
        public string Name { get; set; }

        public List<ItemLocation> Locations { get; set; }
    }
}