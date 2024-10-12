using System.Globalization;
using System.Net;
using System.Text.Json;
using Frontend.Models;
using Microsoft.AspNetCore.Mvc;

namespace Frontend.Controllers;

public class BpkbController : Controller
{
    private readonly IConfiguration _configuration;
    private readonly HttpClient _httpClient;
    private readonly ILogger<BpkbController> _logger;

    public BpkbController(IConfiguration configuration, IHttpClientFactory httpClientFactory, ILogger<BpkbController> logger)
    {
        _configuration = configuration;
        _httpClient = httpClientFactory.CreateClient();
        _httpClient.BaseAddress = new Uri(_configuration.GetValue("BackendUrl", "https://localhost:7200"));
        _logger = logger;
    }

    [HttpGet]
    public async Task<ActionResult> Create()
    {
        try
        {
            var response = await _httpClient.GetFromJsonAsync<ApiResponse<List<StorageLocation>>>("api/storage-locations");

            return View(new CreateBpkbViewModel
            {
                StorageLocations = response?.Data ?? Array.Empty<StorageLocation>().ToList()
            });
        }
        catch (HttpRequestException ex)
        {
            if (ex.StatusCode == HttpStatusCode.Unauthorized)
            {
                return Redirect("/Auth/Login");
            }

            _logger.LogError(ex, "[BpkbController][Create] - HTTP request error occurred: {msg}", ex.Message);

            return View(new CreateBpkbViewModel
            {
                IsError = true,
                Message = ex.Message,
            });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "[BpkbController][Create] - An unhandled exception occurred: {msg}", ex.Message);

            return View(new CreateBpkbViewModel
            {
                IsError = true,
                Message = "Terjadi kesalahan memuat data, silahkan coba lagi nanti",
            });
        }
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromForm] CreateBpkbFormData formData)
    {
        var model = new CreateBpkbViewModel();
        model.FormData = formData;

        try
        {
            var response = await _httpClient.GetFromJsonAsync<ApiResponse<List<StorageLocation>>>("api/storage-locations");

            model.StorageLocations = response?.Data ?? Array.Empty<StorageLocation>().ToList();
        }
        catch (HttpRequestException ex)
        {
            if (ex.StatusCode == HttpStatusCode.Unauthorized)
            {
                return Redirect("/Auth/Login");
            }

            _logger.LogError(ex, "[BpkbController][Create] - HTTP request error occurred: {msg}", ex.Message);
            
            model.IsError = true;
            model.Message = ex.Message;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "[BpkbController][Create] - An unhandled exception occurred: {msg}", ex.Message);

            model.IsError = true;
            model.Message = "Terjadi kesalahan memuat data, silahkan coba lagi nanti";
        }

        try
        {
            var response = await _httpClient.PostAsJsonAsync("api/bpkb", formData);
            var responseStr = await response.Content.ReadAsStringAsync();
            Console.WriteLine(responseStr);

            if (response.IsSuccessStatusCode)
            {
                return Redirect("/");
            }

            var errorJson = await response.Content.ReadFromJsonAsync<ApiResponse<object?>>();

            model.IsError = true;
            model.Message = errorJson.Message;
            model.Errors = errorJson.Errors;
        }
        catch (HttpRequestException ex)
        {
            if (ex.StatusCode == HttpStatusCode.Unauthorized)
            {
                return Redirect("/Auth/Login");
            }

            _logger.LogError(ex, "[BpkbController][Create] - HTTP request error occurred: {msg}", ex.Message);

            model.IsError = true;
            model.Message = ex.Message;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "[BpkbController][Create] - An unhandled exception occurred: {msg}", ex.Message);

            model.IsError = true;
            model.Message = ex.Message;
        }

        return View(model);
    }
}