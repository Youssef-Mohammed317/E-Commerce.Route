using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Shared.Common
{
    public class Error
    {
        private Error(string code, string description, ErrorType errorType)
        {
            Code = code;
            Description = description;
            ErrorType = errorType;
        }

        public string Code { private set; get; } = default!;
        public string Description { private set; get; } = default!;

        public ErrorType ErrorType { private set; get; } = ErrorType.Unknown;

        // Factory helpers
        public static Error Failure(
        string code = "ERR_FAILURE",
        string description = "An unexpected error has occurred.")
        => new Error(code, description, ErrorType.Failure);

        public static Error Validation(
            string code = "ERR_VALIDATION",
            string description = "The request data is invalid.")
            => new Error(code, description, ErrorType.Validation);

        public static Error NotFound(
            string code = "ERR_NOT_FOUND",
            string description = "The requested resource was not found.")
            => new Error(code, description, ErrorType.NotFound);

        public static Error Unauthorized(
            string code = "ERR_UNAUTHORIZED",
            string description = "Authentication is required or has failed.")
            => new Error(code, description, ErrorType.Unauthorized);

        public static Error Forbidden(
            string code = "ERR_FORBIDDEN",
            string description = "You do not have permission to perform this action.")
            => new Error(code, description, ErrorType.Forbidden);

        public static Error InvalidCrendentials(
            string code = "ERR_INVALID_CREDENTIALS",
            string description = "The provided authentication credentials are invalid.")
            => new Error(code, description, ErrorType.InvalidCrendentials);


    }
}
