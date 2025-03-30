using Microsoft.AspNetCore.Mvc;
using CryptoTrade.Business;
using System.Text.Json;

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

    [HttpGet("stocks")]
    public async Task<IActionResult> GetStocksApi()
    {
        var client = _httpClientFactory.CreateClient();
        var apiKey = _configuration["FMPApi:ApiKey"];

        var request = new HttpRequestMessage
        {
            Method = HttpMethod.Get,
            RequestUri = new Uri($"https://financialmodelingprep.com/stable/company-screener?limit=100&apikey={apiKey}"),
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

                // var stocks = JsonSerializer.Deserialize<List<Stock>>(body);

                // // Filtramos las acciones que:
                // // - Tienen un precio no nulo
                // // - No son ETFs ni fondos
                // var validStocks = stocks?
                //     .Where(stock => stock.Price != null && stock.IsEtf == false && stock.IsFund == false && stock.IsActivelyTrading)
                //     .ToList();

                // return Ok(validStocks);
                return Ok(body);
            }
        }
        catch (Exception ex)
        {
            return BadRequest($"Error al obtener las acciones de Financial Modeling Prep API. {ex.Message}");
        }
    }

    [HttpGet("stock/{symbol}")]
    public async Task<IActionResult> GetStockDetails(string symbol)
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
                return Ok(body);
            }
        }
        catch (Exception ex)
        {
            return BadRequest($"Error al obtener los detalles de la acción {symbol}. {ex.Message}");
        }
    }

    [HttpGet("stock-charts/{time}/{symbol}")]
    public async Task<IActionResult> GetStockCharts(string time, string symbol) //1min, 1day, 1month, 1year
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

    [HttpGet("stocks-trending")]
    public async Task<IActionResult> GetStocksTrending()
    {
        var client = _httpClientFactory.CreateClient();
        var apiKey = _configuration["FMPApi:ApiKey"];

        var request = new HttpRequestMessage
        {
            Method = HttpMethod.Get,
            RequestUri = new Uri($"https://financialmodelingprep.com/api/v3/actives?apikey={apiKey}"),
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
            return BadRequest($"Error al obtener las acciones en tendencia. {ex.Message}");
        }
    }

    [HttpGet("biggest-gainers")]
    public async Task<IActionResult> GetBiggestGainers()
    {
        var client = _httpClientFactory.CreateClient();
        var apiKey = _configuration["FMPApi:ApiKey"];

        var request = new HttpRequestMessage
        {
            Method = HttpMethod.Get,
            RequestUri = new Uri($"https://financialmodelingprep.com/api/v3/stock/gainers?apikey={apiKey}"),
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
            return BadRequest($"Error al obtener las acciones más ganadoras. {ex.Message}");
        }
    }

    [HttpGet("biggest-losers")]
    public async Task<IActionResult> GetBiggestLosers()
    {
        var client = _httpClientFactory.CreateClient();
        var apiKey = _configuration["FMPApi:ApiKey"];

        var request = new HttpRequestMessage
        {
            Method = HttpMethod.Get,
            RequestUri = new Uri($"https://financialmodelingprep.com/api/v3/stock/losers?apikey={apiKey}"),
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
            return BadRequest($"Error al obtener las acciones más perdedoras. {ex.Message}");
        }
    }

}
