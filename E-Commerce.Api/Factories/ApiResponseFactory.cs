using Microsoft.AspNetCore.Mvc;

namespace E_Commerce.Api.Factories
{
    public static class ApiResponseFactory
    {
        public static IActionResult GenerateApiValidationResponse(ActionContext context)
        {
            var problem = new ValidationProblemDetails(context.ModelState)
            {
                Status = StatusCodes.Status400BadRequest,
                Title = "One or more validation errors occurred.",
                Detail = "See the errors property for details.",
                Instance = context.HttpContext.Request.Path,
                Errors = context.ModelState
                            .Where(e => e.Value?.Errors.Count > 0)
                            .ToDictionary(
                                kvp => kvp.Key,
                                kvp => kvp.Value?.Errors.Select(e => e.ErrorMessage).ToArray() ?? Array.Empty<string>()
                            ),
                Extensions =
                        {
                            { "traceId", context.HttpContext.TraceIdentifier }
                        }
            };
            return new BadRequestObjectResult(problem);
        }
    }
}
