namespace Common
{
    public enum JobType
    {
        Interval,
        Timming,
        OnlyOne
    }

    public enum UserHttpCode
    {
        Error = -1,
        Success = 0,
        Warning = 1
    }

    public enum IntergrationHandleType
    {
        Insert,
        Update,
        Delete
    }

    public enum Status
    {
        New,
        Read,
        Send
    }

    public enum Permissions
    {
        View,
        Insert,
        Update,
        Delete,
        All
    }

    public enum FilterType
    {
        Day,
        Week,
        Month,
        Quarter,
        Year,
    };
}