using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoTaskManager.Domain.Core;
using ToDoTaskManager.Domain.ToDos;
namespace ToDoTaskManager.Application;

public class ToDoService
{
    private readonly IToDoRepository _toDoRepository;
    private readonly IUnitOfWork _unitOfWork;

    public ToDoService(IToDoRepository toDoRepository, IUnitOfWork unitOfWork)
    {
        _toDoRepository = toDoRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Guid> CreateNewToDoAsync(string name, CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrEmpty(name))
        {
            throw new InvalidOperationException();
        }

        var toDo = ToDo.Create(name);

        await _toDoRepository.AddAsync(toDo, cancellationToken);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return toDo.Id;
    }

    public async Task<ToDo> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var toDo = await _toDoRepository.GetByIdAsync(id, cancellationToken);

        return toDo;
    }

    public async Task<IEnumerable<ToDo>> GetToDos(int count, int page, CancellationToken cancellationToken = default) 
    {
        var toDo = await _toDoRepository.GetToDos(count, page, cancellationToken);

        return toDo;
    }

    public async Task DeleteById(Guid id, CancellationToken cancellationToken = default) 
    {
        var toDo = await _toDoRepository.GetByIdAsync(id, cancellationToken);

        if (toDo == null) 
        {
            throw new ToDoNotFoundExeption(id);
        }
        
        await _toDoRepository.DeleteAsync(toDo, cancellationToken);

        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }

    public async Task CloseToDoById(Guid id, CancellationToken cancellationToken = default) 
    {
        var toDo = await _toDoRepository.GetByIdAsync(id, cancellationToken);

        if (toDo == null) 
        {
            throw new ToDoNotFoundExeption(id);
        }

        toDo.Close();

        await _toDoRepository.UpdateAsync(toDo, cancellationToken);

        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }
}
