using Microsoft.AspNetCore.Mvc;
using System.Text.Json.Serialization;
using ToDoTaskManager.Application.ToDos;
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
    public async Task<CreateNewToDoResponse> CreateNewToDo([FromBody] CreateNewToDoRequest request, CancellationToken cancellationToken)
    {
        return new CreateNewToDoResponse(await _toDoService.CreateNewToDoAsync(request.Name, cancellationToken));
    }

    [HttpGet("GetById")]
    public async Task<ActionResult<GetByIdResponse?>> GetById([FromQuery] Guid id, CancellationToken cancellationToken) 
    {
        var toDos = await _toDoService.GetByIdAsync(id, cancellationToken);

        if (toDos == null) 
        {
            return NotFound();
        }
        
        return Ok(new GetByIdResponse(toDos.Id, toDos.Name, toDos.DoneTime, toDos.IsDone));
    }

    [HttpGet("GetToDos")]
    public async Task<ActionResult<GetToDosResponse>> GetToDos([FromQuery] int count, [FromQuery] int page, CancellationToken cancellationToken) 
    {
        var toDos = await _toDoService.GetToDos(count, page, cancellationToken);

        var toDosResponse = new List<GetToDosResponseItem>();
        
        foreach (var toDo in toDos.ToDos)
        {
            toDosResponse.Add(new GetToDosResponseItem(toDo.Id, toDo.Name, toDo.DoneTime, toDo.IsDone));
        }

        return Ok(toDosResponse);
    }

    [HttpDelete]
    public async Task<ActionResult<RemoveByIdResponse>> RemoveById([FromQuery] RemoveByIdRequest request, CancellationToken cancellationToken) 
    {
        await _toDoService.DeleteById(request.id, cancellationToken);
        
        return Ok(new RemoveByIdResponse());
    }

    [HttpPut]
    public async Task<ActionResult<CloseByIdResponse>> CloseById([FromQuery] CloseByIdRequest request, CancellationToken cancellationToken)
    {
        await _toDoService.CloseToDoById(request.Id, cancellationToken);

        return Ok(new CloseByIdResponse());
    }
}

public record CreateNewToDoRequest(string Name);
public record CreateNewToDoResponse(Guid id);

public record RemoveByIdRequest(Guid id);
public record RemoveByIdResponse();

public record CloseByIdRequest(Guid Id);
public record CloseByIdResponse();