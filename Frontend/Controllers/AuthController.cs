using Microsoft.AspNetCore.Mvc;

namespace Frontend.Controllers;

public class AuthController : Controller
{
    private readonly IConfiguration _configuration;
    private readonly HttpClient _httpClient;
    private readonly ILogger<AuthController> _logger;

    public AuthController(IConfiguration configuration, IHttpClientFactory httpClientFactory, ILogger<AuthController> logger)
    {
        _configuration = configuration;
        _httpClient = httpClientFactory.CreateClient();
        _httpClient.BaseAddress = new Uri(_configuration.GetValue("BackendUrl", "https://localhost:7200"));
        _logger = logger;
    }

    [HttpGet]
    public ActionResult Login()
    {
        return View();
    }

    [HttpPost]
    public async Task<ActionResult> ProcessLogin([FromForm] string username, [FromForm] string password)
    {
        var response = await _httpClient.PostAsJsonAsync("api/auth/login", new {
            username,
            password,
        });

        if (!response.IsSuccessStatusCode) {
            return RedirectToAction("Login");
        }

        return Redirect("/");
    }
}
