using Microsoft.AspNetCore.Mvc;
using CryptoTrade.Business;
using System.Text.Json;
using CryptoTrade.Models;

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

    // [HttpGet("coins")]
    // public async Task<IActionResult> Coins()
    // {
    //     var client = _httpClientFactory.CreateClient();

    //     var request = new HttpRequestMessage
    //     {
    //         Method = HttpMethod.Get,
    //         RequestUri = new Uri("https://api.coingecko.com/api/v3/coins/markets?vs_currency=usd&sparkline=true&price_change_percentage=1h%2C7d"),
    //         Headers =
    //         {
    //             { "accept", "application/json" },
    //             { "x-cg-demo-api-key", _configuration["CoinGekoApi:ApiKey"] },
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
    //         return BadRequest($"Error al obtener las criptomonedas de CoinGeko API. {ex.Message}");
    //     }
    // }

    [HttpGet("cryptos")]
    public async Task<IActionResult> GetCryptosApi()
    {
        var client = _httpClientFactory.CreateClient();

        var request = new HttpRequestMessage
        {
            Method = HttpMethod.Get,
            RequestUri = new Uri("https://api.coingecko.com/api/v3/coins/markets?vs_currency=usd&sparkline=true&price_change_percentage=1h%2C7d"),
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

    [HttpGet("crypto-details/{id}")]
    public async Task<IActionResult> GetCryptoDetails(string id)
    {
        var client = _httpClientFactory.CreateClient();

        var request = new HttpRequestMessage
        {
            Method = HttpMethod.Get,
            RequestUri = new Uri($"https://api.coingecko.com/api/v3/coins/{id}?sparkline=true"),
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
            Console.WriteLine($"Error al obtener los detalles de la criptomoneda {id}. {ex.Message}");
            return null;
        }
    }

    [HttpGet("crypto-charts/{id}")]
    public async Task<IActionResult> GetCryptoCharts(string id, [FromQuery] string days)
    {
        var client = _httpClientFactory.CreateClient();

        var request = new HttpRequestMessage
        {
            Method = HttpMethod.Get,
            RequestUri = new Uri($"https://api.coingecko.com/api/v3/coins/{id}/market_chart?vs_currency=usd&days={days}"),
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
            return BadRequest($"Error al obtener las gráficas de la criptomoneda {id}. {ex.Message}");
        }
    }

    [HttpGet("cryptos-gainers")]
    public async Task<IActionResult> GetCryptosGainers()
    {
        try
        {
            var cryptos = _cryptoService.GetAllCryptos().OrderByDescending(c => c.PriceChangePercentage7d).ToList();

            return Ok(cryptos);
        }
        catch (Exception ex)
        {
            return BadRequest($"Error al obtener las criptomonedas más ganadoras. {ex.Message}");
        }
    }

    [HttpGet("cryptos-losers")]
    public async Task<IActionResult> GetCryptosLosers()
    {
        try
        {
            var cryptos = _cryptoService.GetAllCryptos().OrderBy(c => c.PriceChangePercentage7d).ToList();

            return Ok(cryptos);
        }
        catch (Exception ex)
        {
            return BadRequest($"Error al obtener las criptomonedas más perdedoras. {ex.Message}");
        }
    }

}
