using Data.EF.Interfaces;

namespace Business.Users
{
    public interface IUserRepository : IAsyncRepository<User>
    {
    }
}