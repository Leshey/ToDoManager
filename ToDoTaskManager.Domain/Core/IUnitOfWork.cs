namespace ToDoTaskManager.Domain.Core;

public interface IUnitOfWork
{
    Task SaveChangesAsync(CancellationToken cancellationToken);
}
