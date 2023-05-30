using Microsoft.AspNetCore.Mvc;
using ToDoTaskManager.Application;
using ToDoTaskManager.Domain.ToDos;

namespace ToDoTaskManager.WebApi.Controllers;

[ApiController]
[Route("[controller]")]
public class ToDoController : ControllerBase
{
    private readonly ToDoService _toDoService;

    public ToDoController(ToDoService toDoService)
    {
        _toDoService = toDoService;
    }

    [HttpPost]
    public async Task<Guid> CreateNewToDo([FromBody] string name, CancellationToken cancellationToken)
    {
        return await _toDoService.CreateNewToDoAsync(name, cancellationToken);
    }

    [HttpGet]
    public async Task<ActionResult<ToDo?>> GetById([FromQuery] Guid id, CancellationToken cancellationToken) 
    {
        var toDo = await _toDoService.GetByIdAsync(id, cancellationToken);

        if (toDo == null) 
        {
            return NotFound();
        }

        return Ok(toDo);
    }

    [HttpDelete]
    public async Task RemoveById([FromQuery] Guid id, CancellationToken cancellationToken) 
    {
        await _toDoService.DeleteById(id, cancellationToken);
    }
}
