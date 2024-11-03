using Microsoft.AspNetCore.Mvc;
using CryptoTrade.Business;
using CryptoTrade.Models;

namespace CryptoTrade.API.Controllers;

[ApiController]
[Route("[controller]")]
public class StocksController : ControllerBase
{
    private readonly IStockService _stockService;

    public StocksController(IStockService stockService)
    {
        _stockService = stockService;
    }

    [HttpPost]
    public IActionResult CreateStock([FromBody] StockCreateUpdateDTO stockCreateUpdateDTO)
    {
        if (!ModelState.IsValid)  {return BadRequest(ModelState); } 

        try 
        {
            var stock = _stockService.RegisterStock(stockCreateUpdateDTO);
            return CreatedAtAction(nameof(GetStock), new { stockId = stock.Id }, stock);
        }     
        catch (Exception ex)
        {
            return BadRequest($"Error al registrar la acción. {ex.Message}");
        }
    }

    [HttpGet(Name = "GetAllStocks")] 
    public ActionResult<IEnumerable<Stock>> GetAllStocks()
    {
        try 
        {
            var stocks = _stockService.GetAllStocks();
            return Ok(stocks);
        }
        catch (Exception ex)
        {
            return BadRequest(ex);
        }
    }

    [HttpGet("{stockId}")]
    public IActionResult GetStock(int stockId)
    {
        try
        {
            var stock = _stockService.GetStockById(stockId);
            return Ok(stock);
        }
        catch (KeyNotFoundException knfex)
        {
           return NotFound($"No se ha encontrado la acción con ID: {stockId}. {knfex.Message}");
        }
        catch (Exception ex)
        {
            return BadRequest($"Error al obtener la acción con ID: {stockId}. {ex.Message}");
        }
    }

    [HttpPut("{stockId}")]
    public IActionResult UpdateStock(int stockId, StockCreateUpdateDTO stockCreateUpdateDTO)
    {
        if (!ModelState.IsValid)  {return BadRequest(ModelState); } 

        try 
        {
            _stockService.UpdateStock(stockId, stockCreateUpdateDTO);
            return Ok("Acción actualizada correctamente.");
        }     
        catch (KeyNotFoundException knfex)
        {
            return NotFound($"No se ha encontrado la acción con ID: {stockId}. {knfex.Message}");
        }
        catch (Exception ex)
        {
            return BadRequest($"Error al actualizar la acción con ID: {stockId}. {ex.Message}");
        }
    }

    [HttpDelete("{stockId}")]
    public IActionResult DeleteStock(int stockId)
    {
        try
        {
            // if (!_transactionService.IsStockPurchased(stockId))
            // {
                _stockService.DeleteStock(stockId);
            // }
            return Ok("Acción eliminada correctamente.");
        }
        catch (KeyNotFoundException knfex)
        {
            return NotFound($"No se ha encontrado la acción con ID: {stockId}. {knfex.Message}");
        }
        catch (Exception ex)
        {
            return BadRequest($"Error al eliminar la acción con ID: {stockId}. {ex.Message}");
        }
    }

}
