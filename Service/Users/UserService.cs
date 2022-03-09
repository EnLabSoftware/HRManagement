using Common.DTOs.Users;
using Data.EF.Interfaces;
using Business.Users;
using Business.Departments;
using AutoMapper;

namespace Service.Users
{
    public class UserService : BaseService
    {
        private readonly IMapper _mapper;
        public UserService(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork)
        {
            _mapper = mapper;
        }

        public async Task<AddUserResponse> AddNewAsync(AddUserRequest model)
        {
            var deptRepos = UnitOfWork.AsyncRepository<Department>();
            var dept = await deptRepos.GetAsync(_ => _.Id == model.DepartmentId);
            var user = _mapper.Map<User>(model);

            if (dept != null) {
                user.AddDepartment(dept);
            }

            var repository = UnitOfWork.AsyncRepository<User>();
            await repository.AddAsync(user);
            await UnitOfWork.SaveChangesAsync();

            var response = _mapper.Map<AddUserResponse>(user);

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
//          var repository = UnitOfWork.AsyncRepository<User>();
            var repository = UnitOfWork.UserRepository();
            var users = await repository
                .ListAsyncwithDept(_ => _.UserName.Contains(request.Search));
//              .ListAsync(_ => _.UserName.Contains(request.Search));
            
            var userDTOs = users.Select(_user => _mapper.Map<UserInfoDTO>(_user)).ToList();
            return userDTOs;
        }
    }
}