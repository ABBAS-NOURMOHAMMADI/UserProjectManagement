using Application.Models;
using Domain.Interfaces;
using IdentityModel;
using Infrastructure.Helpers;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Principal;

namespace UserProjectManagement.Services
{
    public class CurrentUserService : ICurrentUserService
    {
        private readonly IConfiguration configuration;
        private readonly IHttpContextAccessor _httpContextAccessor;
        string _requestToken;
        IPrincipal scopePrincipal;

        public CurrentUserService(IHttpContextAccessor httpContextAccessor,
                                  IConfiguration configuration)
        {
            this.configuration = configuration;
            _httpContextAccessor = httpContextAccessor;
        }

        public IPrincipal? User => _httpContextAccessor.HttpContext?.User ?? scopePrincipal;
        string ICurrentUserService.UserId => User?.GetUserId();
        public string? UserUuid => User?.GetUserUuid();
        public string? Role => User?.GetUserRole();
        public string RequestToken => _httpContextAccessor.HttpContext?.Request.Headers["Authorization"] ?? _requestToken;

        public object PermissionConstants { get; private set; }

        public bool HasRole(string role)
        {
            return User?.HasUserRole(role) ?? false;
        }

        public bool HasAnyPermission(params string[] permissions)
        {
            return _httpContextAccessor.HttpContext?.User.HasAnyPermission(permissions) ?? false;
        }

        public void SetScopedUser(ApplicationUser applicationUser)
        {
            if (applicationUser == null)
                throw new Exception("ApplicationUser cannot be null");

            var idClaim = new Claim(JwtClaimTypes.Subject, applicationUser.Id);
            var nameClaim = new Claim(JwtClaimTypes.GivenName, applicationUser.FirstName ?? "");
            var familyClaim = new Claim(JwtClaimTypes.FamilyName, applicationUser.LastName ?? "");
            var usernameClaim = new Claim(JwtClaimTypes.Name, applicationUser.UserName ?? "");

            var identity = new ClaimsIdentity(new[] { idClaim, nameClaim, familyClaim, usernameClaim });

            var principal = new ClaimsPrincipal(identity);
            scopePrincipal = principal;
        }

        public void SetScopedUserByToken(string userUuid, string requestToken)
        {
            if (userUuid == null)
                return;

            if (requestToken != null)
            {
                var jwt = new JwtSecurityTokenHandler().ReadJwtToken(requestToken.RemoveFromStart("Bearer "));
                var identity = new ClaimsIdentity();
                foreach (var claim in jwt.Claims)
                    identity.AddClaim(claim);
                var principal = new ClaimsPrincipal(identity);
                _requestToken = requestToken;
                scopePrincipal = principal;
            }
            else
            {
                var idClaim = new Claim(JwtClaimTypes.Subject, userUuid);
                var identity = new ClaimsIdentity(new[] { idClaim });
                var principal = new ClaimsPrincipal(identity);
                scopePrincipal = principal;
            }
        }

        public string[] GetClaimValues(string claim = "permission")
        {
            return (User as ClaimsPrincipal).Claims.Where(c => c.Type == claim).Select(c => c.Value).ToArray();
        }

        public bool HasAnyClaim(params string[] claims)
        {
            foreach (var claim in claims)
                if ((User as ClaimsPrincipal).HasClaim(c => c.Value == claim))
                    return true;

            return false;
        }
    }
}
