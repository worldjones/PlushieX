using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MassTransit;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Mvc;
using Shop.Catalog.Service.Dtos;
using Shop.Catalog.Service.Entities;
using Shop.Catalog.Service.Repositories;

namespace Shop.Catalog.Service.Controllers
{
    // https://localhost:5001/plushies
    [ApiController]
    [Route("plushies")]
    public class PlushiesController : ControllerBase
    {

        private readonly IRepository<Plushie> plushiesRepository;
        private readonly IPublishEndpoint publishEndpoint;

        public PlushiesController(IRepository<Plushie> plushiesRepository, IPublishEndpoint publishEndpoint)
        {
            this.plushiesRepository = plushiesRepository;
            this.publishEndpoint = publishEndpoint;
        }


        [HttpGet]
        public async Task<IEnumerable<PlushieDto>> GetAsync()
        {
            var plushies = (await plushiesRepository.GetAllAsync())
                            .Select(plushie => plushie.AsDto());
            return plushies;
        }


        //GET /plushies/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<PlushieDto>> GetByIdAsync(Guid id)
        {
            var plushie = await plushiesRepository.GetAsync(id);

            if (plushie == null)
            {
                return NotFound();
            }
            return plushie.AsDto();
        }

        //POST /plushies
        [HttpPost]
        public async Task<ActionResult<PlushieDto>> PostAsync(CreatePlushieDto createPlushieDto)
        {
            var plushie = new Plushie
            {
                Name = createPlushieDto.Name,
                Description = createPlushieDto.Description,
                Color = createPlushieDto.Color,
                Price = createPlushieDto.Price,
                CreatedDate = DateTimeOffset.UtcNow
            };

            await plushiesRepository.CreateAsync(plushie);

            //         await publishEndpoint.Publish(new CatalogPlushieCreated(plushie.Id, plushie.Name, plushie.Description));


            return CreatedAtAction(nameof(GetByIdAsync), new { id = plushie.Id }, plushie);
        }

        //PUT /plushies/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAsync(Guid id, UpdatePlushieDto updatePlushieDto)
        {
            var existingPlushie = await plushiesRepository.GetAsync(id);

            if (existingPlushie == null)

            {
                return NotFound();
            }

            existingPlushie.Name = updatePlushieDto.Name;
            existingPlushie.Description = updatePlushieDto.Description;
            existingPlushie.Color = updatePlushieDto.Color;
            existingPlushie.Price = updatePlushieDto.Price;

            await plushiesRepository.UpdateAsync(existingPlushie);

            //           await publishEndpoint.Publish(new CatalogPlushieUpdated(existingPlushie.Id, existingPlushie.Name, existingPlushie.Description));

            return NoContent();

        }

        //DELETE /plushies/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(Guid id)
        {
            var plushie = await plushiesRepository.GetAsync(id);

            if (plushie == null)

            {
                return NotFound();
            }

            await plushiesRepository.RemoveAsync(plushie.Id);

            //           await publishEndpoint.Publish(new CatalogPlushieDeleted(id));

            return NoContent();
        }

    }

}