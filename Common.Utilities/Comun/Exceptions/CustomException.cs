using Common.Utilities.Bases;

namespace Common.Utilities.Exceptions;


public class CustomException : Exception
{
    public IEnumerable<BaseError> Errors { get; }

    public CustomException() : base()
    {
        Errors = new List<BaseError>();
    }

    public CustomException(IEnumerable<BaseError> errors) : this()
    {
        Errors = errors;
    }
    public CustomException(string message, Exception innerException) : base(message, innerException)
    {}
}
