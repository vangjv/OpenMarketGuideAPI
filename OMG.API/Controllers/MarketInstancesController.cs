using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OMG.Domain.Market.Enums;
using OMG.Domain.Market.Models;
using OMG.Infrastructure.CosmosDbData.Interfaces;
using OMG.API.Extensions;
using OMG.API.Models;
// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace OMG.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MarketInstancesController : ControllerBase
    {
        IMarketInstanceRepository _marketInstanceRepo;
        IMarketRepository _marketRepo;
        public MarketInstancesController(IMarketInstanceRepository marketInstanceRepo, IMarketRepository marketRepo)
        {
            _marketInstanceRepo = marketInstanceRepo;
            _marketRepo = marketRepo;
        }


        // GET api/marketinstances/5
        [HttpGet("{id}")]
        public async Task<MarketInstance> Get(string id)
        {
            return await _marketInstanceRepo.GetItemAsync(id);
        }

        // GET api/marketinstances/bymarketid/{id}
        [HttpGet("bymarketid/{id}")]
        public async Task<IEnumerable<MarketInstance>> GetByMarketId(string id)
        {
            return await _marketInstanceRepo.GetMarketInstancesByMarketIdAsync(id);
        }

        [Authorize]
        // POST api/marketinstance
        [HttpPost]
        public async Task<ActionResult<Market>> PostAsync([FromBody] MarketInstanceRequest marketInstanceReq)
        {
            //check if market exist
            var market = await _marketRepo.GetItemAsync(marketInstanceReq.MarketId);
            if (market == null)
            {
                return NotFound();
            }
            //check if user has access to create instance
            if (!market.UserIsMarketOwner(User)) {
                return Unauthorized();
            }
            //create new instance from market
            var savedMarket = await _marketInstanceRepo.AddItemAsync(market.ToMarketInstance(marketInstanceReq.StartDate, marketInstanceReq.EndDate));
            return Ok(savedMarket);
        }

        // PUT api/market/5
        [Authorize]
        [HttpPut("{id}")]
        public async Task<ActionResult<Market>> Put(string id, [FromBody] MarketInstance marketInstanceUpdate)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var marketInstance = await _marketInstanceRepo.GetItemAsync($"{id}");
            if (marketInstance == null)
            {
                return NotFound();
            }
            if (marketInstance.UserIsMarketOwner(User) || marketInstance.UserIsMarketVendor(User))
            {
                marketInstance.ThreeDModelEntities = marketInstanceUpdate.ThreeDModelEntities;
                marketInstance.VendorLocations = marketInstanceUpdate.VendorLocations;
                marketInstance.MarketLocation = marketInstanceUpdate.MarketLocation;
                var savedMarketInstance = await _marketInstanceRepo.UpdateItemAsync(id, marketInstance);
                return Ok(savedMarketInstance); 
            } else
            {
                return Unauthorized();
            }       
        }
    }
}
