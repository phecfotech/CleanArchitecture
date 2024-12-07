namespace CleanArchitecture.Domain.Abstractions;

public class Result
{
    protected internal Result(bool isSuccess, Error error)
    {
        if (isSuccess && error != Error.None)
        {
            throw new InvalidOperationException();
        }
        if (!isSuccess && error == Error.None)
        {
            throw new InvalidOperationException();
        }

        IsSuccess = isSuccess;
        Error = error;
    }
    public bool IsSuccess { get; }
    public bool IsFailure => !IsSuccess;
    public Error Error { get; }

    public static Result Sucess() => new(true, Error.None);

    public static Result Failure(Error error) => new(false, error);

    public static Result<Tvalue> Success<Tvalue>(Tvalue value)
    => new(value, true, Error.None);
    public static Result<Tvalue> Failure<Tvalue>(Error error)
  => new(default, false, error);
    public static Result<Tvalue> Create<Tvalue>(Tvalue? value)
  => value is not null ? Sucess(value) : Failure<Tvalue>(Error.NullValue);

[NotNull]
public class Result<Tvalue> : Result {
        private readonly Tvalue? _value;

        protected internal Result(Tvalue? value, bool isSuccess, Error error) : base(isSuccess, errro)
        {
            _value = value;
        }

        public Tvalue Value => IsSuccess ? _value! : throw new InvalidOperationException("El resultado del valor de error no es admisible");

        public static impplicit operator Result<Tvalue> (Tvalue value) => Create (value)
}
}