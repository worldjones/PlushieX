using System;

namespace Shop.Inventory.Service.Dtos
{
    public record GrantPlushiesDto(Guid UserId, Guid CatalogPlushieId, int Quantity);

    public record InventoryPlushieDto(Guid CatalogPlushieId, string Name, string Description, int Quantity, DateTimeOffset AcquiredDate);

    public record CatalogPlushieDto(Guid Id, string Name, string Description);
}
