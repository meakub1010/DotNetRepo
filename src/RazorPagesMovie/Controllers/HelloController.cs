using Microsoft.AspNetCore.Mvc;

[Route("[controller]")]
public class HelloController : Controller
{
    public IActionResult Index()
    {
        return Ok("Hello Muhammad!!!");
    }

    [HttpGet("/hello/{name}")]
    public IActionResult Greet(string name)
    {
        ViewData["Message"] = $"Hello, {name}!";
        return View("Index", name);
    }
}