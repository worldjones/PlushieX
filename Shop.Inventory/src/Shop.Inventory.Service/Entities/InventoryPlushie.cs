using System;

namespace Shop.Inventory.Service.Entities
{
    public class InventoryPlushie : IEntity
    {
        public Guid Id { get; set; }

        public Guid UserId { get; set; }

        public Guid CatalogPlushieId { get; set; }

        public int Quantity { get; set; }

        public DateTimeOffset AcquiredDate { get; set; }

    }
}
