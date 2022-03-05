using Business.Base;
using Business.Interfaces;
using Data.EF.Repositories;
using System;
using System.Threading.Tasks;

namespace Data.EF;

public class UnitOfWork : IUnitOfWork
{
    private readonly EFContext _dbContext;

    public UnitOfWork(EFContext dbContext)
    {
        _dbContext = dbContext;
    }

    public IAsyncRepository<T> AsyncRepository<T>() where T : RootEntity
    {
        return new RepositoryBase<T>(_dbContext);
    }

    public Task<int> SaveChangesAsync()
    {
        return _dbContext.SaveChangesAsync();
    }
}
