using Microsoft.AspNetCore.Mvc;
using CryptoTrade.Business;
using CryptoTrade.Models;
using Microsoft.AspNetCore.Authorization;

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

    [Authorize(Roles = Roles.Admin)]
    [HttpPost]
    public IActionResult CreateStock([FromBody] StockCreateUpdateDto dto)
    {
        if (!ModelState.IsValid)  {return BadRequest(ModelState); } 

        try 
        {
            var stock = _stockService.RegisterStock(dto);
            return CreatedAtAction(nameof(GetStock), new { stockId = stock.Id }, stock);
        }     
        catch (Exception ex)
        {
            return BadRequest($"Error al registrar la acción. {ex.Message}");
        }
    }

    [HttpGet]
    public ActionResult<IEnumerable<Stock>> GetAllStocks([FromQuery] StockQueryParameters dto)
    {
        try 
        {
            var stocks = _stockService.GetAllStocks(dto);
            return Ok(stocks);
        }
        catch (Exception ex)
        {
            return BadRequest($"Error al obtener todas las acciones. {ex.Message}");
        }
    }

    [HttpGet("{stockId}")]
    public IActionResult GetStock(string stockId)
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

    [Authorize(Roles = Roles.Admin)]
    [HttpPut("{stockId}")]
    public IActionResult UpdateStock(string stockId, StockCreateUpdateDto dto)
    {
        if (!ModelState.IsValid)  {return BadRequest(ModelState); } 

        try 
        {
            _stockService.UpdateStock(stockId, dto);
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

    [Authorize(Roles = Roles.Admin)]
    [HttpDelete("{stockId}")]
    public IActionResult DeleteStock(string stockId)
    {
        try
        {
            _stockService.DeleteStock(stockId);
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

    [HttpGet("search-stock")]
    public IActionResult SearchStock(string query)
    {
        try
        {
            List<Stock> stocks = _stockService.SearchStock(query);
            return Ok(stocks);
        }
        catch (KeyNotFoundException knfex)
        {
           return NotFound($"No se encontraron acciones que coincidan con la búsqueda: {query}. {knfex.Message}");
        }
        catch (Exception ex)
        {
            return BadRequest($"Error al buscar acciones con la búsqueda: {query}. {ex.Message}");
        }
    }
}