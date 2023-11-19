using Microsoft.AspNetCore.Mvc;
using OMG.Infrastructure.CosmosDbData.Interfaces;
using OMG.API.Extensions;
using OMG.Domain.Vendor.Models;
using Microsoft.AspNetCore.Authorization;
using OMG.Infrastructure.CosmosDbData.DatabaseModel;

namespace OMG.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VendorsController : ControllerBase
    {
        IVendorRepository _vendorsRepo;
        public VendorsController(IVendorRepository vendorsRep)
        {
            _vendorsRepo = vendorsRep;
        }

        [Authorize]
        [HttpGet("me")]
        public async Task<ActionResult<Vendor>> GetMyVendor()
        {
            var currentUserOid = User.GetOid();
            var vendor = await _vendorsRepo.GetItemAsync(currentUserOid);
            if (vendor == null)
            {
                return NotFound();
            }
            return Ok(vendor.ToVendor());
        }


        [Authorize]
        // POST api/vendors
        [HttpPost]
        public async Task<ActionResult<Vendor>> PostAsync([FromBody] Vendor newVendor)
        {
            var currentUserOid = User.GetOid();
            //check if vendor already exist with oid
            var vendor = await _vendorsRepo.GetItemAsync(currentUserOid);
            if (vendor != null)
            {
                return BadRequest("A vendor already exist");
            }
            var vendorDM = new VendorDM(newVendor);
            vendorDM.Id = currentUserOid;
            var createdVendor = await _vendorsRepo.AddItemAsync(vendorDM);
            return Ok(createdVendor.ToVendor());
        }

    }
}
