namespace ToDoManager.Core;

public class ToDoTask
{
    private readonly string _name;
    private DateTime _deadline;
    private DateTime? _timeWhenCompleted;

    public ToDoTask(string taskName, DateTime deadline) : this(taskName, deadline, null)
    {
    }

    public ToDoTask(string name, DateTime deadline, DateTime? taskTimeWhenCompleted)
    {
        _name = name;
        _deadline = deadline;
        _timeWhenCompleted = taskTimeWhenCompleted;
        Id = IdSetter.GetInctance().SetId();
    }
    
    public ToDoTask(uint id, string name, DateTime deadline, DateTime? taskTimeWhenCompleted)
    {
        Id = id;
        _name = name;
        _deadline = deadline;
        _timeWhenCompleted = taskTimeWhenCompleted;
    }

    public uint Id { get; }
    public string Name { get { return _name; } }
    public DateTime Deadline { get { return _deadline; } }
    public DateTime? TimeWhenCompleted { get { return _timeWhenCompleted; } }
    public bool Status => _timeWhenCompleted != null;

    public void Complete()
    {
        if (Status)
        {
            return;
        }
        _timeWhenCompleted = DateTime.Now;
    }

    public void Reopen()
    {
        Reopen(_deadline);
    }

    public void Reopen(DateTime newDeadline)
    {
        _timeWhenCompleted = null;
        _deadline = newDeadline;
    }

    public void PostponeDeadline(DateTime newDeadline)
    {
        SetDeadline(newDeadline);
    }

    private void SetDeadline(DateTime deadline)
    {
        _deadline = deadline;
    }

    public override bool Equals(object? obj)
    {
        if(obj is ToDoTask toDoTask)
        {
            return Id == toDoTask.Id 
                && Name == toDoTask.Name 
                && Deadline.Equals(toDoTask.Deadline)
                && Nullable.Equals(TimeWhenCompleted, toDoTask.TimeWhenCompleted);
        }
        return false;
    }

    public override string ToString()
    {
        var TimeCompletedString = String.Empty;
        if (Status)
        {
            TimeCompletedString = $"Time when completed: {TimeWhenCompleted}, ";
        }
        else
        {
            TimeCompletedString = $"Time when completed: NOT_COMPLETED, ";
        }
        return $"Id: {Id}, Name: {Name}, Deadline: {Deadline}, {TimeCompletedString}Status: {Status}";
    }
}

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