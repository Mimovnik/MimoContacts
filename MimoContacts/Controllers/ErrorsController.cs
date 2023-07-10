using Microsoft.AspNetCore.Mvc;

namespace BubskiBreakfast.Controllers;

public class ErrorsController : ControllerBase
{
    [Route("/error")]
    public IActionResult Error()
    {
        return Problem();
    }
}