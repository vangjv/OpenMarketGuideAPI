using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OMG.Domain.Market.Models;
using OMG.Infrastructure.CosmosDbData.Interfaces;
using OMG.Infrastructure.Extensions;

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
        public async Task<IEnumerable<ThreeDModelCollection>> Get()
        {
            return await _modelsRepo.GetItemsAsync("SELECT * FROM c");
        }

        [HttpGet("me")]
        public async Task<IEnumerable<ThreeDModelCollection>> GetMine()
        {
            var currentUserOid = User.GetOid();
            return await _modelsRepo.GetThreeDModelCollectionsAsyncByUser(currentUserOid);
        }

        // GET api/models/5
        [HttpGet("{id}")]
        public async Task<ThreeDModelCollection> Get(string id)
        {
            return await _modelsRepo.GetItemAsync(id);
        }

        //[Authorize]
        //// POST api/models
        //[HttpPost]
        //public async Task<ActionResult<ThreeDModelCollection>> PostAsync([FromBody] ThreeDModelCollection modelCollection)
        //{
        //    var userClaims = User.Claims;
        //    model.AddThreeDModelCollectionOwnerFromClaimsPrincipal(User);
        //    model.ThreeDModelCollectionEntityType = ThreeDModelCollectionEntityType.Template;
        //    var savedThreeDModelCollection = await _modelsRepo.AddItemAsync(model);
        //    return savedThreeDModelCollection;
        //}

    }
}
