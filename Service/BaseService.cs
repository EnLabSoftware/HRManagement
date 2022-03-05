using Business.Interfaces;

namespace Service
{
    public class BaseService
    {
        public BaseService(IUnitOfWork unitOfWork)
        {
            UnitOfWork = unitOfWork;
        }

        protected internal IUnitOfWork UnitOfWork { get; set; }
    }
}