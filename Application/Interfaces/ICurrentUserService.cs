using Application.Models;
using System.Security.Principal;

namespace Domain.Interfaces
{
    public interface ICurrentUserService
    {
        string UserId { get; }
        string? UserUuid { get; }
        bool HasRole(string role);
        bool HasAnyPermission(params string[] permission);
        IPrincipal? User { get; }
        void SetScopedUserByToken(string userUuid, string requestToken);
        void SetScopedUser(ApplicationUser applicationUser);
        string[] GetClaimValues(string claim = "permission");
        bool HasAnyClaim(params string[] claims);
    }
}
