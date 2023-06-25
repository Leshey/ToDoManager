using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoTaskManager.Domain.ToDos;

namespace ToDoTaskManager.Infrastructure.ToDos;

public class ToDoRepository : IToDoRepository
{
    private readonly ToDoTaskManagerContext _context;

    public ToDoRepository(ToDoTaskManagerContext context) 
    {
        _context = context;
    }

    public async Task AddAsync(ToDo toDo, CancellationToken cancellationToken = default) 
    {
        await _context.ToDos.AddAsync(toDo, cancellationToken);
    }

    public async Task<ToDo?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default) 
    {
        return await _context.ToDos.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<IEnumerable<ToDo>> GetToDos(int count, int page, CancellationToken cancellationToken = default) 
    {
        var toDo = await _context.ToDos.Skip(count * page).Take(count).ToListAsync();

        return toDo;
    }

    public Task UpdateAsync(ToDo toDo, CancellationToken cancellationToken = default) 
    {
        _context.ToDos.Update(toDo);

        return Task.CompletedTask;
    }

    public Task DeleteAsync(ToDo toDo, CancellationToken cancellationToken = default) 
    {
        _context.ToDos.Remove(toDo);

        return Task.CompletedTask;
    }
}
