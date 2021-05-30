using Domain.Interfaces;

namespace Domain.Users
{
    public interface IUserRepository : IAsyncRepository<User>
    {
    }
}