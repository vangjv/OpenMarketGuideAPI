using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;
using OMG.Domain.Market.Enums;
using OMG.Domain.Market.Models;
using OMG.Infrastructure.CosmosDbData.Interfaces;
using OMG.Infrastructure.Extensions;
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
        // GET: api/markets
        [HttpGet]
        public async Task<IEnumerable<Market>> Get()
        {
            return await _marketRepo.GetItemsAsync("SELECT * FROM c");
        }

        [HttpGet("me")]
        public async Task<IEnumerable<Market>> GetMine()
        {
            var currentUserOid = User.GetOid();
            return await _marketRepo.GetMarketsAsyncByUser(currentUserOid);
        }

        // GET api/markets/5
        [HttpGet("{id}")]
        public async Task<Market> Get(string id)
        {
            return await _marketRepo.GetItemAsync(id);
        }

        [Authorize]
        // POST api/markets
        [HttpPost]
        public async Task<ActionResult<Market>> PostAsync([FromBody] Market market)
        {
            var userClaims = User.Claims;
            market.AddMarketOwnerFromClaimsPrincipal(User);
            market.MarketEntityType = MarketEntityType.Template;
            var savedMarket = await _marketRepo.AddItemAsync(market);
            return savedMarket;
        }

        [HttpPost]
        [Route("api/markets/{id}/vendors")]
        public async Task<ActionResult<Market>> AddVendor([FromBody] Vendor vendor, string id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var market = await _marketRepo.GetItemAsync($"{id}");
            if (market == null)
            {
                return NotFound();
            }
            market.Vendors.Add(vendor);
            var savedMarket = await _marketRepo.UpdateItemAsync(id, market);
            return savedMarket;
        }

        // PUT api/market/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/market/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
