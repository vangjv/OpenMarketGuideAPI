using OMG.Domain.Market.Models;
using OMG.Domain.Market.ValueObjects;
using System.Security.Claims;

namespace OMG.API.Extensions
{
    public static class MarketExtensions {

        public static void AddMarketOwnerFromClaimsPrincipal(this Market market, ClaimsPrincipal User)
        {
            if (User != null)
            {
                MarketUser newMarketUser = new MarketUser();
                var objectId = User.FindFirst("http://schemas.microsoft.com/identity/claims/objectidentifier")?.Value;
                var userName = User.FindFirst("name")?.Value;
                newMarketUser.Id = objectId;
                newMarketUser.Name = userName;
                newMarketUser.Role = "Owner";
                if (market.MarketUsers == null)
                {
                    market.MarketUsers = new List<MarketUser>
                    {
                        newMarketUser
                    };
                }
                else
                {
                    market.MarketUsers.Add(newMarketUser);
                }
            }
        }

        public static bool UserIsMarketOwner(this Market market, ClaimsPrincipal user)
        {
            bool userIsOwner = false;
            market.MarketUsers.ForEach(usr =>
            {
                if (usr.Id == user.GetOid() && usr.Role == "Owner")
                {
                    userIsOwner = true;
                }
            });
            return userIsOwner;
        }

    }
}
