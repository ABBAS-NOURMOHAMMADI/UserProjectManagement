using Application.Queries.Task.GetListTask;
using Domain.Interfaces;
using Moq;

namespace TestUserProjectManagment.HandlerTest.TaskHandlerTests
{
    public class GetTaskCommandHandler
    {
        private int _projectId = 1;
        private readonly Mock<IApplicationDbContext> _context;
        private readonly GetListTaskHandler _handler;

        public GetTaskCommandHandler()
        {
            _context = new Mock<IApplicationDbContext>();
            _handler = new GetListTaskHandler(_context.Object);
        }

        [Fact]
        public async void GetListTaskSecced()
        {
            var command = new GetListTaskQuery { ProjectId = _projectId };
            var result = await _handler.Handle(command, CancellationToken.None);

            Assert.NotNull(result);
        }

        [Fact]
        public async void GetListTaskFailed()
        {
            var command = new GetListTaskQuery { ProjectId = 0 };
            var exception = await Assert.ThrowsAsync<Exception>(() => _handler.Handle(command, CancellationToken.None));

            Assert.Equal("project not found", exception.Message);
        }
    }
}
