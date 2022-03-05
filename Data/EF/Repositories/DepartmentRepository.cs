using Business.Departments;

namespace Data.EF.Repositories
{
    public class DepartmentRepository : RepositoryBase<Department>
        , IDepartmentRepository
    {
        public DepartmentRepository(EFContext dbContext) : base(dbContext)
        {
        }
    }
}