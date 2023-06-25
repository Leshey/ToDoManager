using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDoTaskManager.Application;

public sealed class ToDoNotFoundExeption : ApplicationExeption
{
    private const int STATUS_CODE = 404;
    
    public ToDoNotFoundExeption(Guid id) : base($"No ToDo has been found with this id: {id}", STATUS_CODE) 
    {
    }
}
