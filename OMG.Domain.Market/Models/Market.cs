using OMG.Domain.Market.Enums;
using OMG.Domain.Market.ValueObjects;
using OMG.SharedKernel.Entities.Base;
using System.Security.Claims;
using OMG.Domain.Market.SchemaVersioning;

namespace OMG.Domain.Market.Models
{
    public class Market:BaseEntity
    {
        public MarketEntityType MarketEntityType { get; set; } = MarketEntityType.Template;
        public string Name { get; set; }
        public string Description { get; set; }
        public string State { get; set; }
        public CoordinateData Location { get; set; }
        public MarketLocation MarketLocation{ get; set; }
        public List<VendorLocation> VendorLocations { get; set; }
        public List<ThreeDModelEntity> ThreeDModelEntities { get; set; }
        public List<Vendor> Vendors { get; set; }
        public List<MarketUser> MarketUsers { get; set; }
        public Market(string name, string description, string state)
        {
            Name = name;
            Description = description;
            State = state;
        }
        public new int SchemaVersion = SchemaVersionConfig.MarketCurrentSchemaVersion;
        //public Market(string name, string description,string state, Location location, Boundary boundaries)
        //{
        //    Name = name;
        //    Description = description;
        //    State = state;
        //    Location = location;
        //    Boundaries = boundaries;
        //    VendorLocation = new List<VendorLocation>();
        //}

        public void AddMarketOwnerFromClaimsPrincipal (ClaimsPrincipal User)
        {
            if (User != null) {
                MarketUser newMarketUser = new MarketUser();
                var objectId = User.FindFirst("http://schemas.microsoft.com/identity/claims/objectidentifier")?.Value;
                var userName = User.FindFirst("name")?.Value;
                newMarketUser.Id = objectId;
                newMarketUser.Name = userName;
                newMarketUser.Role = "Owner";
                if (MarketUsers == null)
                {
                    MarketUsers = new List<MarketUser>
                    {
                        newMarketUser
                    };
                } else
                {
                    MarketUsers.Add(newMarketUser);
                }
            }
        }
    }
}
