using Application.Services;
using Domain.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Queries.Login
{
    public class LoginHandler : IRequestHandler<LoginQuery, LoginQueryResult>
    {
        private readonly IApplicationDbContext context;

        public LoginHandler(IApplicationDbContext context)
        {
            this.context = context;
        }

        public async Task<LoginQueryResult> Handle(LoginQuery request, CancellationToken cancellationToken)
        {
            var user = await context.Users
                       .Where(u => u.DeletedAt == null &&
                        u.NormalizedUserName == request.UserName.ToUpper() && u.Password == request.Password)
                       .SingleOrDefaultAsync();

            if (user is null)
                throw new Exception("user not found");

            return new(GeneratJwtTokenService.GenerateJwtToken(user.UserId));
        }
    }
}
