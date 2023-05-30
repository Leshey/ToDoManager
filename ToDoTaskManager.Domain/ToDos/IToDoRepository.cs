using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDoTaskManager.Domain.ToDos;

public interface IToDoRepository
{
    Task AddAsync(ToDo toDo, CancellationToken cancellationToken = default);

    Task<ToDo> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

    Task UpdateAsync(ToDo toDo, CancellationToken cancellationToken = default);

    Task DeleteAsync(ToDo toDo, CancellationToken cancellationToken = default);

    Task SaveChangesAsync(CancellationToken cancellationToken = default);
}

