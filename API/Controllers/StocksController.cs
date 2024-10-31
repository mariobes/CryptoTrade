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

}
