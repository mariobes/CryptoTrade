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

    private async Task<string> CallFMPApiAsync(string urlTemplate, params object[] args)
    {
        try
        {
            var client = _httpClientFactory.CreateClient();

            var apiKey = _configuration["FMPApi:ApiKey"];

            var finalArgs = args?.Append(apiKey).ToArray() ?? new object[] { apiKey };

            var url = string.Format(urlTemplate, finalArgs);

            var request = new HttpRequestMessage(HttpMethod.Get, url);
            request.Headers.Add("accept", "application/json");

            using var response = await client.SendAsync(request);
            response.EnsureSuccessStatusCode();

            return await response.Content.ReadAsStringAsync();
        }
        catch (Exception ex)
        {
            return $"Error al llamar a Financial Modeling Prep API: {ex.Message}";
        }
    }

    // [HttpGet("coins")]
    // public async Task<IActionResult> Coins()
    // {
    //     try
    //     {
    //         var urlTemplate = _configuration["Endpoints:Stock:GetStocks"];
    //         var body = await CallFMPApiAsync(urlTemplate);

    //         var stocks = JsonSerializer.Deserialize<List<StockApiDto>>(body);
    //         if (stocks == null || !stocks.Any())
    //             return BadRequest("Error al deserializar las acciones.");

    //         var validStocks = stocks
    //             .Where(s => s.Price != null && s.IsEtf == false && s.IsFund == false && s.IsActivelyTrading == true)
    //             .OrderByDescending(s => s.MarketCap)
    //             .Take(50)
    //             .ToList();

    //         return Ok(validStocks);
    //     }
    //     catch (Exception ex)
    //     {
    //         return BadRequest($"Error al obtener las acciones de Financial Modeling Prep API. {ex.Message}");
    //     }
    // }

    [HttpGet("stocks")]
    public async Task<IActionResult> GetStocksApi()
    {
        try
        {
            var url = _configuration["Endpoints:Stock:GetStocks"];
            var body = await CallFMPApiAsync(url);

            var stocks = JsonSerializer.Deserialize<List<StockApiDto>>(body);
            if (stocks == null || !stocks.Any())
                return BadRequest("Error al deserializar.");

            var validStocks = stocks
                .Where(s => s.Price != null && s.IsEtf == false && s.IsFund == false && s.IsActivelyTrading == true)
                .OrderByDescending(s => s.MarketCap)
                .Take(50)
                .ToList();

            var resultStocks = new List<Stock>();

            foreach (var stock in validStocks)
            {
                var detailsBody = await CallFMPApiAsync(
                    _configuration["Endpoints:Stock:GetStockDetails"], 
                    stock.Symbol);

                var stockDetails = JsonSerializer.Deserialize<List<Stock>>(detailsBody)?.FirstOrDefault();
                if (stockDetails != null)
                    resultStocks.Add(stockDetails);
            }

            // PROBAR ESTO EN VEZ DEL FOREACH (Debería ir más rápido)
            // var tasks = validStocks.Select(stock => CallFMPApiAsync(
            //     _configuration["Endpoints:Stock:GetStockDetails"], stock.Symbol
            // )).ToList();

            // var results = await Task.WhenAll(tasks);

            // var resultStocks = results
            //     .Select(r => JsonSerializer.Deserialize<List<Stock>>(r)?.FirstOrDefault())
            //     .Where(s => s != null)
            //     .ToList();

            await _stockService.UpdateStocksDatabase(resultStocks);
            return Ok("Acciones obtenidas y actualizadas en la base de datos con éxito.");
        }
        catch (Exception ex)
        {
            return BadRequest($"Error al obtener las acciones de Financial Modeling Prep API. {ex.Message}");
        }
    }

    [HttpGet("stock-details/{symbol}")]
    public async Task<IActionResult> GetStockDetails(string symbol)
    {
        try
        {
            var url = _configuration["Endpoints:Stock:GetStockDetails"];
            var body = await CallFMPApiAsync(url, symbol);
            return Ok(body);
        }
        catch (Exception ex)
        {
            return BadRequest($"Error al obtener los detalles de la acción con símbolo: {symbol}. {ex.Message}");
        }
    }

    [HttpGet("stock-charts/{symbol}")]
    public async Task<IActionResult> GetStockCharts(string symbol, [FromQuery] string time)
    {
        try
        {
            var url = _configuration["Endpoints:Stock:GetStockCharts"];
            var body = await CallFMPApiAsync(url, time, symbol);

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

            return Ok(new { prices, volumes });
        }
        catch (Exception ex)
        {
            return BadRequest($"Error al obtener las gráficas de la acción con símbolo: {symbol}. {ex.Message}");
        }
    }
}