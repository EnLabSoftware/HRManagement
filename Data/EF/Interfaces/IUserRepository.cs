using Business.Users;
using System.Linq.Expressions;

namespace Data.EF.Interfaces
{
    public interface IUserRepository : IAsyncRepository<User>
    {
        Task<List<User>> ListAsyncwithDept(Expression<Func<User, bool>> expression);
    }
}