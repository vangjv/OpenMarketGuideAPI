using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OMG.Domain.Market.Models;
using OMG.Infrastructure.CosmosDbData.Interfaces;
using OMG.API.Extensions;

namespace OMG.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ModelsController : ControllerBase
    {
        IThreeDModelCollectionRepository _modelsRepo;
        public ModelsController(IThreeDModelCollectionRepository modelsRepo)
        {
            _modelsRepo = modelsRepo;
        }
        // GET: api/models
        [HttpGet]
        public async Task<ActionResult<ThreeDModelCollection>> Get()
        {
            var threeDModelCollection = await _modelsRepo.GetItemAsync("public");
            return Ok(threeDModelCollection.ToThreeDModelCollection());
        }

        [Authorize]
        [HttpGet("me")]
        public async Task<ThreeDModelCollection> GetMine()
        {
            var currentUserOid = User.GetOid();
            return await _modelsRepo.GetItemAsync(currentUserOid);
        }
    }
}
