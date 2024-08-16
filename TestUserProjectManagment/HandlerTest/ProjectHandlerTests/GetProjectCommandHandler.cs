using Application.Queries.Project.GetListProject;
using Domain.Interfaces;
using Moq;

namespace TestUserProjectManagment.HandlerTest.ProjectHandlerTests
{
    public class GetProjectCommandHandler
    {
        private readonly Mock<IApplicationDbContext> _context;
        private readonly GetListProjectHandler _handler;

        public GetProjectCommandHandler()
        {
            _context = new Mock<IApplicationDbContext>();
            _handler = new GetListProjectHandler(_context.Object);
        }

        [Fact]
        public async void GetListProjectSecced()
        {
            var command = new GetListProjectQuery();
            var result = await _handler.Handle(command, CancellationToken.None);

            Assert.NotNull(result);
        }
    }
}
