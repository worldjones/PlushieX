using System;
using System.ComponentModel.DataAnnotations;

namespace Shop.Catalog.Service.Dtos
{
    public record PlushieDto(Guid Id, string Name, string Description, string Color, decimal Price, DateTimeOffset CreatedDate);

    public record CreatePlushieDto([Required] string Name, string Description, string Color, [Range(0, 1000)] decimal Price);

    public record UpdatePlushieDto([Required] string Name, string Description, string Color, [Range(0, 1000)] decimal Price);
}

