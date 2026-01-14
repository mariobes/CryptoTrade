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
    public IActionResult Login([FromBody] UserLoginDto dto)
    {
        try
        {
            var user = _authService.CheckLogin(dto.EmailOrPhone, dto.Password);
            if (user != null)
            {
                var token = _authService.GenerateJwtToken(user);
                return Ok(token);
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

    [HttpPost("register")]
    public IActionResult Register([FromBody] UserCreateDto dto)
    {
        if (!ModelState.IsValid)  {return BadRequest(ModelState); } 

        try 
        {
            var user = _userService.RegisterUser(dto);
            if (!user.IsAdult())
            {
                return BadRequest("Debes ser mayor de 18 años para registrarte.");
            }
            return CreatedAtAction(nameof(Login), new { userId = user.Id }, user);
        }     
        catch (Exception ex)
        {
            return BadRequest($"Error al registrar el usuario. {ex.Message}");
        }
    }
}