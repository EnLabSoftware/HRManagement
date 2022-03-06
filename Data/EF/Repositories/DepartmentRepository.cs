using Business.Departments;
using Data.EF.Interfaces;

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