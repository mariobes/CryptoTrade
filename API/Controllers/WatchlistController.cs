using Microsoft.AspNetCore.Mvc;
using CryptoTrade.Business;
using CryptoTrade.Models;
using Microsoft.AspNetCore.Authorization;

namespace CryptoTrade.API.Controllers;

[ApiController]
[Route("[controller]")]
public class WatchlistController : ControllerBase
{
    private readonly IWatchlistService _watchlistService;
    private readonly IAuthService _authService;

    public WatchlistController(IWatchlistService watchlistService, IAuthService authService)
    {
        _watchlistService = watchlistService;
        _authService = authService;
    }

    [Authorize(Roles = Roles.Admin + "," +  Roles.User)]
    [HttpPost]
    public IActionResult CreateWatchlist(WatchlistCreateDto dto)
    {
        if (!_authService.HasAccessToResource(Convert.ToInt32(dto.UserId), null, HttpContext.User)) 
            {return Forbid(); }

        try
        {
            var watchlist = _watchlistService.RegisterWatchlist(dto);
            return Ok(watchlist);
        }
        catch (Exception ex)
        {
            return BadRequest($"Error al crear la lista de seguimiento. {ex.Message}");
        }
    }

    [Authorize(Roles = Roles.Admin + "," +  Roles.User)]
    [HttpDelete]
    public IActionResult DeleteWatchlist(WatchlistCreateDto dto)
    {
        if (!_authService.HasAccessToResource(Convert.ToInt32(dto.UserId), null, HttpContext.User)) 
            {return Forbid(); }

        try
        {
            _watchlistService.DeleteWatchlist(dto);
            return Ok("Lista de seguimiento eliminada correctamente.");
        }
        catch (Exception ex)
        {
            return BadRequest($"Error al eliminar la lista de seguimiento con ID: {dto.Id}. {ex.Message}");
        }
    }

    [Authorize(Roles = Roles.Admin + "," +  Roles.User)]
    [HttpGet]
    public IActionResult GetAllWatchlists(int userId, string typeAsset)
    {
        if (!_authService.HasAccessToResource(Convert.ToInt32(userId), null, HttpContext.User)) 
            {return Forbid(); }

        try
        {
            var userWatchlists = _watchlistService.GetAllWatchlists(userId, typeAsset);
            return Ok(userWatchlists);
        }
        catch (Exception ex)
        {
            return BadRequest($"Error al eliminar la lista de seguimiento con ID: {userId}. {ex.Message}");
        }
    }

}
