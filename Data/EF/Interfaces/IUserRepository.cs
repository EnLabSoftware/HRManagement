using Business.Users;
using Business.Users;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Data.EF.Interfaces;

namespace Data.EF.Interfaces
{
    public interface IUserRepository : IAsyncRepository<User>
    {
        Task<List<User>> ListAsyncwithDept(Expression<Func<User, bool>> expression);
    }
}