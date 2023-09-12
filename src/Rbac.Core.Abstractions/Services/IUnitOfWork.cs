namespace AHutak.Rbac.Core.Abstractions.Services;

public interface IUnitOfWork
{
    Task CompleteAsync(CancellationToken cancellationToken);
}
