using Shop.Inventory.Service.Entities;
using Shop.Inventory.Service.Dtos;

namespace Shop.Inventory.Service

{
    public static class Extensions
    {
        public static InventoryPlushieDto AsDto(this InventoryPlushie plushie, string name, string description)
        {
            return new InventoryPlushieDto(plushie.CatalogPlushieId, name, description, plushie.Quantity, plushie.AcquiredDate);
        }
    }
}