using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Shared.Common
{
    public class Result
    {
        private readonly List<Error> _errors = new();

        public bool IsSuccess => _errors.Count == 0;
        public bool IsFailure => !IsSuccess;
        public IReadOnlyList<Error> Errors => _errors;

        // Ok - Success
        protected Result()
        {
        }

        // Fail with single Error
        protected Result(Error error)
        {
            _errors.Add(error);
        }

        // Fail with multiple Errors (optional convenience)
        protected Result(IEnumerable<Error> errors)
        {
            _errors.AddRange(errors);
        }

        // Factory helpers
        public static Result Ok() => new Result();

        public static Result Fail(Error error) => new Result(error);

        public static Result Fail(IEnumerable<Error> errors) => new Result(errors);
    }

    public class Result<T> : Result
    {
        public T Value { get; } = default!;
        public bool HasValue =>
         IsSuccess && !EqualityComparer<T>.Default.Equals(Value, default!);

        // Success with data
        protected Result(T value) : base()
        {
            Value = value;
        }

        // Failure with errors
        protected Result(IEnumerable<Error> errors) : base(errors)
        {
        }

        // Failure with error
        protected Result(Error error) : base(error)
        {
        }

        // Factory helpers
        public static Result<T> Ok(T value) => new Result<T>(value);

        public static new Result<T> Fail(IEnumerable<Error> errors)
            => new Result<T>(errors);

        public static new Result<T> Fail(Error error)
            => new Result<T>(error);
    }
}
