using Application.Queries.Login;
using Domain.Interfaces;
using Moq;

namespace TestUserProjectManagment.HandlerTest.LoginHandlerTests
{
    public class LoginCommandHandler
    {
        private readonly Mock<IApplicationDbContext> _context;
        private readonly LoginHandler _handler;

        public LoginCommandHandler()
        {
            _context = new Mock<IApplicationDbContext>();
            _handler = new LoginHandler(_context.Object);
        }

        [Fact]
        public async void LoginSecced()
        {
            var command = new LoginQuery { UserName = "test", Password = "test" };
            var result = await _handler.Handle(command, CancellationToken.None);

            Assert.NotNull(result);
            Assert.True(result.Token.Any());
        }

        [Fact]
        public async void LoginFailed()
        {
            var command = new LoginQuery { UserName = "123", Password = "123" };
            var exception = await Assert.ThrowsAsync<Exception>(() => _handler.Handle(command, CancellationToken.None));

            Assert.Equal("user not foundy", exception.Message);
        }
    }
}
