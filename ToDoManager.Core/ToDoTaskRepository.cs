namespace ToDoManager.Core;

public class ToDoTaskRepository
{
    private Dictionary<uint, ToDoTask> _taskList;

    public ToDoTaskRepository()
    {
        _taskList = new Dictionary<uint, ToDoTask>();
    }

    public void Add(ToDoTask toDoTask)
    {
        _taskList.Add(toDoTask.Id, toDoTask);
    }

    public void Update(ToDoTask toDoTask)
    {
        _taskList[toDoTask.Id] = toDoTask;
    }

    public void Remove(ToDoTask toDoTask)
    {
        _taskList.Remove(toDoTask.Id);
    }

    public IEnumerable<ToDoTask> GetToDoWithinRange(DateTime? from, DateTime? to)
    {
        List<ToDoTask> newTaskList = new List<ToDoTask>();

        if (from == null && to == null)
        {
            return _taskList.Values;
        }

        foreach (ToDoTask toDoTask in _taskList.Values)
        {
            if ((from == null || toDoTask.Deadline >= from.Value)
                && (to == null || toDoTask.Deadline <= to.Value))
            {
                newTaskList.Add(toDoTask);
            }
        }
        return newTaskList;
    }

    public List<ToDoTask> GetToDoBeforeSelectedDeadline(DateTime dateTime)
    {
        List<ToDoTask> newTaskList = new List<ToDoTask>();

        foreach (ToDoTask toDoTask in _taskList.Values)
        {
            if (DateTime.Compare(toDoTask.Deadline, dateTime) < 0)
            {
                newTaskList.Add(toDoTask);
            }
        }
        return newTaskList;
    }
}

// Брошенный код :(
//interface IDateTimeFake
//{
//    DateTime MakeFakeDateTimeNow();
//}

//public class FakeDateTime : IDateTimeFake
//{
//    public DateTime MakeFakeDateTimeNow()
//    {
//       return new DateTime(22, 7, 24, 22, 0, 0);
//    }
//}