using System;
using System.Collections.Generic;

namespace WriteModel.Domain.ExampleToDelete.Views
{
    public class ItemView
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public IEnumerable<ItemLocationView> Locations { get; set; }
    }
}
