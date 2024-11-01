using Microsoft.AspNetCore.Mvc;
using CryptoTrade.Business;
using CryptoTrade.Models;

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

}
