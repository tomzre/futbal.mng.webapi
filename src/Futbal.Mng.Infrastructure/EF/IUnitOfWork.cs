using System.Threading.Tasks;

namespace Futbal.Mng.Infrastructure.EF
{
    public interface IUnitOfWork
    {
         Task SaveChangesAsync();
    }
}