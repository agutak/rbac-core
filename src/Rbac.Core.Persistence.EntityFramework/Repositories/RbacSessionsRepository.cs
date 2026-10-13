using AHutak.Rbac.Core.Abstractions.Entities;
using AHutak.Rbac.Core.Abstractions.Services;
using Microsoft.EntityFrameworkCore;

namespace AHutak.Rbac.Core.Persistence.EntityFramework.Repositories;

public class RbacSessionsRepository : RbacSessionsRepository<RbacSession>
{
    public RbacSessionsRepository(RbacDbContext dbContext) : base(dbContext)
    {
    }
}

public class RbacSessionsRepository<TSession> : IRbacSessionsRepository<TSession>
    where TSession : RbacSession
{
    private readonly RbacDbContext _dbContext;
    private readonly DbSet<TSession> _set;
    private readonly IQueryable<TSession> _readOnlySet;

    public RbacSessionsRepository(RbacDbContext dbContext)
    {
        _dbContext = dbContext;
        _set = dbContext.Set<TSession>();
        _readOnlySet = dbContext.Set<TSession>().AsNoTracking();
    }

    public virtual async Task AddAsync(TSession rbacSession, CancellationToken cancellationToken)
    {
        _set.Add(rbacSession);

        await _dbContext
            .SaveChangesAsync(cancellationToken)
            .ConfigureAwait(false);
    }

    public virtual async Task UpdateAsync(TSession rbacSession, CancellationToken cancellationToken)
    {
        _set.Update(rbacSession);

        await _dbContext
            .SaveChangesAsync(cancellationToken)
            .ConfigureAwait(false);
    }

    public virtual async Task DeleteAsync(TSession rbacSession, CancellationToken cancellationToken)
    {
        _set.Remove(rbacSession);

        await _dbContext
            .SaveChangesAsync(cancellationToken)
            .ConfigureAwait(false);
    }

    public virtual async Task<TSession?> GetAggregateAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _set
            .Include(x => x.RbacSessionRoles)
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken)
            .ConfigureAwait(false);
    }
}
