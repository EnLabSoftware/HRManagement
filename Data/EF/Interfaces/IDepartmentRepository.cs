using Data.EF.Interfaces;

namespace Business.Departments
{
    public interface IDepartmentRepository : IAsyncRepository<Department>
    {
    }
}