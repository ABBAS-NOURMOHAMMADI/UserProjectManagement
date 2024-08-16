using Application.Commands.Task.CreateTask;
using Domain.Interfaces;
using Moq;

namespace TestUserProjectManagment.HandlerTest.TaskHandlerTests
{
    public class CreateTaskCommandHandler
    {
        private int _projectId = 1;
        private readonly Mock<IApplicationDbContext> _context;
        private readonly CreateTaskHandler _handler;

        public CreateTaskCommandHandler()
        {
            _context = new Mock<IApplicationDbContext>();
            _handler = new CreateTaskHandler(_context.Object);
        }

        [Fact]
        public async void CreateTaskSecced()
        {
            var command = new CreateTaskCommand
            {
                Name = "testHandlerCreateTask",
                ProjectId = _projectId,
                Status = Domain.Enums.Status.Todo
            };

            var result = await _handler.Handle(command, CancellationToken.None);

            Assert.NotNull(result);
            Assert.Equal(command.Name, result.Name);
            Assert.True(result.Id > 0);
        }

        [Fact]
        public async void CreateTaskFailed()
        {
            var command = new CreateTaskCommand { ProjectId = 0, };
            var exception = await Assert.ThrowsAsync<Exception>(() => _handler.Handle(command, CancellationToken.None));

            Assert.Equal("project not found", exception.Message);
        }
    }
}
