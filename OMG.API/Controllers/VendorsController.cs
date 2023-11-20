using Microsoft.AspNetCore.Mvc;
using OMG.Infrastructure.CosmosDbData.Interfaces;
using OMG.API.Extensions;
using OMG.Domain.Vendor.Models;
using Microsoft.AspNetCore.Authorization;
using OMG.Infrastructure.CosmosDbData.DatabaseModel;
using OMG.API.Models;
using OMG.Infrastructure.Services;
using Newtonsoft.Json;

namespace OMG.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VendorsController : ControllerBase
    {
        IVendorRepository _vendorsRepo;
        ProductImageStorageService _productImageStorageService;
        public VendorsController(IVendorRepository vendorsRep, ProductImageStorageService productImageStorageService)
        {
            _vendorsRepo = vendorsRep;
            _productImageStorageService = productImageStorageService;
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

        [Authorize]
        // POST api/vendors/{vendorId}/products
        [HttpPost("{vendorId}/products")]
        public async Task<ActionResult<Vendor>> AddProduct(string vendorId, [FromForm] NewProductRequest newProductRequest)
        {
            //check if vendor exists
            var vendor = await _vendorsRepo.GetItemAsync(vendorId);
            //check if user is owner of vendor
            if (!vendor.UserIsVendorOwner(User))
            {
                return Unauthorized();
            }
            //parse productJson
            var newProduct = JsonConvert.DeserializeObject<Product>(newProductRequest.ProductJson);
            if (newProduct.Id == null)
            {
                newProduct.Id = Guid.NewGuid().ToString();
            }
            //check for file
            if (newProductRequest.File != null || newProductRequest.File.Length > 0)
            {
                string imageFileName = Path.GetFileName(newProductRequest.File.FileName);
                using (var stream = newProductRequest.File.OpenReadStream())
                {
                    var imageId = Guid.NewGuid().ToString();
                    await _productImageStorageService.UploadBlobAsync(vendorId, newProduct.Id, imageId, imageFileName, stream);
                    var newImage = new ProductImage { Id = imageId, FileName = imageFileName, Url = _productImageStorageService.GetBlobUrl(vendorId, newProduct.Id, imageId, imageFileName) };
                    newProduct.Images.Add(newImage);
                }
            }
             //add product to vendor
            if (vendor.Products == null)
            {
                vendor.Products = new List<Product>();
            }
            vendor.Products.Add(newProduct);
            var updatedVendor = await _vendorsRepo.UpdateItemAsync(vendorId, vendor);
            return Ok(updatedVendor.ToVendor());
        }

    }
}
