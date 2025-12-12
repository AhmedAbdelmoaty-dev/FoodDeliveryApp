using Domain.Errors;

namespace Domain.Abstractions.Result
{
    public interface IAppResult
    {
        bool IsSuccess { get; }

        
        Error Error { get; }

    }

    public interface IAppResult<T>
    {
        T Value { get; }
    }
}
