using System;
using System.Collections.Generic;

namespace ReadModel.Domain.WriteModel.Views
{
    public class ItemView
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public List<ItemLocationView> Locations { get; set; }
    }
}