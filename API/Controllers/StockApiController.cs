using Microsoft.AspNetCore.Mvc;
using CryptoTrade.Business;
using System.Text.Json;
using CryptoTrade.Models;

namespace CryptoTrade.API.Controllers;

[ApiController]
[Route("[controller]")]
public class StockApiController : ControllerBase
{
    private readonly IStockService _stockService;
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly IConfiguration _configuration;

    public StockApiController(IStockService stockService, IHttpClientFactory httpClientFactory, IConfiguration configuration)
    {
        _stockService = stockService;
        _httpClientFactory = httpClientFactory;
        _configuration = configuration;
    }

    [HttpGet("coins")]
    public async Task<IActionResult> Coins()
    {
        var client = _httpClientFactory.CreateClient();
        var apiKey = _configuration["FMPApi:ApiKey"];

        var request = new HttpRequestMessage
        {
            Method = HttpMethod.Get,
            RequestUri = new Uri($"https://financialmodelingprep.com/api/v3/stock-screener?limit=100&apikey={apiKey}"),
            Headers =
            {
                { "accept", "application/json" }
            },
        };

        try
        {
            using (var response = await client.SendAsync(request))
            {
                response.EnsureSuccessStatusCode();
                var body = await response.Content.ReadAsStringAsync();

                var stocks = JsonSerializer.Deserialize<List<StockApiDto>>(body);

                var validStocks = stocks?
                    .Where(s => s.Price != null && s.IsEtf == false && s.IsFund == false && s.IsActivelyTrading == true)
                    .OrderByDescending(s => s.MarketCap)
                    .ToList();

                return Ok(validStocks);
            }
        }
        catch (Exception ex)
        {
            return BadRequest($"Error al obtener las acciones de Financial Modeling Prep API. {ex.Message}");
        }
    }

    [HttpGet("stocks")]
    public async Task<IActionResult> GetStocksApi()
    {
        var client = _httpClientFactory.CreateClient();
        var apiKey = _configuration["FMPApi:ApiKey"];

        var request = new HttpRequestMessage
        {
            Method = HttpMethod.Get,
            RequestUri = new Uri($"https://financialmodelingprep.com/api/v3/stock-screener?limit=100&apikey={apiKey}"),
            Headers =
            {
                { "accept", "application/json" }
            },
        };

        try
        {
            using (var response = await client.SendAsync(request))
            {
                response.EnsureSuccessStatusCode();
                var body = await response.Content.ReadAsStringAsync();

                var stocks = JsonSerializer.Deserialize<List<StockApiDto>>(body);
                if (stocks == null || !stocks.Any())
                {
                    return BadRequest("Error al deserializar.");
                }

                var validStocks = stocks
                    .Where(s => s.Price != null && s.IsEtf == false && s.IsFund == false && s.IsActivelyTrading == true)
                    .OrderByDescending(s => s.MarketCap)
                    .Take(50)
                    .ToList();

                var resultStocks = new List<Stock>();

                foreach (var stock in validStocks)
                {
                    var stockDetails = await GetStockDetails(stock.Symbol);
                    if (stockDetails != null)
                    {
                        resultStocks.Add(stockDetails);
                    }
                }

                await _stockService.UpdateStocksDatabase(resultStocks);
                return Ok("Acciones obtenidas y actualizadas en la base de datos con éxito.");
            }
        }
        catch (Exception ex)
        {
            return BadRequest($"Error al obtener las acciones de Financial Modeling Prep API. {ex.Message}");
        }
    }

    [HttpGet("stock-details/{symbol}")]
    public async Task<Stock?> GetStockDetails(string symbol)
    {
        var client = _httpClientFactory.CreateClient();
        var apiKey = _configuration["FMPApi:ApiKey"];

        var request = new HttpRequestMessage
        {
            Method = HttpMethod.Get,
            RequestUri = new Uri($"https://financialmodelingprep.com/api/v3/profile/{symbol}?apikey={apiKey}"),
            Headers =
            {
                { "accept", "application/json" }
            },
        };

        try
        {
            using (var response = await client.SendAsync(request))
            {
                response.EnsureSuccessStatusCode();
                var body = await response.Content.ReadAsStringAsync();
                var stockDetailsList = JsonSerializer.Deserialize<List<Stock>>(body);
                return stockDetailsList?.FirstOrDefault();
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error al obtener los detalles de la acción {symbol}. {ex.Message}");
            return null;
        }
    }

    [HttpGet("stock-charts/{symbol}")]
    public async Task<IActionResult> GetStockCharts(string symbol, [FromQuery] string time) // 1/5/15/30min, 1/4hour 1day, 1month, 1year
    {
        var client = _httpClientFactory.CreateClient();
        var apiKey = _configuration["FMPApi:ApiKey"];

        var request = new HttpRequestMessage
        {
            Method = HttpMethod.Get,
            RequestUri = new Uri($"https://financialmodelingprep.com/api/v3/historical-chart/{time}/{symbol}?apikey={apiKey}"),
            Headers =
            {
                { "accept", "application/json" }
            },
        };

        try
        {
            using (var response = await client.SendAsync(request))
            {
                response.EnsureSuccessStatusCode();
                var body = await response.Content.ReadAsStringAsync();
                return Ok(body);
            }
        }
        catch (Exception ex)
        {
            return BadRequest($"Error al obtener las gráficas de la acción {symbol}. {ex.Message}");
        }
    }

    // [HttpGet("stocks-trending")]
    // public async Task<IActionResult> GetStocksTrending()
    // {
    //     var client = _httpClientFactory.CreateClient();
    //     var apiKey = _configuration["FMPApi:ApiKey"];

    //     var request = new HttpRequestMessage
    //     {
    //         Method = HttpMethod.Get,
    //         RequestUri = new Uri($"https://financialmodelingprep.com/api/v3/actives?apikey={apiKey}"),
    //         Headers =
    //         {
    //             { "accept", "application/json" }
    //         },
    //     };

    //     try
    //     {
    //         using (var response = await client.SendAsync(request))
    //         {
    //             response.EnsureSuccessStatusCode();
    //             var body = await response.Content.ReadAsStringAsync();
    //             return Ok(body);
    //         }
    //     }
    //     catch (Exception ex)
    //     {
    //         return BadRequest($"Error al obtener las acciones en tendencia. {ex.Message}");
    //     }
    // }

    // [HttpGet("stocks-gainers")]
    // public async Task<IActionResult> GetStocksGainers()
    // {
    //     var client = _httpClientFactory.CreateClient();
    //     var apiKey = _configuration["FMPApi:ApiKey"];

    //     var request = new HttpRequestMessage
    //     {
    //         Method = HttpMethod.Get,
    //         RequestUri = new Uri($"https://financialmodelingprep.com/api/v3/stock/gainers?apikey={apiKey}"),
    //         Headers =
    //         {
    //             { "accept", "application/json" }
    //         },
    //     };

    //     try
    //     {
    //         using (var response = await client.SendAsync(request))
    //         {
    //             response.EnsureSuccessStatusCode();
    //             var body = await response.Content.ReadAsStringAsync();
    //             return Ok(body);
    //         }
    //     }
    //     catch (Exception ex)
    //     {
    //         return BadRequest($"Error al obtener las acciones más ganadoras. {ex.Message}");
    //     }
    // }

    // [HttpGet("stocks-losers")]
    // public async Task<IActionResult> GetStocksLosers()
    // {
    //     var client = _httpClientFactory.CreateClient();
    //     var apiKey = _configuration["FMPApi:ApiKey"];

    //     var request = new HttpRequestMessage
    //     {
    //         Method = HttpMethod.Get,
    //         RequestUri = new Uri($"https://financialmodelingprep.com/api/v3/stock/losers?apikey={apiKey}"),
    //         Headers =
    //         {
    //             { "accept", "application/json" }
    //         },
    //     };

    //     try
    //     {
    //         using (var response = await client.SendAsync(request))
    //         {
    //             response.EnsureSuccessStatusCode();
    //             var body = await response.Content.ReadAsStringAsync();
    //             return Ok(body);
    //         }
    //     }
    //     catch (Exception ex)
    //     {
    //         return BadRequest($"Error al obtener las acciones más perdedoras. {ex.Message}");
    //     }
    // }

    // [HttpGet("stocks-most-actives")]
    // public async Task<IActionResult> GetStocksMostActives()
    // {
    //     var client = _httpClientFactory.CreateClient();
    //     var apiKey = _configuration["FMPApi:ApiKey"];

    //     var request = new HttpRequestMessage
    //     {
    //         Method = HttpMethod.Get,
    //         RequestUri = new Uri($"https://financialmodelingprep.com/api/v3/actives?apikey={apiKey}"),
    //         Headers =
    //         {
    //             { "accept", "application/json" }
    //         },
    //     };

    //     try
    //     {
    //         using (var response = await client.SendAsync(request))
    //         {
    //             response.EnsureSuccessStatusCode();
    //             var body = await response.Content.ReadAsStringAsync();
    //             return Ok(body);
    //         }
    //     }
    //     catch (Exception ex)
    //     {
    //         return BadRequest($"Error al obtener las acciones más activas. {ex.Message}");
    //     }
    // }

}
