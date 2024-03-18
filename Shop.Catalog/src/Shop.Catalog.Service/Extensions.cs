using Shop.Catalog.Service.Dtos;
using Shop.Catalog.Service.Entities;

namespace Shop.Catalog.Service
{
    public static class Extensions
    {
        public static PlushieDto AsDto(this Plushie plushie)
        {
            return new PlushieDto(plushie.Id, plushie.Name, plushie.Description, plushie.Color, plushie.Price, plushie.CreatedDate);
        }
    }
}