namespace MusicNotification.Common.Exceptions;

public class EntityProcessException : Exception
{
    public EntityProcessException()
    {
    }

    public EntityProcessException(string message)
        : base(message)
    {
    }

    public EntityProcessException(string message, Exception inner)
        : base(message, inner)
    {
    }
}
