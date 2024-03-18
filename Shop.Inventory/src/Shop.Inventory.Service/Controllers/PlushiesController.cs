using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Shop.Inventory.Service.Clients;
using Shop.Inventory.Service.Dtos;
using Shop.Inventory.Service.Entities;
using Shop.Inventory.Service.Repositories;

namespace Shop.Inventory.Service.Controllers
{


    [ApiController]
    [Route("plushies")]
    public class PlushiesController : ControllerBase
    {
        private readonly IRepository<InventoryPlushie> plushiesRepository;
        private readonly CatalogClient catalogClient;

        public PlushiesController(IRepository<InventoryPlushie> plushiesRepository, CatalogClient catalogClient)
        {
            this.plushiesRepository = plushiesRepository;
            this.catalogClient = catalogClient;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<InventoryPlushieDto>>> GetAsync(Guid userId)
        {
            if (userId == Guid.Empty)
            {
                return BadRequest();
            }

            var catalogPlushies = await catalogClient.GetCatalogPlushiesAsync();
            var inventoryPlushieeEntities = await plushiesRepository.GetAllAsync(plushie => plushie.UserId == userId);

            var inventoryPlushieDtos = inventoryPlushieeEntities.Select(inventoryPlushie =>
            {
                var catalogPlushie = catalogPlushies.Single(catalogPlushie => catalogPlushie.Id == inventoryPlushie.CatalogPlushieId);
                return inventoryPlushie.AsDto(catalogPlushie.Name, catalogPlushie.Description);
            });

            return Ok(inventoryPlushieDtos);
        }

        [HttpPost]
        public async Task<ActionResult> PostAsync(GrantPlushiesDto grantPlushiesDto)
        {
            var inventoryPlushie = await plushiesRepository.GetAsync(
                plushies => plushies.UserId == grantPlushiesDto.UserId && plushies.CatalogPlushieId == grantPlushiesDto.CatalogPlushieId);

            if (inventoryPlushie == null)
            {
                inventoryPlushie = new InventoryPlushie
                {
                    CatalogPlushieId = grantPlushiesDto.CatalogPlushieId,
                    UserId = grantPlushiesDto.UserId,
                    Quantity = grantPlushiesDto.Quantity,
                    AcquiredDate = DateTimeOffset.UtcNow
                };

                await plushiesRepository.CreateAsync(inventoryPlushie);
            }
            else
            {
                inventoryPlushie.Quantity += grantPlushiesDto.Quantity;
                await plushiesRepository.UpdateAsync(inventoryPlushie);
            }

            return Ok();
        }

    }

}