using System;
using System.Collections.Generic;

namespace ReadModel.Domain.Index
{
    public class Item
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public List<ItemLocation> Locations { get; set; }
    }
}