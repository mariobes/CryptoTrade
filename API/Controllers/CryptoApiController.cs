using Microsoft.AspNetCore.Mvc;
using CryptoTrade.Business;
using System.Text.Json;

namespace CryptoTrade.API.Controllers;

[ApiController]
[Route("[controller]")]
public class CryptoApiController : ControllerBase
{
    private readonly ICryptoService _cryptoService;
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly IConfiguration _configuration;

    public CryptoApiController(ICryptoService cryptoService, IHttpClientFactory httpClientFactory, IConfiguration configuration)
    {
        _cryptoService = cryptoService;
        _httpClientFactory = httpClientFactory;
        _configuration = configuration;
    }

    [HttpGet("coins")]
    public async Task<IActionResult> Coins()
    {
        var client = _httpClientFactory.CreateClient();

        var request = new HttpRequestMessage
        {
            Method = HttpMethod.Get,
            RequestUri = new Uri("https://api.coingecko.com/api/v3/coins/markets?vs_currency=eur&sparkline=true"),
            Headers =
            {
                { "accept", "application/json" },
                { "x-cg-demo-api-key", _configuration["CoinGekoApi:ApiKey"] },
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
            return BadRequest($"Error al obtener las criptomonedas de CoinGeko API. {ex.Message}");
        }
    }

    [HttpGet("cryptos")]
    public async Task<IActionResult> GetCryptosApi()
    {
        var client = _httpClientFactory.CreateClient();

        var request = new HttpRequestMessage
        {
            Method = HttpMethod.Get,
            RequestUri = new Uri("https://api.coingecko.com/api/v3/coins/markets?vs_currency=usd&sparkline=true"),
            Headers =
            {
                { "accept", "application/json" },
                { "x-cg-demo-api-key", _configuration["CoinGekoApi:ApiKey"] },
            },
        };

        try
        {
            using (var response = await client.SendAsync(request))
            {
                response.EnsureSuccessStatusCode();
                var body = await response.Content.ReadAsStringAsync();

                var cryptos = JsonSerializer.Deserialize<List<Crypto>>(body);
                if (cryptos == null || !cryptos.Any())
                {
                    return BadRequest("Error al deserializar.");
                }

                await _cryptoService.UpdateCryptosDatabase(cryptos);
                return Ok("Criptomonedas obtenidas y actualizadas en la base de datos con éxito.");
            }
        }
        catch (Exception ex)
        {
            return BadRequest($"Error al obtener las criptomonedas de CoinGeko API. {ex.Message}");
        }
    }

    [HttpGet("cryptos-trending")]
    public async Task<IActionResult> GetCryptosTrending()
    {
        var client = _httpClientFactory.CreateClient();

        var request = new HttpRequestMessage
        {
            Method = HttpMethod.Get,
            RequestUri = new Uri("https://api.coingecko.com/api/v3/search/trending"),
            Headers =
            {
                { "accept", "application/json" },
                { "x-cg-demo-api-key", _configuration["CoinGekoApi:ApiKey"] },
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
            return BadRequest($"Error al obtener las criptomonedas en tendencia. {ex.Message}");
        }
    }

    [HttpGet("total-market-cap")]
    public async Task<IActionResult> GetTotalMarketCap()
    {
        var request = new HttpRequestMessage
        {
            Method = HttpMethod.Get,
            RequestUri = new Uri("https://pro-api.coinmarketcap.com/v1/global-metrics/quotes/latest"),
            Headers =
            {
                { "X-CMC_PRO_API_KEY", _configuration["CoinMarketCapApi:ApiKey"] },
                { "Accept", "application/json" }
            }
        };

        try
        {
            var client = _httpClientFactory.CreateClient();
            using var response = await client.SendAsync(request);
            response.EnsureSuccessStatusCode();
            var body = await response.Content.ReadAsStringAsync();

            var data = JsonSerializer.Deserialize<JsonElement>(body);
            var marketData = data.GetProperty("data").GetProperty("quote").GetProperty("USD");

            var totalMarketCap = marketData.GetProperty("total_market_cap").GetDecimal();
            var marketCapChangePercentage = marketData.GetProperty("total_market_cap_yesterday_percentage_change").GetDecimal();

            var result = new
            {
                TotalMarketCap = totalMarketCap,
                MarketCapChangePercentage = marketCapChangePercentage
            };

            return Ok(result);
        }
        catch (Exception ex)
        {
            return BadRequest($"Error al obtener la capitalización total del mercado. {ex.Message}");
        }
    }

    [HttpGet("fear-greed-index")]
    public async Task<IActionResult> GetFearGreedIndex()
    {
        var request = new HttpRequestMessage
        {
            Method = HttpMethod.Get,
            RequestUri = new Uri("https://pro-api.coinmarketcap.com/v3/fear-and-greed/historical"),
            Headers =
            {
                { "X-CMC_PRO_API_KEY", _configuration["CoinMarketCapApi:ApiKey"] },
                { "Accept", "application/json" }
            }
        };

        try
        {
            var client = _httpClientFactory.CreateClient();
            using var response = await client.SendAsync(request);
            response.EnsureSuccessStatusCode();
            var body = await response.Content.ReadAsStringAsync();

            var data = JsonSerializer.Deserialize<JsonElement>(body);
            var latestData = data.GetProperty("data")[0];

            var result = new
            {
                Value = latestData.GetProperty("value").GetInt32(),
                ValueClassification = latestData.GetProperty("value_classification").GetString(),
            };

            return Ok(result);

        }
        catch (Exception ex)
        {
            return BadRequest($"Error al obtener el índice de miedo y codicia. {ex.Message}");
        }
    }

    [HttpGet("CMC100-index")]
    public async Task<IActionResult> GetCMC100Index()
    {
        var request = new HttpRequestMessage
        {
            Method = HttpMethod.Get,
            RequestUri = new Uri("https://pro-api.coinmarketcap.com/v3/index/cmc100-historical"),
            Headers =
            {
                { "X-CMC_PRO_API_KEY", _configuration["CoinMarketCapApi:ApiKey"] },
                { "Accept", "application/json" }
            }
        };

        try
        {
            var client = _httpClientFactory.CreateClient();
            using var response = await client.SendAsync(request);
            response.EnsureSuccessStatusCode();
            var body = await response.Content.ReadAsStringAsync();

            var data = JsonSerializer.Deserialize<JsonElement>(body);
            var latestData = data.GetProperty("data").EnumerateArray().ToList();

            var mostRecent = latestData[latestData.Count - 1];
            var secondMostRecent = latestData[latestData.Count - 2];

            var mostRecentValue = mostRecent.GetProperty("value").GetDecimal();
            var secondMostRecentValue = secondMostRecent.GetProperty("value").GetDecimal();

            var percentageChange = (mostRecentValue - secondMostRecentValue) / secondMostRecentValue * 100;

            var result = new
            {
                Value = mostRecentValue,
                PercentageChange = percentageChange,
            };

            return Ok(result);
        }
        catch (Exception ex)
        {
            return BadRequest($"Error al obtener el índice CMC100. {ex.Message}");
        }
    }

}
