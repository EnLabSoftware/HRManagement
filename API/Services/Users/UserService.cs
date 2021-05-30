using API.DTOs.Users;
using Domain.Interfaces;
using Domain.Users;
using System;
using System.Collections.Generic;
using System.Linq;
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

        public async Task<AddPayslipResponse> AddUserPayslipAsync(AddPayslipRequest model)
        {
            var repository = UnitOfWork.AsyncRepository<User>();
            var user = await repository.GetAsync(_ => _.Id == model.UserId);
            if (user != null)
            {
                var payslip = user.AddPayslip(model.Date.Value
                    , model.WorkingDays.Value
                    , model.Bonus
                    , model.IsPaid.Value);

                await repository.UpdateAsync(user);
                await UnitOfWork.SaveChangesAsync();

                return new AddPayslipResponse()
                {
                    UserId = user.Id,
                    TotalSalary = payslip.TotalSalary
                };
            }

            throw new Exception("User not found.");
        }

        public async Task<List<UserInfoDTO>> SearchAsync(GetUserRequest request)
        {
            var repository = UnitOfWork.AsyncRepository<User>();
            var users = await repository
                .ListAsync(_ => _.UserName.Contains(request.Search));

            var userDTOs = users.Select(_ => new UserInfoDTO()
            {
                Address = _.Address,
                BirthDate = _.BirthDate,
                DepartmentId = _.DepartmentId,
                FirstName = _.FirstName,
                Id = _.Id,
                LastName = _.LastName,
                UserName = _.UserName
            })
            .ToList();

            return userDTOs;
        }
    }
}