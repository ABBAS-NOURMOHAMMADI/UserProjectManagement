using Application.Commands.Project.UpdateProject;
using Domain.Interfaces;
using Moq;

namespace TestUserProjectManagment.HandlerTest.ProjectHandlerTests
{
    public class UpdateProjectCommandHandler
    {
        private int _projectId = 1;
        private readonly Mock<IApplicationDbContext> _context;
        private readonly Mock<ICurrentUserService> _currentUser;
        private readonly UpdateProjectHandler _handler;

        public UpdateProjectCommandHandler()
        {
            _context = new Mock<IApplicationDbContext>();
            _currentUser = new Mock<ICurrentUserService>();
            _handler = new UpdateProjectHandler(_context.Object, _currentUser.Object);
        }

        [Fact]
        public async void UpdateProjectSecced()
        {
            var command = new UpdateProjectCommand { Name = "testHandlerUpdtaeProject", ProjectId = _projectId };
            var result = await _handler.Handle(command, CancellationToken.None);

            Assert.NotNull(result);
            Assert.Equal(command.Name, result.Name);
        }

        [Fact]
        public async void UpdateProjectFailed()
        {
            var command = new UpdateProjectCommand { Name = null, ProjectId = 0 };
            var exception = await Assert.ThrowsAsync<Exception>(() => _handler.Handle(command, CancellationToken.None));

            Assert.Equal("project not found", exception.Message);
        }
    }
}
