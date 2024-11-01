using Microsoft.AspNetCore.Mvc;
using CryptoTrade.Business;
using CryptoTrade.Models;

namespace CryptoTrade.API.Controllers;

[ApiController]
[Route("[controller]")]
public class TransactionsController : ControllerBase
{
    private readonly ITransactionService _transactionService;

    public TransactionsController(ITransactionService transactionService)
    {
        _transactionService = transactionService;
    }

    [HttpGet("{userId}")]
    public ActionResult<IEnumerable<Transaction>> GetAllTransactions(int userId)
    {
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

    [HttpPost("deposit")]
    public IActionResult MakeDeposit([FromBody] DepositWithdrawalDTO depositWithdrawalDTO)
    {
        try {
            _transactionService.MakeDeposit(depositWithdrawalDTO);
            return Ok("Depósito realizado correctamente.");
        }     
        catch (KeyNotFoundException knfex)
        {
            return NotFound($"No se ha encontrado el usuario con ID: {depositWithdrawalDTO.UserId}. {knfex.Message}");
        }
        catch (Exception ex)
        {
            return BadRequest($"Error al realizar el depósito del usuario con ID: {depositWithdrawalDTO.UserId}. {ex.Message}");
        }
    }

    [HttpPost("withdrawal")]
    public IActionResult MakeWithdrawal([FromBody] DepositWithdrawalDTO depositWithdrawalDTO)
    {
        try {
            _transactionService.MakeWithdrawal(depositWithdrawalDTO);
            return Ok("Retiro realizado correctamente.");
        }     
        catch (KeyNotFoundException knfex)
        {
            return NotFound($"No se ha encontrado el usuario con ID: {depositWithdrawalDTO.UserId}. {knfex.Message}");
        }
        catch (Exception ex)
        {
            return BadRequest($"Error al hacer el retiro del usuario con ID: {depositWithdrawalDTO.UserId}. {ex.Message}");
        }
    }

    [HttpPost("buy-crypto")]
    public IActionResult BuyCrypto([FromBody] BuySellCrypto buySellCrypto)
    {
        try {
            _transactionService.BuyCrypto(buySellCrypto);
            return Ok("Compra realizada correctamente.");
        }     
        catch (KeyNotFoundException knfex)
        {
            return NotFound($"No se ha encontrado la criptomoneda con ID: {buySellCrypto.CryptoId}. {knfex.Message}");
        }
        catch (Exception ex)
        {
            return BadRequest($"Error al hacer la compra del usuario con ID: {buySellCrypto.UserId}. {ex.Message}");
        }
    }

    [HttpPost("sell-crypto")]
    public IActionResult SellCrypto([FromBody] BuySellCrypto buySellCrypto)
    {
        try {
            _transactionService.SellCrypto(buySellCrypto);
            return Ok("Venta realizada correctamente.");
        }     
        catch (KeyNotFoundException knfex)
        {
            return NotFound($"No se ha encontrado la criptomoneda con ID: {buySellCrypto.CryptoId}. {knfex.Message}");
        }
        catch (Exception ex)
        {
            return BadRequest($"Error al hacer la venta del usuario con ID: {buySellCrypto.UserId}. {ex.Message}");
        }
    }

}
