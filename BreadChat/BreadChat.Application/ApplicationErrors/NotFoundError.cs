namespace BreadChat.Application.ApplicationErrors;

public class NotFoundError : ApplicationError
{
    public NotFoundError()
    {
    }

    public NotFoundError(string? message) : base(message)
    {
    }

    public NotFoundError(string? message, Exception? innerException) : base(message, innerException)
    {
    }
}