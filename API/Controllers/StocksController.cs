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

}
