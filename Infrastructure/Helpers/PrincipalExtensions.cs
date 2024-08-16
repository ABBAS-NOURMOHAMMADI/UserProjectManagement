using IdentityModel;
using System.Security.Claims;
using System.Security.Principal;

namespace Infrastructure.Helpers
{
    public static class PrincipalExtensions
    {
        public static string GetUserId(this IPrincipal principal) => GetUserSub(principal);

        public static string? GetUserUuid(this IPrincipal principal)
        {
            return GetUserSub(principal);
        }

        public static string? GetUserRole(this IPrincipal principal)
        {
            var id = (principal as ClaimsPrincipal)?.FindFirst(JwtClaimTypes.Role);
            return id?.Value;
        }

        public static bool HasUserRole(this IPrincipal principal, string role)
        {
            var id = (principal as ClaimsPrincipal)?.FindFirst(c => c.Type == JwtClaimTypes.Role && c.Value == role);
            return id != null;
        }

        /// <summary>
        /// Return user subject claim or NameIdentifier (which is openid uuid)
        /// </summary>
        /// <param name="principal"></param>
        /// <returns></returns>
        public static string? GetUserSub(this IPrincipal principal)
        {
            var id = (principal as ClaimsPrincipal)?.FindFirst(JwtClaimTypes.Subject);
            return id?.Value;
        }
    }
}
