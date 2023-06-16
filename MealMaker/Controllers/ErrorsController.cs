using Microsoft.AspNetCore.Mvc;

namespace MealMaker.Controllers;

public class ErrorsContorller : ApiController
{
    [Route("/error")]
    public IActionResult Error()
    {
        return Problem();
    }
}