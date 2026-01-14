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

    private async Task<string> CallCoinGekoApiAsync(string urlTemplate, params object[] args)
    {
        try
        {
            var client = _httpClientFactory.CreateClient();

            var url = args != null && args.Length > 0 ? string.Format(urlTemplate, args) : urlTemplate;

            var request = new HttpRequestMessage(HttpMethod.Get, url);
            request.Headers.Add("accept", "application/json");
            request.Headers.Add("x-cg-demo-api-key", _configuration["CoinGekoApi:ApiKey"]);

            using var response = await client.SendAsync(request);
            response.EnsureSuccessStatusCode();

            return await response.Content.ReadAsStringAsync();
        }
        catch (Exception ex)
        {
            return $"Error al llamar a CoinGecko API: {ex.Message}";
        }
    }

    // [HttpGet("coins")]
    // public async Task<IActionResult> Coins()
    // {
    //     try
    //     {
    //         var url = _configuration["Endpoints:Crypto:GetCryptos"];
    //         var body = await CallCoinGekoApiAsync(url);
    //         return Ok(body);
    //     }
    //     catch (Exception ex)
    //     {
    //         return BadRequest($"Error al obtener las criptomonedas de CoinGeko API. {ex.Message}");
    //     }
    // }

    [HttpGet("cryptos")]
    public async Task<IActionResult> GetCryptosApi()
    {
        try
        {
            var url = _configuration["Endpoints:Crypto:GetCryptos"];
            var body = await CallCoinGekoApiAsync(url);

            var cryptos = JsonSerializer.Deserialize<List<Crypto>>(body);
            if (cryptos == null || !cryptos.Any())
                return BadRequest("Error al deserializar.");

            await _cryptoService.UpdateCryptosDatabase(cryptos);
            return Ok("Criptomonedas obtenidas y actualizadas en la base de datos con éxito.");
        }
        catch (Exception ex)
        {
            return BadRequest($"Error al obtener las criptomonedas de CoinGeko API. {ex.Message}");
        }
    }

    [HttpGet("crypto-details/{id}")]
    public async Task<IActionResult> GetCryptoDetails(string id)
    {
        try
        {
            var url = _configuration["Endpoints:Crypto:GetCryptoDetails"];
            var body = await CallCoinGekoApiAsync(url, id);
            return Ok(body);
        }
        catch (Exception ex)
        {
            return BadRequest($"Error al obtener los detalles de la criptomoneda con ID: {id}. {ex.Message}");
        }
    }

    [HttpGet("crypto-charts/{id}")]
    public async Task<IActionResult> GetCryptoCharts(string id, [FromQuery] string days)
    {
        try
        {
            var url = _configuration["Endpoints:Crypto:GetCryptoCharts"];
            var body = await CallCoinGekoApiAsync(url, id, days);
            return Ok(body);
        }
        catch (Exception ex)
        {
            return BadRequest($"Error al obtener las gráficas de la criptomoneda con ID: {id}. {ex.Message}");
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