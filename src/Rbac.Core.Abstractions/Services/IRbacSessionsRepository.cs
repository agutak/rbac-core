using AHutak.Rbac.Core.Abstractions.Entities;

namespace AHutak.Rbac.Core.Abstractions.Services;

public interface IRbacSessionsRepository<TSession> where TSession : RbacSession
{
    Task AddAsync(TSession rbacSession, CancellationToken cancellationToken);

    Task UpdateAsync(TSession rbacSession, CancellationToken cancellationToken);

    Task DeleteAsync(TSession rbacSession, CancellationToken cancellationToken);

    Task<TSession?> GetAggregateAsync(Guid id, CancellationToken cancellationToken);
}
