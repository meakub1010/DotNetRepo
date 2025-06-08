using Microsoft.AspNetCore.Mvc;

[Route("[controller]")]
public class RetryTestController : Controller
{
    private readonly ExternalApiService _externalApiService;
    public RetryTestController(ExternalApiService externalApiService)
    {
        _externalApiService = externalApiService;
    }

    // public IActionResult Index()
    // {
    //     return Ok("Hello Retry Test!!!");
    // }

    //[HttpGet("/retrytest/tryagain")]
    [HttpGet]
    public async Task<IActionResult> TryAgain()
    {
        //Simulate a retry operation
        // try
        // {
        // Simulate a failure
        var result = await _externalApiService.GetDataFromApiAsync();
        return Ok(result);

        // }
        // catch (Exception ex)
        // {
        //     // Handle the exception, log it, or return an error response
        //     return StatusCode(500, $"An error occurred: {ex.Message}");
        // }
    }
}   