using Microsoft.AspNetCore.Mvc;
using CryptoTrade.Business;
using CryptoTrade.Models;

namespace CryptoTrade.API.Controllers;

[ApiController]
[Route("[controller]")]
public class UsersController : ControllerBase
{
    private readonly IUserService _userService;
    private readonly IAuthService _authService;

    public UsersController(IUserService userService, IAuthService authService)
    {
        _userService = userService;
        _authService = authService;
    }

    [HttpGet]
    public ActionResult<IEnumerable<User>> GetAllUsers()
    {
        try 
        {
            var users = _userService.GetAllUsers();
            return Ok(users);
        }     
        catch (Exception ex)
        {
            return BadRequest($"Error al obtener todos los usuarios. {ex.Message}");
        }
    }

    [HttpGet("{userId}")]
    public IActionResult GetUser(int userId)
    {
        if (!_authService.HasAccessToResource(Convert.ToInt32(userId), null, HttpContext.User)) 
            {return Forbid(); }

        try
        {
            var user = _userService.GetUserById(userId);
            return Ok(user);
        }
        catch (KeyNotFoundException knfex)
        {
            return NotFound($"No se ha encontrado el usuario con ID: {userId}. {knfex.Message}");
        }
        catch (Exception ex)
        {
            return BadRequest($"Error al obtener el usuario con ID: {userId}. {ex.Message}");
        }
    }

    [HttpGet("by-email")]
    public IActionResult GetUserByEmail(string userEmail)
    {
        if (!_authService.HasAccessToResource(null, userEmail, HttpContext.User)) 
            {return Forbid(); }

        try
        {
            var user = _userService.GetUserByEmail(userEmail);
            return Ok(user);
        }
        catch (KeyNotFoundException knfex)
        {
            return NotFound($"No se ha encontrado el usuario con email: {userEmail}. {knfex.Message}");
        }
        catch (Exception ex)
        {
            return BadRequest($"Error al obtener el usuario con email: {userEmail}. {ex.Message}");
        }
    }

    [HttpPut("{userId}")]
    public IActionResult UpdateUser(int userId, UserUpdateDTO userUpdateDTO)
    {
        if (!ModelState.IsValid)  {return BadRequest(ModelState); } 

        if (!_authService.HasAccessToResource(Convert.ToInt32(userId), null, HttpContext.User)) 
            {return Forbid(); }

        try 
        {
            _userService.UpdateUser(userId, userUpdateDTO);
            return Ok("Usuario actualizado correctamente.");
        }     
        catch (KeyNotFoundException knfex)
        {
            return NotFound($"No se ha encontrado el usuario con ID: {userId}. {knfex.Message}");
        }
        catch (Exception ex)
        {
            return BadRequest($"Error al actualizar el usuario con ID: {userId}. {ex.Message}");
        }
    }

    [HttpDelete("{userId}")]
    public IActionResult DeleteUser(int userId)
    {
        if (!_authService.HasAccessToResource(Convert.ToInt32(userId), null, HttpContext.User)) 
            {return Forbid(); }

        try
        {
            _userService.DeleteUser(userId);
            return Ok("Usuario eliminado correctamente.");
        }
        catch (KeyNotFoundException knfex)
        {
            return NotFound($"No se ha encontrado el usuario con ID: {userId}. {knfex.Message}");
        }
        catch (Exception ex)
        {
            return BadRequest($"Error al eliminar el usuario con ID: {userId}. {ex.Message}");
        }
    }

}
