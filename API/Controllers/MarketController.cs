using Microsoft.AspNetCore.Mvc;
using CryptoTrade.Business;
using CryptoTrade.Models;

namespace CryptoTrade.API.Controllers;

[ApiController]
[Route("[controller]")]
public class MarketController : ControllerBase
{
    private readonly IMarketService _marketService;

    public MarketController(IMarketService marketService)
    {
        
        _marketService = marketService;
    }

    [HttpGet("crypto-indices")]
    public ActionResult<IEnumerable<CryptoIndex>> GetCryptoIndices()
    {
        try 
        {
            var cryptoIndices = _marketService.GetCryptoIndices();
            return Ok(cryptoIndices);
        }
        catch (Exception ex)
        {
            return BadRequest($"Error al obtener los índices de las criptomonedas. {ex.Message}");
        }
    }

    [HttpGet("cryptos-trending")]
    public ActionResult<IEnumerable<CryptoTrending>> GetCryptosTrending()
    {
        try 
        {
            var cryptosTrending = _marketService.GetCryptosTrending();
            return Ok(cryptosTrending);
        }
        catch (Exception ex)
        {
            return BadRequest($"Error al obtener las criptomonedas en tendencia. {ex.Message}");
        }
    }

    [HttpGet("stocks-trending")]
    public ActionResult<IEnumerable<StockTrending>> GetStocksTrending()
    {
        try 
        {
            var stocksTrending = _marketService.GetStocksTrending();
            return Ok(stocksTrending);
        }
        catch (Exception ex)
        {
            return BadRequest($"Error al obtener las acciones en tendencia. {ex.Message}");
        }
    }

    [HttpGet("stocks-gainers")]
    public ActionResult<IEnumerable<StockGainer>> GetStocksGainers()
    {
        try 
        {
            var stocksGainers = _marketService.GetStocksGainers();
            return Ok(stocksGainers);
        }
        catch (Exception ex)
        {
            return BadRequest($"Error al obtener las acciones más ganadoras. {ex.Message}");
        }
    }

    [HttpGet("stocks-losers")]
    public ActionResult<IEnumerable<StockLoser>> GetStocksLosers()
    {
        try 
        {
            var stocksLosers = _marketService.GetStocksLosers();
            return Ok(stocksLosers);
        }
        catch (Exception ex)
        {
            return BadRequest($"Error al obtener las acciones más perdedoras. {ex.Message}");
        }
    }

    [HttpGet("stocks-most-actives")]
    public ActionResult<IEnumerable<StockMostActive>> GetStocksMostActives()
    {
        try 
        {
            var stocksMostActives = _marketService.GetStocksMostActives();
            return Ok(stocksMostActives);
        }
        catch (Exception ex)
        {
            return BadRequest($"Error al obtener las acciones más activas. {ex.Message}");
        }
    }
}
