using System.Security.Claims;
namespace OMG.API.Extensions
{
    public static class ClaimsPrincipalExtensions
    {
        public static string? GetUserName(this ClaimsPrincipal principal)
        {
            // The claim type for UserName can vary; it's often "name"
            return principal.FindFirst("name")?.Value;
        }

        public static string? GetUserFirstName(this ClaimsPrincipal principal)
        {
            // The claim type for UserId is often "sub" (subject) in Azure B2C
            return principal.FindFirst("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/givenname")?.Value;
        }

        public static string? GetUserLastName(this ClaimsPrincipal principal)
        {
            // The claim type for UserId is often "sub" (subject) in Azure B2C
            return principal.FindFirst("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/surname")?.Value;
        }

        public static string? GetOid(this ClaimsPrincipal principal)
        {
            // The "oid" claim is used to uniquely identify the user
            return principal.FindFirst("http://schemas.microsoft.com/identity/claims/objectidentifier")?.Value;
        }
    }
}