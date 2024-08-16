using Application.Commands.Project.CreateProject;
using Domain.Interfaces;
using Moq;

namespace TestUserProjectManagment.HandlerTest.ProjectHandlerTests
{
    public class CreateProjectCommandHandler
    {
        private readonly Mock<IApplicationDbContext> _context;
        private readonly Mock<ICurrentUserService> _currentUser;
        private readonly CreateProjectHandler _handler;

        public CreateProjectCommandHandler()
        {
            _context = new Mock<IApplicationDbContext>();
            _currentUser = new Mock<ICurrentUserService>();
            _handler = new CreateProjectHandler(_context.Object, _currentUser.Object);
        }

        [Fact]
        public async void CreateProjectSecced()
        {
            var command = new CreateProjectCommand { Name = "testHandlerCreateProject" };
            var result = await _handler.Handle(command, CancellationToken.None);

            Assert.NotNull(result);
            Assert.Equal(command.Name, result.Name);
            Assert.True(result.Id > 0);
        }

        [Fact]
        public async void CreateProjectFailed()
        {
            var command = new CreateProjectCommand { Name = null };
            var exception = await Assert.ThrowsAsync<Exception>(() => _handler.Handle(command, CancellationToken.None));

            Assert.Equal("name is empity", exception.Message);
        }
    }
}