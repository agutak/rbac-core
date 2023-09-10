namespace AHutak.Rbac.Core.Abstractions.Services;

internal interface IUnitOfWork
{
    Task CompleteAsync(CancellationToken cancellationToken);
}
