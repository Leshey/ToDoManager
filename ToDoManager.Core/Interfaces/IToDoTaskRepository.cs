namespace ToDoManager.Core;

public interface IToDoTaskRepository
{
    void Add(ToDoTask toDoTask);
    void Update(ToDoTask toDoTask);
    void Remove(ToDoTask toDoTask);
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