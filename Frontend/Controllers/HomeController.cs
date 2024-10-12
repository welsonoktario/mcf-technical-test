using System.Diagnostics;
using System.Net;
using System.Text.Json;
using Frontend.Models;
using Microsoft.AspNetCore.Mvc;

namespace Frontend.Controllers;

public class HomeController : Controller
{
    private readonly IConfiguration _configuration;
    private readonly HttpClient _httpClient;
    private readonly ILogger<HomeController> _logger;

    public HomeController(IConfiguration configuration, IHttpClientFactory httpClientFactory, ILogger<HomeController> logger)
    {
        _configuration = configuration;
        _httpClient = httpClientFactory.CreateClient();
        _httpClient.BaseAddress = new Uri(_configuration.GetValue("BackendUrl", "https://localhost:7200"));
        _logger = logger;
    }

    public async Task<IActionResult> Index()
    {
        try
        {
            var response = await _httpClient.GetFromJsonAsync<ApiResponse<List<Bpkb>>>("api/bpkb");

            return View(new HomeViewModel
            {
                Bpkbs = response?.Data ?? Array.Empty<Bpkb>().ToList()
            });
        }
        catch (HttpRequestException ex)
        {
            if (ex.StatusCode == HttpStatusCode.Unauthorized)
            {
                return Redirect("/Auth/Login");
            }

            _logger.LogError(ex, "[HomeController][Index] - HTTP request error occurred: {msg}", ex.Message);

            return View(new HomeViewModel
            {
                IsError = true
            });
        } catch (Exception ex) {
            _logger.LogError(ex, "[HomeController][Index] - An unhandled exception occurred: {msg}", ex.Message);

            return View(new HomeViewModel
            {
                IsError = true
            });
        }
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
