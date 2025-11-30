using E_Commerce.Shared.Common;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ApiBaseController : ControllerBase
    {

        protected IActionResult FromResult(Result result)
        {
            if (result.IsSuccess)
                return NoContent();

            var first = result.Errors.First();

            var problem = new ProblemDetails
            {
                Status = MapStatusCode(first.ErrorType),
                Title = first.Code,
                Detail = first.Description
            };

            problem.Extensions["errors"] = result.Errors.Select(e => new
            {
                code = e.Code,
                description = e.Description,
                type = e.ErrorType.ToString()
            });

            return StatusCode(problem.Status.Value, problem);
        }

        // Handle Result with value
        protected IActionResult FromResult<T>(Result<T> result)
        {
            if (result.IsSuccess && result.HasValue)
                return Ok(result.Value);

            var first = result.Errors.First();

            var problem = new ProblemDetails
            {
                Status = MapStatusCode(first.ErrorType),
                Title = first.Code,
                Detail = first.Description
            };

            problem.Extensions["errors"] = result.Errors.Select(e => new
            {
                code = e.Code,
                description = e.Description,
                type = e.ErrorType.ToString()
            });

            return StatusCode(problem.Status.Value, problem);
        }


        private static int MapStatusCode(ErrorType type)
        {
            return type switch
            {
                ErrorType.Validation => StatusCodes.Status400BadRequest,
                ErrorType.NotFound => StatusCodes.Status404NotFound,
                ErrorType.Unauthorized => StatusCodes.Status401Unauthorized,
                ErrorType.Forbidden => StatusCodes.Status403Forbidden,
                ErrorType.InvalidCrendentials => StatusCodes.Status401Unauthorized,
                _ => StatusCodes.Status500InternalServerError
            };
        }
    }
}
