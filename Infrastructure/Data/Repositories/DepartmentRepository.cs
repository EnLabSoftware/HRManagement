using Domain.Departments;

namespace Infrastructure.Data.Repositories
{
    public class DepartmentRepository : RepositoryBase<Department>
        , IDepartmentRepository
    {
        public DepartmentRepository(EFContext dbContext) : base(dbContext)
        {
        }
    }
}