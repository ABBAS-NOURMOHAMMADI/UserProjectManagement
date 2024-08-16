namespace Domain.Interfaces
{
    public interface ICommand { }

    public interface ICreateCommand : ICommand { }
    public interface IUpdateCommand : ICommand { }
    public interface IDeleteCommand : ICommand { }
}
