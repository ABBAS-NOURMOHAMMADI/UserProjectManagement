using Application.Commands.Task.DeleteTask;
using Domain.Interfaces;
using Moq;

namespace TestUserProjectManagment.HandlerTest.TaskHandlerTests
{
    public class DeleteTaskCommandHandler
    {
        private int _projectId = 1;
        private int _taskId = 1;
        private readonly Mock<IApplicationDbContext> _context;
        private readonly Mock<ICurrentUserService> _currentUser;
        private readonly DeleteTaskHandler _handler;

        public DeleteTaskCommandHandler()
        {
            _context = new Mock<IApplicationDbContext>();
            _currentUser = new Mock<ICurrentUserService>();
            _handler = new DeleteTaskHandler(_context.Object, _currentUser.Object);
        }

        [Fact]
        public async void DeleteTaskSecced()
        {
            var command = new DeleteTaskCommand
            {
                TaskId = _taskId,
                ProjectId = _projectId,
            };

            var result = await _handler.Handle(command, CancellationToken.None);

            Assert.NotNull(result);
            Assert.True(result);
        }

        [Fact]
        public async void DeleteTaskFailed()
        {
            var command = new DeleteTaskCommand { ProjectId = 0, };
            var exception = await Assert.ThrowsAsync<Exception>(() => _handler.Handle(command, CancellationToken.None));

            Assert.Equal("project not found", exception.Message);
        }
    }
}
