using Microsoft.AspNetCore.Mvc;

namespace MealMaker.Controllers;

public class ErrorsController : ApiController
{
    [Route("/error")]
    public IActionResult Error()
    {
        return Problem();
    }
}