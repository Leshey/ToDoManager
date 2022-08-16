namespace ToDoManager.Core;

public class IdSetter 
{
    private static uint? _id;
    private static IdSetter _instance;

    private IdSetter()
    {
    }

    public static IdSetter GetInctance()
    {
        if(_instance == null)
        {
            _instance = new IdSetter();
        }
        return _instance;
    }

    public uint SetId()
    {
        if(_id == null)
        {
            _id = 0;
            return _id.Value;
        }
        else
        {
            _id += 1;
            return _id.Value;
        }
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