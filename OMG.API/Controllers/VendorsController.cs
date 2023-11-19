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
        public async Task<ActionResult<List<Vendor>>> GetMyVendor()
        {
            var currentUserOid = User.GetOid();
            var vendors = await _vendorsRepo.GetMyVendorsAsync(currentUserOid);
            if (vendors == null)
            {
                return NotFound();
            }
            var vendorList = new List<Vendor>();
            vendors.ToList().ForEach(v => vendorList.Add(v.ToVendor()));
            return Ok(vendorList);
        }


        [Authorize]
        // POST api/vendors
        [HttpPost]
        public async Task<ActionResult<Vendor>> PostAsync([FromBody] Vendor newVendor)
        {
            var vendorDM = new VendorDM(newVendor);
            VendorUser vendorUser = new VendorUser { Id = User.GetOid(), Role = "Owner", Name = User.GetUserName() };
            vendorDM.Users.Add(vendorUser);   
            var createdVendor = await _vendorsRepo.AddItemAsync(vendorDM);
            return Ok(createdVendor.ToVendor());
        }

    }
}
