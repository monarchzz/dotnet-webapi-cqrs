using API.Common.Permissions;
using ErrorOr;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Produces("application/json")]
[Authorize]
public class ApiController : ControllerBase
{
    protected CurrentUser CurrentUser => HttpContext.User.GetCurrentUser();

    protected IActionResult Problem(List<Error> errors)
    {
        if (errors is { Count: 0 })
        {
            return Problem();
        }

        HttpContext.Items["errors"] = errors;
        var firstError = errors.First();

        return Problem(firstError);
    }

    private IActionResult Problem(Error error)
    {
        var statusCode = error.Type switch
        {
            ErrorType.Conflict => StatusCodes.Status409Conflict,
            ErrorType.Validation => StatusCodes.Status400BadRequest,
            ErrorType.NotFound => StatusCodes.Status404NotFound,
            _ => StatusCodes.Status500InternalServerError
        };

        return Problem(statusCode: statusCode, title: error.Description);
    }
}