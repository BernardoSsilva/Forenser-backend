using ForenserBackend.Domain;

namespace ForenserBackend.Infrastructure
{
    public class UnitOfWork : IUnitOfWork
    {

        private readonly ForenserDbContext _dbContext;
        public UnitOfWork(ForenserDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task Commit()
        {
            await _dbContext.SaveChangesAsync();
        }
    }
}
