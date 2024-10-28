using Microsoft.AspNetCore.Mvc;

using CryptoTrade.Business;
using CryptoTrade.Models;


namespace CryptoTrade.API.Controllers;

[ApiController]
[Route("[controller]")]
public class AuthController : ControllerBase
{
    private readonly IUserService _userService;
    private readonly IAuthService _authService;

    public AuthController(IUserService userService, IAuthService authService)
    {
        _userService = userService;
        _authService = authService;
    }

    [HttpPost("login")]
    public IActionResult Login([FromBody] UserLoginDTO userLoginDTO)
    {
        try
        {
            var user = _authService.CheckLogin(userLoginDTO.Email, userLoginDTO.Password);
            if (user != null)
            {
                return Ok("Has iniciado sesión");
            }
            else
            {
                return NotFound("No se ha encontrado ningún usuario con esas credenciales");
            }
        }
        catch (KeyNotFoundException knfex)
        {
           return NotFound($"No se ha encontrado ningún usuario. {knfex.Message}");
        }
        catch (Exception ex)
        {
            return BadRequest($"No se ha encontrado ningún usuario. {ex.Message}");
        }
    }

}
