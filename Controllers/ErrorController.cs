using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace ToDoListApp.Controllers;

[ApiController]
[Route("[controller]")]
public class ErrorController : ControllerBase
{
    private readonly ILogger<ErrorController> _logger;

    public ErrorController(ILogger<ErrorController> logger)
    {
        _logger = logger;
    }

    [Route("/error")]
    [HttpGet]
    public IActionResult Error()
    {
        var context = HttpContext.Features.Get<IExceptionHandlerFeature>();
        var exception = context?.Error;

        if (exception == null)
        {
            _logger.LogError("An error occurred but exception was null");
            return Problem(title: "An error occurred", detail: "Unknown error");
        }

        _logger.LogError(exception, "An error occurred: {Message}", exception.Message);

        return Problem(detail: exception.Message, title: "An error occurred");
    }
}