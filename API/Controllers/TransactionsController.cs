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
    public IActionResult MakeDeposit([FromBody] DepositDTO depositDTO)
    {
        if (!_authService.HasAccessToResource(Convert.ToInt32(depositDTO.UserId), null, HttpContext.User)) 
            {return Forbid(); }

        try 
        {
            _transactionService.MakeDeposit(depositDTO);
            return Ok("Depósito realizado correctamente.");
        }     
        catch (KeyNotFoundException knfex)
        {
            return NotFound($"No se ha encontrado el usuario con ID: {depositDTO.UserId}. {knfex.Message}");
        }
        catch (Exception ex)
        {
            return BadRequest($"Error al realizar el depósito del usuario con ID: {depositDTO.UserId}. {ex.Message}");
        }
    }

    [Authorize(Roles = Roles.Admin + "," + Roles.User)]
    [HttpPost("withdrawal")]
    public IActionResult MakeWithdrawal([FromBody] WithdrawalDTO withdrawalDTO)
    {
        if (!_authService.HasAccessToResource(Convert.ToInt32(withdrawalDTO.UserId), null, HttpContext.User)) 
            {return Forbid(); }

        try 
        {
            _transactionService.MakeWithdrawal(withdrawalDTO);
            return Ok("Retiro realizado correctamente.");
        }     
        catch (KeyNotFoundException knfex)
        {
            return NotFound($"No se ha encontrado el usuario con ID: {withdrawalDTO.UserId}. {knfex.Message}");
        }
        catch (Exception ex)
        {
            return BadRequest($"Error al hacer el retiro del usuario con ID: {withdrawalDTO.UserId}. {ex.Message}");
        }
    }

    [Authorize(Roles = Roles.Admin + "," + Roles.User)]
    [HttpPost("buy-crypto")]
    public IActionResult BuyCrypto([FromBody] BuySellAssetDTO buySellAssetDTO)
    {
        if (!_authService.HasAccessToResource(Convert.ToInt32(buySellAssetDTO.UserId), null, HttpContext.User)) 
            {return Forbid(); }

        try 
        {
            _transactionService.BuyCrypto(buySellAssetDTO);
            return Ok("Compra realizada correctamente.");
        }     
        catch (KeyNotFoundException knfex)
        {
            return NotFound($"No se ha encontrado la criptomoneda con ID: {buySellAssetDTO.AssetId}. {knfex.Message}");
        }
        catch (Exception ex)
        {
            return BadRequest($"Error al hacer la compra del usuario con ID: {buySellAssetDTO.UserId}. {ex.Message}");
        }
    }

    [Authorize(Roles = Roles.Admin + "," + Roles.User)]
    [HttpPost("sell-crypto")]
    public IActionResult SellCrypto([FromBody] BuySellAssetDTO buySellAssetDTO)
    {
        if (!_authService.HasAccessToResource(Convert.ToInt32(buySellAssetDTO.UserId), null, HttpContext.User)) 
            {return Forbid(); }

        try 
        {
            _transactionService.SellCrypto(buySellAssetDTO);
            return Ok("Venta realizada correctamente.");
        }     
        catch (KeyNotFoundException knfex)
        {
            return NotFound($"No se ha encontrado la criptomoneda con ID: {buySellAssetDTO.AssetId}. {knfex.Message}");
        }
        catch (Exception ex)
        {
            return BadRequest($"Error al hacer la venta del usuario con ID: {buySellAssetDTO.UserId}. {ex.Message}");
        }
    }

    [Authorize(Roles = Roles.Admin + "," + Roles.User)]
    [HttpPost("buy-stock")]
    public IActionResult BuyStock([FromBody] BuySellAssetDTO buySellAssetDTO)
    {
        if (!_authService.HasAccessToResource(Convert.ToInt32(buySellAssetDTO.UserId), null, HttpContext.User)) 
            {return Forbid(); }

        try 
        {
            _transactionService.BuyStock(buySellAssetDTO);
            return Ok("Compra realizada correctamente.");
        }     
        catch (KeyNotFoundException knfex)
        {
            return NotFound($"No se ha encontrado la acción con ID: {buySellAssetDTO.AssetId}. {knfex.Message}");
        }
        catch (Exception ex)
        {
            return BadRequest($"Error al hacer la compra del usuario con ID: {buySellAssetDTO.UserId}. {ex.Message}");
        }
    }

    [Authorize(Roles = Roles.Admin + "," + Roles.User)]
    [HttpPost("sell-stock")]
    public IActionResult SellStock([FromBody] BuySellAssetDTO buySellAssetDTO)
    {
        if (!_authService.HasAccessToResource(Convert.ToInt32(buySellAssetDTO.UserId), null, HttpContext.User)) 
            {return Forbid(); }

        try 
        {
            _transactionService.SellStock(buySellAssetDTO);
            return Ok("Venta realizada correctamente.");
        }     
        catch (KeyNotFoundException knfex)
        {
            return NotFound($"No se ha encontrado la acción con ID: {buySellAssetDTO.AssetId}. {knfex.Message}");
        }
        catch (Exception ex)
        {
            return BadRequest($"Error al hacer la venta del usuario con ID: {buySellAssetDTO.UserId}. {ex.Message}");
        }
    }

    [Authorize(Roles = Roles.Admin + "," + Roles.User)]
    [HttpPost("crypto-converter")]
    public IActionResult CryptoConverter(CryptoConverterDTO cryptoConverterDTO)
    {
        if (!_authService.HasAccessToResource(Convert.ToInt32(cryptoConverterDTO.UserId), null, HttpContext.User)) 
            {return Forbid(); }

        try 
        {
            _transactionService.CryptoConverter(cryptoConverterDTO);
            return Ok("Conversión realizada correctamente.");
        }     
        catch (KeyNotFoundException knfex)
        {
            return NotFound($"No se ha encontrado la criptomoneda con ID: {cryptoConverterDTO.CryptoId}. {knfex.Message}");
        }
        catch (Exception ex)
        {
            return BadRequest($"Error al hacer la conversión del usuario con ID: {cryptoConverterDTO.UserId}. {ex.Message}");
        }
    }

    [Authorize(Roles = Roles.Admin + "," + Roles.User)]
    [HttpGet("{userId}/cryptos")]
    public ActionResult<IEnumerable<Transaction>> GetCryptos(int userId)
    {
        if (!_authService.HasAccessToResource(Convert.ToInt32(userId), null, HttpContext.User)) 
            {return Forbid(); }

        try 
        {
            var userCryptos = _transactionService.MyCryptos(userId);
            return Ok(userCryptos);
        }     
        catch (KeyNotFoundException knfex)
        {
            return NotFound($"No se ha encontrado el usuario con ID: {userId}. {knfex.Message}");
        }  
        catch (Exception ex)
        {
            return BadRequest($"Error al obtener las criptomonedas del usuario {userId}. {ex.Message}");
        }
    }

    [Authorize(Roles = Roles.Admin + "," + Roles.User)]
    [HttpGet("{userId}/stocks")]
    public ActionResult<IEnumerable<Transaction>> GetStocks(int userId)
    {
        if (!_authService.HasAccessToResource(Convert.ToInt32(userId), null, HttpContext.User)) 
            {return Forbid(); }

        try 
        {
            var userStocks = _transactionService.MyStocks(userId);
            return Ok(userStocks);
        }     
        catch (KeyNotFoundException knfex)
        {
            return NotFound($"No se ha encontrado el usuario con ID: {userId}. {knfex.Message}");
        }  
        catch (Exception ex)
        {
            return BadRequest($"Error al obtener las acciones del usuario {userId}. {ex.Message}");
        }
    }

}