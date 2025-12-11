using Domain.Errors;

namespace Domain.Abstractions
{
    public class Result
    {
        public bool IsSuccess { get; }

        public bool IsFailure => !IsSuccess;    

        public Error Error { get; }

        protected Result(bool isSuccess, Error error)
        {
            if(isSuccess && error != Error.None)
            {
                throw new InvalidOperationException("A successful result cannot have an error.");
            }

            if(!isSuccess && error == Error.None)
            {
                throw new InvalidOperationException("A failed result must have an error.");
            }


            IsSuccess = isSuccess;
            Error = error;
        }

        public static Result Success() => new(true, Error.None);

        public static Result Failure(Error error) => new(false, error);

        public TResult Match<TResult>(Func<TResult> onSuccess,Func<Error,TResult> onFailure)
        {
            return IsSuccess ? onSuccess() : onFailure(Error);
        }

    }

    public  class Result<T>: Result
    {

        private readonly T _value;

        public T Value
        {
            get
            {
                if (IsFailure)
                {
                    throw new InvalidOperationException("Cannot access the value of a failed result.");
                }
                return _value;
            }
        }

        private Result(bool isSuccess, T value, Error error):base(isSuccess, error)
        {
            _value = value;
        }
        public static Result<T> Success(T value) => new(true, value, Error.None);
        public static Result<T> Failure(Error error) => new(false, default, error);

        public TResult Match<TResult>(Func<T,TResult>onSuccess,Func<Error,TResult> onFailure)
        {
            return IsSuccess ? onSuccess(Value) : onFailure(Error);
        }
    }
}
