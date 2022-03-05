using Business.Users;

namespace Data.EF.Repositories
{
    public class UserRepository : RepositoryBase<User>
        , IUserRepository
    {
        public UserRepository(EFContext dbContext) : base(dbContext)
        {
        }
    }
}