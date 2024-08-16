namespace Infrastructure.Exceptions
{
    public class NotFoundException : Exception
    {
        public NotFoundException(string name, params object[] keys)
            : base(keys.Length > 0 ? $"آیتم \"{name}\" ({string.Join(',', keys)}) وجود ندارد" : $"آیتم {name} وجود ندارد")
        {
        }
    }
}
