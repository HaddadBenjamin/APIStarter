﻿using System;

namespace APIStarter.Domain.ExampleToDelete.Aggregates
{
    public class ItemLocation
    {
        public Guid Id { get; set; }
        public Guid ItemId { get; set; }
        public Item Item { get; set; }
        public string Name { get; set; }
    }
}