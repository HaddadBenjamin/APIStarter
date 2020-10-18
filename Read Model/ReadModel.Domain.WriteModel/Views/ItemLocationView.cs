using System;

namespace ReadModel.Domain.WriteModel.Views
{
    public class ItemLocationView
    {
        public Guid ItemId { get; set; }
        public Guid Id { get; set; }
        public string Name { get; set; }
    }
}