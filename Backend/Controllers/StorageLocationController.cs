using Backend.Data.Dto;
using Backend.Data.Interfaces;
using Backend.Data.Repositories;
using Backend.Models;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers;

[ApiController]
[Route("api/storage-locations")]

public class StorageLocationController : ControllerBase
{
    private readonly IUserRepository _userRepository;
    private readonly IBaseRepository<StorageLocationModel> _storageLocationRepository;
    private readonly ILogger<StorageLocationController> _logger;

    public StorageLocationController(IUserRepository userRepository, IBaseRepository<StorageLocationModel> storageLocationRepository, ILogger<StorageLocationController> logger)
    {
        _userRepository = userRepository;
        _storageLocationRepository = storageLocationRepository;
        _logger = logger;
    }

    [HttpGet("")]
    public async Task<IActionResult> Index()
    {
        var userName = User.Identity?.Name;
        if (userName == null)
        {
            return ApiResponse.Fail(
                statusCode: StatusCodes.Status401Unauthorized,
                message: "Unauthorized"
            );
        }

        var user = await _userRepository.Find(userName);
        if (user == null)
        {
            return ApiResponse.Fail(
                statusCode: StatusCodes.Status401Unauthorized,
                message: "Unauthorized"
            );
        }

        var storageLocations = await _storageLocationRepository.GetAll();

        return ApiResponse.Success(storageLocations);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Show(string id)
    {
        var userName = User.Identity?.Name;
        if (userName == null)
        {
            return ApiResponse.Fail(
                statusCode: StatusCodes.Status401Unauthorized,
                message: "Unauthorized"
            );
        }

        var user = await _userRepository.Find(userName);
        if (user == null)
        {
            return ApiResponse.Fail(
                statusCode: StatusCodes.Status401Unauthorized,
                message: "Unauthorized"
            );
        }

        if (string.IsNullOrEmpty(id))
        {
            return ApiResponse.Fail(
                statusCode: StatusCodes.Status400BadRequest,
                message: "Id cannot be empty"
            );
        }

        var storageLocation = await _storageLocationRepository.Find(id);

        if (storageLocation == null)
        {
            return ApiResponse.Fail(
                statusCode: StatusCodes.Status400BadRequest,
                message: "Data doesn't exists"
            );
        }

        return ApiResponse.Success(
            message: "Data found",
            data: storageLocation
        );
    }
}
