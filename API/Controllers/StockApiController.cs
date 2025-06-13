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

    // [HttpGet("coins")]
    // public async Task<IActionResult> Coins()
    // {
    //     var client = _httpClientFactory.CreateClient();
    //     var apiKey = _configuration["FMPApi:ApiKey"];

    //     var request = new HttpRequestMessage
    //     {
    //         Method = HttpMethod.Get,
    //         RequestUri = new Uri($"https://financialmodelingprep.com/api/v3/stock-screener?limit=100&apikey={apiKey}"),
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

    //             var stocks = JsonSerializer.Deserialize<List<StockApiDto>>(body);

    //             var validStocks = stocks?
    //                 .Where(s => s.Price != null && s.IsEtf == false && s.IsFund == false && s.IsActivelyTrading == true)
    //                 .OrderByDescending(s => s.MarketCap)
    //                 .ToList();

    //             return Ok(validStocks);
    //         }
    //     }
    //     catch (Exception ex)
    //     {
    //         return BadRequest($"Error al obtener las acciones de Financial Modeling Prep API. {ex.Message}");
    //     }
    // }

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
    public async Task<IActionResult> GetStockCharts(string symbol, [FromQuery] string time)
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

                var data = JsonSerializer.Deserialize<List<StockChartDto>>(body, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });

                var prices = new List<List<object>>();
                var volumes = new List<List<object>>();

                if (time == "1min")
                {
                    var groupedByDate = data
                        .Where(d => DateTime.TryParse(d.Date, out _))
                        .GroupBy(d => DateTime.Parse(d.Date).Date)
                        .OrderByDescending(g => g.Key)
                        .FirstOrDefault();

                    if (groupedByDate != null)
                    {
                        foreach (var item in groupedByDate)
                        {
                            var dateTime = DateTime.Parse(item.Date);
                            var timestamp = new DateTimeOffset(dateTime).ToUnixTimeMilliseconds();

                            prices.Add(new List<object> { timestamp, item.Price });
                            volumes.Add(new List<object> { timestamp, item.Volume });
                        }
                    }
                }
                else
                {
                    foreach (var item in data)
                    {
                        if (DateTime.TryParse(item.Date, out var dateTime))
                        {
                            var timestamp = new DateTimeOffset(dateTime).ToUnixTimeMilliseconds();
                            prices.Add(new List<object> { timestamp, item.Price });
                            volumes.Add(new List<object> { timestamp, item.Volume });
                        }
                    }
                }

                var result = new
                {
                    prices,
                    volumes
                };

                return Ok(result);
            }
        }
        catch (Exception ex)
        {
            return BadRequest($"Error al obtener las gráficas de la acción {symbol}. {ex.Message}");
        }
    }
}
