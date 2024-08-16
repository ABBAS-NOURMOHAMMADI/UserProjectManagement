using Domain.Interfaces;
using MediatR;

namespace Application.Queries.Login
{
    public class LoginQuery : IRequest<LoginQueryResult>, IQuery
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }

    public record LoginQueryResult(string Token);
}
