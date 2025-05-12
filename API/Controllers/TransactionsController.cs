using Microsoft.AspNetCore.Mvc;
using CryptoTrade.Business;
using CryptoTrade.Models;
using Microsoft.AspNetCore.Authorization;

namespace CryptoTrade.API.Controllers;

[ApiController]
[Route("[controller]")]
public class TransactionsController : ControllerBase
{
    private readonly ITransactionService _transactionService;
    private readonly IAuthService _authService;

    public TransactionsController(ITransactionService transactionService, IAuthService authService)
    {
        _transactionService = transactionService;
        _authService = authService;
    }

    [Authorize(Roles = Roles.Admin + "," + Roles.User)]
    [HttpGet("{userId}")]
    public ActionResult<IEnumerable<Transaction>> GetAllTransactions(int userId)
    {
        if (!_authService.HasAccessToResource(Convert.ToInt32(userId), null, HttpContext.User)) 
            {return Forbid(); }

        try 
        {
            var transactions = _transactionService.GetAllTransactions(userId);
            return Ok(transactions);
        }   
        catch (KeyNotFoundException knfex)
        {
            return NotFound($"No se ha encontrado el usuario con ID: {userId}. {knfex.Message}");
        }  
        catch (Exception ex)
        {
            return BadRequest($"Error al obtener todas las transacciones del usuario con ID {userId}. {ex.Message}");
        }
    }

    [Authorize(Roles = Roles.Admin + "," + Roles.User)]
    [HttpPost("deposit")]
    public IActionResult MakeDeposit([FromBody] DepositDto dto)
    {
        if (!_authService.HasAccessToResource(Convert.ToInt32(dto.UserId), null, HttpContext.User)) 
            {return Forbid(); }

        try 
        {
            _transactionService.MakeDeposit(dto);
            return Ok("Depósito realizado correctamente.");
        }     
        catch (KeyNotFoundException knfex)
        {
            return NotFound($"No se ha encontrado el usuario con ID: {dto.UserId}. {knfex.Message}");
        }
        catch (Exception ex)
        {
            return BadRequest($"Error al realizar el depósito del usuario con ID: {dto.UserId}. {ex.Message}");
        }
    }

    [Authorize(Roles = Roles.Admin + "," + Roles.User)]
    [HttpPost("withdrawal")]
    public IActionResult MakeWithdrawal([FromBody] WithdrawalDto dto)
    {
        if (!_authService.HasAccessToResource(Convert.ToInt32(dto.UserId), null, HttpContext.User)) 
            {return Forbid(); }

        try 
        {
            _transactionService.MakeWithdrawal(dto);
            return Ok("Retiro realizado correctamente.");
        }     
        catch (KeyNotFoundException knfex)
        {
            return NotFound($"No se ha encontrado el usuario con ID: {dto.UserId}. {knfex.Message}");
        }
        catch (Exception ex)
        {
            return BadRequest($"Error al hacer el retiro del usuario con ID: {dto.UserId}. {ex.Message}");
        }
    }

    [Authorize(Roles = Roles.Admin + "," + Roles.User)]
    [HttpPost("buy-crypto")]
    public IActionResult BuyCrypto([FromBody] BuySellAssetDto dto)
    {
        if (!_authService.HasAccessToResource(Convert.ToInt32(dto.UserId), null, HttpContext.User)) 
            {return Forbid(); }

        try 
        {
            _transactionService.BuyCrypto(dto);
            return Ok("Compra realizada correctamente.");
        }     
        catch (KeyNotFoundException knfex)
        {
            return NotFound($"No se ha encontrado la criptomoneda con ID: {dto.AssetId}. {knfex.Message}");
        }
        catch (Exception ex)
        {
            return BadRequest($"Error al hacer la compra del usuario con ID: {dto.UserId}. {ex.Message}");
        }
    }

    [Authorize(Roles = Roles.Admin + "," + Roles.User)]
    [HttpPost("sell-crypto")]
    public IActionResult SellCrypto([FromBody] BuySellAssetDto dto)
    {
        if (!_authService.HasAccessToResource(Convert.ToInt32(dto.UserId), null, HttpContext.User)) 
            {return Forbid(); }

        try 
        {
            _transactionService.SellCrypto(dto);
            return Ok("Venta realizada correctamente.");
        }     
        catch (KeyNotFoundException knfex)
        {
            return NotFound($"No se ha encontrado la criptomoneda con ID: {dto.AssetId}. {knfex.Message}");
        }
        catch (Exception ex)
        {
            return BadRequest($"Error al hacer la venta del usuario con ID: {dto.UserId}. {ex.Message}");
        }
    }

    [Authorize(Roles = Roles.Admin + "," + Roles.User)]
    [HttpPost("buy-stock")]
    public IActionResult BuyStock([FromBody] BuySellAssetDto dto)
    {
        if (!_authService.HasAccessToResource(Convert.ToInt32(dto.UserId), null, HttpContext.User)) 
            {return Forbid(); }

        try 
        {
            _transactionService.BuyStock(dto);
            return Ok("Compra realizada correctamente.");
        }     
        catch (KeyNotFoundException knfex)
        {
            return NotFound($"No se ha encontrado la acción con ID: {dto.AssetId}. {knfex.Message}");
        }
        catch (Exception ex)
        {
            return BadRequest($"Error al hacer la compra del usuario con ID: {dto.UserId}. {ex.Message}");
        }
    }

    [Authorize(Roles = Roles.Admin + "," + Roles.User)]
    [HttpPost("sell-stock")]
    public IActionResult SellStock([FromBody] BuySellAssetDto dto)
    {
        if (!_authService.HasAccessToResource(Convert.ToInt32(dto.UserId), null, HttpContext.User)) 
            {return Forbid(); }

        try 
        {
            _transactionService.SellStock(dto);
            return Ok("Venta realizada correctamente.");
        }     
        catch (KeyNotFoundException knfex)
        {
            return NotFound($"No se ha encontrado la acción con ID: {dto.AssetId}. {knfex.Message}");
        }
        catch (Exception ex)
        {
            return BadRequest($"Error al hacer la venta del usuario con ID: {dto.UserId}. {ex.Message}");
        }
    }

    [Authorize(Roles = Roles.Admin + "," + Roles.User)]
    [HttpGet("{userId}/assets")]
    public ActionResult<IEnumerable<UserAssetsSummaryDto>> GetAssets(int userId, [FromQuery] string? typeAsset = null, string? assetId = null)
    {
        if (!_authService.HasAccessToResource(Convert.ToInt32(userId), null, HttpContext.User)) 
            {return Forbid(); }

        try 
        {
            var userCryptos = _transactionService.MyAssets(userId, typeAsset, assetId);
            return Ok(userCryptos);
        }     
        catch (KeyNotFoundException knfex)
        {
            return NotFound($"No se ha encontrado el usuario con ID: {userId}. {knfex.Message}");
        }  
        catch (Exception ex)
        {
            return BadRequest($"Error al obtener los activos del usuario {userId}. {ex.Message}");
        }
    }
}