using System.Threading.Tasks;

namespace Futbal.Mng.Infrastructure.EF {
    public class UnitOfWork : IUnitOfWork
    {
        private readonly FutbalMngContext _dbContext;
        public UnitOfWork (FutbalMngContext dbContext) {
            _dbContext = dbContext;
        }

        public async Task SaveChangesAsync()
        {
            await _dbContext.SaveChangesAsync();
        }
    }
}