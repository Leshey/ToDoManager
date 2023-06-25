using ToDoTaskManager.Domain.Core;

namespace ToDoTaskManager.Infrastructure;

public class UnitOfWork : IUnitOfWork
{
    private readonly ToDoTaskManagerContext _context;

    public UnitOfWork(ToDoTaskManagerContext context) 
    {
        _context = context;
    }
    
    public async Task SaveChangesAsync(CancellationToken cancellationToken) 
    {
        await _context.SaveChangesAsync(cancellationToken);
    }
}
