using OMG.Domain.Vendor.Models;
using System.Security.Claims;

namespace OMG.API.Extensions
{
    public static class VendorExtensions
    {
        public static bool UserIsVendorOwner(this Vendor vendor, ClaimsPrincipal user)
        {
            bool userIsOwner = false;
            vendor.Users.ForEach(usr =>
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
