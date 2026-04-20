namespace ForenserBackend.Domain
{
    public interface IUnitOfWork
    {
        public Task Commit();
    }
}
