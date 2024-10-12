using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Backend.Data.Dto;

public class LoginDto
{
    [JsonPropertyName("username")]
    [Required(ErrorMessage = "Please enter the user name")]
    public string UserName { get; set; }

    [JsonPropertyName("password")]
    [Required(ErrorMessage = "Please enter the password")]
    public string Password { get; set; }
}
