using Business.Base;
using System.Threading.Tasks;

namespace Business.Interfaces
{
    public interface IUnitOfWork
    {
        Task<int> SaveChangesAsync();

        IAsyncRepository<T> AsyncRepository<T>() where T : RootEntity;
    }
}