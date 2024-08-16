using Application.Commands.Task.UpdateTask;
using Domain.Interfaces;
using Moq;

namespace TestUserProjectManagment.HandlerTest.TaskHandlerTests
{
    public class UpdateTaskCommandHandler
    {
        private int _projectId = 1;
        private int _taskId = 1;
        private readonly Mock<IApplicationDbContext> _context;
        private readonly Mock<ICurrentUserService> _currentUser;
        private readonly UpdateTaskHandler _handler;

        public UpdateTaskCommandHandler()
        {
            _context = new Mock<IApplicationDbContext>();
            _currentUser = new Mock<ICurrentUserService>();
            _handler = new UpdateTaskHandler(_context.Object, _currentUser.Object);
        }

        [Fact]
        public async void UpdateTaskSecced()
        {
            var command = new UpdateTaskCommand
            {
                TaskId = _taskId,
                Name = "testHandlerUpdateTask",
                ProjectId = _projectId,
                Status = Domain.Enums.Status.Inprogress
            };

            var result = await _handler.Handle(command, CancellationToken.None);

            Assert.NotNull(result);
            Assert.Equal(command.Name, result.Name);
            Assert.Equal(command.Status, result.Status);
        }

        [Fact]
        public async void UpdateTaskFailed()
        {
            var command = new UpdateTaskCommand { ProjectId = 0 };
            var exception = await Assert.ThrowsAsync<Exception>(() => _handler.Handle(command, CancellationToken.None));

            Assert.Equal("project not found", exception.Message);
        }
    }
}
