using System;

namespace Shop.Catalog.Contracts
{
    public record CatalogPlushieCreated(Guid PlushieId, string Name, string Description);

    public record CatalogPlushieUpdated(Guid PlushieId, string Name, string Description);

    public record CatalogPlushieDeleted(Guid PlushieId);
}