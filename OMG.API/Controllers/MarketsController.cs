using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;
using OMG.Domain.Market.Models;
using OMG.Infrastructure.CosmosDbData.Interfaces;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace OMG.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MarketsController : ControllerBase
    {
        IMarketRepository _marketRepo;
        public MarketsController(IMarketRepository marketRepo)
        {
            _marketRepo = marketRepo;
        }
        // GET: api/<MarketController>
        [HttpGet]
        public async Task<IEnumerable<Market>> Get()
        {
            return await _marketRepo.GetItemsAsync("SELECT * FROM c");
        }

        // GET api/<MarketController>/5
        [HttpGet("{id}")]
        public async Task<Market> Get(string id)
        {
            return await _marketRepo.GetItemAsync(id);
        }

        // POST api/<MarketController>
        [HttpPost]
        public async Task<ActionResult<Market>> PostAsync([FromBody] Market market)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var savedMarket = await _marketRepo.AddItemAsync(market);
            return savedMarket;
        }

        // PUT api/<MarketController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<MarketController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
