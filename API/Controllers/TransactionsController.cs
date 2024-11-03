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
    public IActionResult BuyCrypto([FromBody] BuySellAsset buySellAsset)
    {
        try {
            _transactionService.BuyCrypto(buySellAsset);
            return Ok("Compra realizada correctamente.");
        }     
        catch (KeyNotFoundException knfex)
        {
            return NotFound($"No se ha encontrado la criptomoneda con ID: {buySellAsset.AssetId}. {knfex.Message}");
        }
        catch (Exception ex)
        {
            return BadRequest($"Error al hacer la compra del usuario con ID: {buySellAsset.UserId}. {ex.Message}");
        }
    }

    [HttpPost("sell-crypto")]
    public IActionResult SellCrypto([FromBody] BuySellAsset buySellAsset)
    {
        try {
            _transactionService.SellCrypto(buySellAsset);
            return Ok("Venta realizada correctamente.");
        }     
        catch (KeyNotFoundException knfex)
        {
            return NotFound($"No se ha encontrado la criptomoneda con ID: {buySellAsset.AssetId}. {knfex.Message}");
        }
        catch (Exception ex)
        {
            return BadRequest($"Error al hacer la venta del usuario con ID: {buySellAsset.UserId}. {ex.Message}");
        }
    }

    [HttpPost("buy-stock")]
    public IActionResult BuyStock([FromBody] BuySellAsset buySellAsset)
    {
        try {
            _transactionService.BuyStock(buySellAsset);
            return Ok("Compra realizada correctamente.");
        }     
        catch (KeyNotFoundException knfex)
        {
            return NotFound($"No se ha encontrado la acción con ID: {buySellAsset.AssetId}. {knfex.Message}");
        }
        catch (Exception ex)
        {
            return BadRequest($"Error al hacer la compra del usuario con ID: {buySellAsset.UserId}. {ex.Message}");
        }
    }

    [HttpPost("sell-stock")]
    public IActionResult SellStock([FromBody] BuySellAsset buySellAsset)
    {
        try {
            _transactionService.SellStock(buySellAsset);
            return Ok("Venta realizada correctamente.");
        }     
        catch (KeyNotFoundException knfex)
        {
            return NotFound($"No se ha encontrado la acción con ID: {buySellAsset.AssetId}. {knfex.Message}");
        }
        catch (Exception ex)
        {
            return BadRequest($"Error al hacer la venta del usuario con ID: {buySellAsset.UserId}. {ex.Message}");
        }
    }

    [HttpPost("crypto-converter")]
    public IActionResult CryptoConverter(CryptoConverterDTO cryptoConverterDTO)
    {
        try {
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

    [HttpGet("{userId}/cryptos")]
    public ActionResult<IEnumerable<Transaction>> GetCryptos(int userId)
    {
        try {
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

    [HttpGet("{userId}/stocks")]
    public ActionResult<IEnumerable<Transaction>> GetStocks(int userId)
    {
        try {
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