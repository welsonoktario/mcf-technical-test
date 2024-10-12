using System.Security.Claims;
using Backend.Data.Dto;
using Backend.Data.Repositories;
using Backend.Models;
using Backend.Utils;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers;

[ApiController]
[Route("api/auth")]
public class AuthController : ControllerBase
{
    private readonly IUserRepository _userRepository;
    private readonly ILogger<AuthController> _logger;

    public AuthController(IUserRepository userRepository, ILogger<AuthController> logger)
    {
        _userRepository = userRepository;
        _logger = logger;
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginDto payload)
    {
        if (!ModelState.IsValid)
        {
            var errors = ModelStateUtils.GetModelStateErrors(ModelState);

            return ApiResponse.Fail(
                statusCode: 400,
                message: "Login failed",
                errors: errors
            );
        }

        try
        {
            UserModel? user = await _userRepository.Find(payload.UserName);

            if (user == null || user.Password != payload.Password)
            {
                return ApiResponse.Fail(
                    statusCode: 400,
                    message: "Invalid user name or password"
                );
            }

            var claims = new List<Claim>{
                new (ClaimTypes.Name, user.UserName)
            };
            var claimsIdentity = new ClaimsIdentity(claims, "Cookies");
            var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);
            await HttpContext.SignInAsync("Cookies", claimsPrincipal);

            return ApiResponse.Success(message: "Login success", data: user);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "[AuthController][Login] - An error occurred: {msg}", ex.Message);

            return ApiResponse.Fail(
                statusCode: 500,
                message: "Unhandled error. Please try again"
            );
        }
    }
}
