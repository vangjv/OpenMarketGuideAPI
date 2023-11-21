using OMG.Domain.Vendor.Models;
using OMG.Infrastructure.Interfaces.Persistence;
namespace OMG.Infrastructure.CosmosDbData.DatabaseModel
{
    public class VendorDM : Vendor, IMetadata
    {
        public string Type { get; } = "Vendor";
        public VendorDM() { }
        public VendorDM(Vendor vendor)
        {
            Name = vendor.Name;
            PrimaryContactName = vendor.PrimaryContactName;
            PrimaryContactTitle = vendor.PrimaryContactTitle;
            Categories = vendor.Categories;
            ContactInfo = vendor.ContactInfo;
            Products = vendor.Products;
            BillboardImageUrl = vendor.BillboardImageUrl;
            BillboardScale = vendor.BillboardScale;
        }

        public Vendor ToVendor()
        {
            var vendor = new Vendor();
            vendor.Id = Id;
            vendor.Name = Name;
            vendor.PrimaryContactName = PrimaryContactName;
            vendor.PrimaryContactTitle = PrimaryContactTitle;
            vendor.Categories = Categories;
            vendor.ContactInfo = ContactInfo;
            vendor.Products = Products;
            vendor.BillboardImageUrl = BillboardImageUrl;
            vendor.BillboardScale = BillboardScale;
            return vendor;
        }
    }
}
