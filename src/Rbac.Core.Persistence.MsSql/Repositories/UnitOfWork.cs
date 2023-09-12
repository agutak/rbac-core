using AHutak.Rbac.Core.Abstractions.Services;
using AHutak.Rbac.Core.Persistence.MsSql.Context;

namespace AHutak.Rbac.Core.Persistence.MsSql.Repositories;

internal class UnitOfWork : IUnitOfWork
{
    private readonly RbacDbContext _dbContext;

    public UnitOfWork(RbacDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task CompleteAsync(CancellationToken cancellation)
    {
        await _dbContext.SaveChangesAsync(cancellation);
    }
}
