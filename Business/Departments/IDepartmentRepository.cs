using Business.Interfaces;

namespace Business.Departments
{
    public interface IDepartmentRepository : IAsyncRepository<Department>
    {
    }
}