using System.Runtime.Serialization;

namespace BreadChat.Application.ApplicationErrors;

public class ApplicationError : Exception
{
    public ApplicationError()
    {
    }

    protected ApplicationError(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }

    public ApplicationError(string? message) : base(message)
    {
    }

    public ApplicationError(string? message, Exception? innerException) : base(message, innerException)
    {
    }
}