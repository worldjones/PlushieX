using System;

namespace Shop.Catalog.Service.Entities
{

    public class Plushie : IEntity
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string Color { get; set; }

        public decimal Price { get; set; }

        public DateTimeOffset CreatedDate { get; set; }


    }
}