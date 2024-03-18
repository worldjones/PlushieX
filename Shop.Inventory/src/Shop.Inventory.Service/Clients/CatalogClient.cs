using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Shop.Inventory.Service.Dtos;

namespace Shop.Inventory.Service.Clients
{

    public class CatalogClient
    {
        private readonly HttpClient httpClient;

        public CatalogClient(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public async Task<IReadOnlyCollection<CatalogPlushieDto>> GetCatalogPlushiesAsync()
        {
            var plushies = await httpClient.GetFromJsonAsync<IReadOnlyCollection<CatalogPlushieDto>>("/plushies");
            return plushies;
        }
    }
}