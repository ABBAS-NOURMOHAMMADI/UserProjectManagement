using Application.Commands.Project.DeleteProject;
using Domain.Interfaces;
using Moq;

namespace TestUserProjectManagment.HandlerTest.ProjectHandlerTests
{
    public class DeleteProjectCommandHandler
    {
        private int _projectId = 1;
        private readonly Mock<IApplicationDbContext> _context;
        private readonly Mock<ICurrentUserService> _currentUser;
        private readonly DeleteProjectHandler _handler;

        public DeleteProjectCommandHandler()
        {
            _context = new Mock<IApplicationDbContext>();
            _currentUser = new Mock<ICurrentUserService>();
            _handler = new DeleteProjectHandler(_context.Object, _currentUser.Object);
        }

        [Fact]
        public async void DeleteProjectSecced()
        {
            var command = new DeleteProjectCommand { ProjectId = _projectId };
            var result = await _handler.Handle(command, CancellationToken.None);

            Assert.True(result);
        }

        [Fact]
        public async void DeleteProjectFailed()
        {
            var command = new DeleteProjectCommand { ProjectId = 0 };
            var exception = await Assert.ThrowsAsync<Exception>(() => _handler.Handle(command, CancellationToken.None));

            Assert.Equal("project not found", exception.Message);
        }
    }
}
