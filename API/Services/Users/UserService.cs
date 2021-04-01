using API.DTOs.Users;
using Domain.Entities;
using Domain.Interfaces;
using System.Threading.Tasks;

namespace API.Services.Users
{
    public class UserService : BaseService
    {
        public UserService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public async Task<AddUserResponse> AddNewAsync(AddUserRequest model)
        {
            // You can you some mapping tools as such as AutoMapper
            var user = new User(model.UserName
                , model.FirstName
                , model.LastName
                , model.Address
                , model.BirthDate
                , model.DepartmentId.Value);

            var repository = UnitOfWork.AsyncRepository<User>();
            await repository.AddAsync(user);
            await UnitOfWork.SaveChangesAsync();

            var response = new AddUserResponse()
            {
                Id = user.Id,
                UserName = user.UserName
            };

            return response;
        }
    }
}