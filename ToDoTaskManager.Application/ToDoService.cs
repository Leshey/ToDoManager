using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoTaskManager.Domain.ToDos;
namespace ToDoTaskManager.Application;

public class ToDoService
{
    private readonly IToDoRepository _toDoRepository;

    public ToDoService(IToDoRepository toDoRepository)
    {
        _toDoRepository = toDoRepository;
    }

    public async Task<Guid> CreateNewToDoAsync(string name, CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrEmpty(name))
        {
            throw new InvalidOperationException();
        }

        var toDo = ToDo.Create(name);

        await _toDoRepository.AddAsync(toDo, cancellationToken);

        await _toDoRepository.SaveChangesAsync(cancellationToken);

        return toDo.Id;
    }

    public async Task<ToDo> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var toDo = await _toDoRepository.GetByIdAsync(id, cancellationToken);

        await _toDoRepository.SaveChangesAsync(cancellationToken);

        return toDo;
    }

    public async Task DeleteById(Guid id, CancellationToken cancellationToken = default) 
    {
        var toDo = await _toDoRepository.GetByIdAsync(id, cancellationToken);
        
        await _toDoRepository.DeleteAsync(toDo, cancellationToken);

        await _toDoRepository.SaveChangesAsync(cancellationToken);
    }
}
