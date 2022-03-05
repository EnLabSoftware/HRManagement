using Business.Interfaces;

namespace Business.Users
{
    public interface IUserRepository : IAsyncRepository<User>
    {
    }
}