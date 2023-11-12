namespace OMG.SharedKernel.Shared
{
    public readonly struct Result
    {
        private readonly Result<int, StringError> _result => new Result<int, StringError>(0);

        public int Value => _result.Value;

        public StringError? Error => _result.Error;

        public bool Succeeded => _result.Succeeded;

        public static Result<TValue, StringError> Ok<TValue>(TValue value) => new Result<TValue, StringError>(value);
    }

    public readonly struct Result<TValue>
    {
        public Result(TValue value) : this()
        {
            _result = new Result<TValue, StringError>(value);
        }

        private readonly Result<TValue, StringError> _result;

        public TValue Value => _result.Value;

        public StringError? Error => _result.Error;

        public bool Succeeded => _result.Succeeded;

        public static Result<TValue, StringError> Ok(TValue value) => new Result<TValue, StringError>(value);

        public static implicit operator Result<TValue>(Result<TValue, StringError> result) =>
            new Result<TValue>(result.Value);
    }

    public readonly struct Result<TValue, TError> where TError : Error
    {
        public Result(TValue value, TError? error = null)
        {
            Value = value;
            Error = error;
        }

        public TValue Value { get; }

        public TError? Error { get; }

        public bool Succeeded => !(Error is null);

        public static Result<TValue, TError> Ok(TValue value) => new Result<TValue, TError>(value);
    }

    public class StringError : Error
    {
        public override void Throw()
        {
            throw new NotImplementedException();
        }
    }

    public abstract class Error
    {
        public static readonly Error Default = new StringError();

        public abstract void Throw();
    }
}
