using ToDoTaskManager.Application.ToDos;
using ToDoTaskManager.Application.ToDos.Exceptions;
using ToDoTaskManager.Domain.Core;
using ToDoTaskManager.Domain.ToDos;
using ToDoTaskManager.Infrastructure;
using ToDoTaskManager.Infrastructure.Exceptions;
using ToDoTaskManager.Infrastructure.ToDos;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IToDoRepository, ToDoRepository>();
builder.Services.AddScoped<ToDoService>();
builder.Services.AddScoped<ToDoTaskManagerContext>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//app.Use(
//    async (context, next) =>
//    {
//        await next(context);

//        context.Response.Headers.Add("Super-Header", "Test");
//    });

app.UseHttpsRedirection();

app.UseAuthorization();

app.Use(
    async (context, next) =>
    {
        try
        {
            await next();
        }
        catch (ApplicationExeption e)
        {
            context.Response.StatusCode = e.StatusCode;
            await context.Response.WriteAsJsonAsync(e.Message);
        }
        catch (BusinessExeption e)
        {
            context.Response.StatusCode = e.StatusCode;
            await context.Response.WriteAsJsonAsync(e.Message);
        }
        catch (InfrastructureException e) 
        {
            context.Response.StatusCode = e.StatusCode;
            await context.Response.WriteAsJsonAsync(e.Message);
        }

    });

app.MapControllers();

app.Run();
