using Business.Departments;

namespace Data.EF.Interfaces
{
    public interface IDepartmentRepository : IAsyncRepository<Department>
    {
    }
}