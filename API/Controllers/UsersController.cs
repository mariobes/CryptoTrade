using Microsoft.AspNetCore.Mvc;
using CryptoTrade.Business;
using CryptoTrade.Models;

namespace CryptoTrade.API.Controllers;

[ApiController]
[Route("[controller]")]
public class UsersController : ControllerBase
{
    private readonly IUserService _userService;

    public UsersController(IUserService userService)
    {
        _userService = userService;
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

        try {
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
