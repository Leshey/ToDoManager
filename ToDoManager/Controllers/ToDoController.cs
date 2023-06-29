using Microsoft.AspNetCore.Mvc;
using ToDoTaskManager.Application.ToDos;
using ToDoTaskManager.Domain.ToDos;
using ToDoTaskManager.WebApi.Responses;

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

    [HttpGet("GetById")]
    public async Task<ActionResult<GetByIdResponse?>> GetById([FromQuery] Guid id, CancellationToken cancellationToken) 
    {
        var toDo = await _toDoService.GetByIdAsync(id, cancellationToken);

        if (toDo == null) 
        {
            return NotFound();
        }

        return Ok(new GetByIdResponse(toDo.Id, toDo.Name, toDo.DoneTime, toDo.IsDone));
    }

    [HttpGet("GetToDos")]
    public async Task<ActionResult<IEnumerable<ToDo?>>> GetToDos([FromQuery] int count, [FromQuery] int page, CancellationToken cancellationToken) 
    {
        var toDos = await _toDoService.GetToDos(count, page, cancellationToken);

        return Ok(toDos);
    }

    [HttpDelete]
    public async Task<ActionResult> RemoveById([FromQuery] Guid id, CancellationToken cancellationToken) 
    {
        await _toDoService.DeleteById(id, cancellationToken);
        
        return Ok();
    }

    [HttpPut]
    public async Task<ActionResult> CloseById([FromQuery] Guid id, CancellationToken cancellationToken)
    {
        await _toDoService.CloseToDoById(id, cancellationToken);

        return Ok();
    }
}
