using Azure;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce.Api.Middleware
{
    public class ExceptionHandlerMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionHandlerMiddleware> _logger;
        private ProblemDetails response = new ProblemDetails();

        public ExceptionHandlerMiddleware(RequestDelegate next, ILogger<ExceptionHandlerMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }
        public async Task InvokeAsync(HttpContext context)
        {

            try
            {
                await _next(context);

                context.Response.ContentType = "application/json";

                response.Instance = context.Request.Path;

                await HandleNotFoundExceptionAsync(context);

                await HandleBadRequestExceptionAsync(context);

                await HandleUnauthorizedExceptionAsync(context);

                await HandleForbiddenExceptionAsync(context);
                await HandleConflictExceptionAsync(context);
                await context.Response.WriteAsJsonAsync(response);

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unhandled exception occurred while processing the request.");

                await HandleInternalServerErrorExceptionAsync(context);

                await context.Response.WriteAsJsonAsync(response);
            }
        }

        private async Task HandleInternalServerErrorExceptionAsync(HttpContext context)
        {
            context.Response.StatusCode = StatusCodes.Status500InternalServerError;
            response.Title = "An unexpected error occurred.";
            response.Status = StatusCodes.Status500InternalServerError;
            response.Detail = "An unexpected error occurred on the server. Please try again later.";
        }
        private async Task HandleNotFoundExceptionAsync(HttpContext context)
        {
            if (context.Response.StatusCode == StatusCodes.Status404NotFound)
            {
                context.Response.StatusCode = StatusCodes.Status404NotFound;
                response.Status = StatusCodes.Status404NotFound;
                response.Detail = "The requested resource was not found on the server.";
                response.Title = "Resource Not Found";
            }
        }
        private async Task HandleBadRequestExceptionAsync(HttpContext context)
        {
            if (context.Response.StatusCode == StatusCodes.Status400BadRequest)
            {
                context.Response.StatusCode = StatusCodes.Status400BadRequest;
                response.Status = StatusCodes.Status400BadRequest;
                response.Detail = "The request could not be understood or was missing required parameters.";
                response.Title = "Bad Request";
            }
        }
        private async Task HandleUnauthorizedExceptionAsync(HttpContext context)
        {
            if (context.Response.StatusCode == StatusCodes.Status401Unauthorized)
            {
                context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                response.Status = StatusCodes.Status401Unauthorized;
                response.Detail = "Authentication is required and has failed or has not yet been provided.";
                response.Title = "Unauthorized";
            }
        }
        private async Task HandleForbiddenExceptionAsync(HttpContext context)
        {
            if (context.Response.StatusCode == StatusCodes.Status403Forbidden)
            {
                context.Response.StatusCode = StatusCodes.Status403Forbidden;
                response.Status = StatusCodes.Status403Forbidden;
                response.Detail = "You do not have permission to access this resource.";
                response.Title = "Forbidden";
            }
        }
        private async Task HandleConflictExceptionAsync(HttpContext context)
        {
            if (context.Response.StatusCode == StatusCodes.Status409Conflict)
            {
                context.Response.StatusCode = StatusCodes.Status409Conflict;
                response.Status = StatusCodes.Status409Conflict;
                response.Detail = "The request could not be completed due to a conflict with the current state of the resource.";
                response.Title = "Conflict";
            }
        }
    }
}
