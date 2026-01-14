using Microsoft.AspNetCore.Mvc;
using CryptoTrade.Business;
using System.Text.Json;
using CryptoTrade.Models;
using System.Globalization;

namespace CryptoTrade.API.Controllers;

[ApiController]
[Route("[controller]")]
public class MarketApiController : ControllerBase
{
    private readonly IMarketService _marketService;
    private readonly ICryptoService _cryptoService;
    private readonly IStockService _stockService;
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly IConfiguration _configuration;

    public MarketApiController(IMarketService marketService, ICryptoService cryptoService, IStockService stockService, IHttpClientFactory httpClientFactory, IConfiguration configuration)
    {
        _marketService = marketService;
        _cryptoService = cryptoService;
        _stockService = stockService;
        _httpClientFactory = httpClientFactory;
        _configuration = configuration;
    }

    private async Task<string> CallMarketApiAsync(string urlTemplate, string apiKeyHeader = null, params object[] args)
    {
        try
        {
            var client = _httpClientFactory.CreateClient();
            var url = args != null && args.Length > 0 ? string.Format(urlTemplate, args) : urlTemplate;

            var request = new HttpRequestMessage(HttpMethod.Get, url);
            request.Headers.Add("accept", "application/json");

            if (!string.IsNullOrEmpty(apiKeyHeader))
            {
                string apiKeyValue = apiKeyHeader switch
                {
                    "x-cg-demo-api-key" => _configuration["CoinGekoApi:ApiKey"],
                    "X-CMC_PRO_API_KEY" => _configuration["CoinMarketCapApi:ApiKey"],
                    _ => _configuration[$"{apiKeyHeader}:ApiKey"]
                };
                request.Headers.Add(apiKeyHeader, apiKeyValue);
            }

            using var response = await client.SendAsync(request);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsStringAsync();
        }
        catch (Exception ex)
        {
            return $"Error al llamar a la API de los mercados: {ex.Message}";
        }
    }

    [HttpGet("total-market-cap")]
    public async Task<IActionResult> GetTotalMarketCapApi()
    {
        try
        {
            var url = _configuration["Endpoints:Market:GetTotalMarketCap"];
            var body = await CallMarketApiAsync(url, "X-CMC_PRO_API_KEY");

            var data = JsonSerializer.Deserialize<JsonElement>(body);
            var marketData = data.GetProperty("data").GetProperty("quote").GetProperty("USD");

            var result = new CryptoIndexDto
            {
                Name = "total-market-cap",
                Value = marketData.GetProperty("total_market_cap").GetDouble(),
                ChangePercentage = marketData.GetProperty("total_market_cap_yesterday_percentage_change").GetDouble()
            };

            await _marketService.DeleteCryptoIndexDatabase();
            await _marketService.UpdateCryptoIndexDatabase(result);
            return Ok("Capitalización de mercado obtenida y actualizada en la base de datos con éxito.");
        }
        catch (Exception ex)
        {
            return BadRequest($"Error al obtener la capitalización total del mercado. {ex.Message}");
        }
    }

    [HttpGet("fear-greed-index")]
    public async Task<IActionResult> GetFearGreedIndexApi()
    {
        try
        {
            var url = _configuration["Endpoints:Market:GetFearGreedIndex"];
            var body = await CallMarketApiAsync(url, "X-CMC_PRO_API_KEY");

            var data = JsonSerializer.Deserialize<JsonElement>(body);
            var latestData = data.GetProperty("data")[0];

            var result = new CryptoIndexDto
            {
                Name = "fear-greed-index",
                Value = latestData.GetProperty("value").GetInt32(),
                Sentiment = latestData.GetProperty("value_classification").GetString()
            };

            await _marketService.UpdateCryptoIndexDatabase(result);
            return Ok("Índice de miedo y codicia obtenido y actualizado en la base de datos con éxito.");

        }
        catch (Exception ex)
        {
            return BadRequest($"Error al obtener el índice de miedo y codicia. {ex.Message}");
        }
    }

    [HttpGet("CMC100-index")]
    public async Task<IActionResult> GetCMC100IndexApi()
    {
        try
        {
            var url = _configuration["Endpoints:Market:GetCMC100Index"];
            var body = await CallMarketApiAsync(url, "X-CMC_PRO_API_KEY");

            var data = JsonSerializer.Deserialize<JsonElement>(body);
            var latestData = data.GetProperty("data").EnumerateArray().ToList();

            var mostRecentValue = latestData[^1].GetProperty("value").GetDouble();
            var previousValue = latestData[^2].GetProperty("value").GetDouble();

            var result = new CryptoIndexDto
            {
                Name = "CMC100-index",
                Value = mostRecentValue,
                ChangePercentage = (mostRecentValue - previousValue) / previousValue * 100
            };

            await _marketService.UpdateCryptoIndexDatabase(result);
            return Ok("Índice CMC100 obtenido y actualizado en la base de datos con éxito.");
        }
        catch (Exception ex)
        {
            return BadRequest($"Error al obtener el índice CMC100. {ex.Message}");
        }
    }

    [HttpGet("cryptos-trending")]
    public async Task<IActionResult> GetCryptosTrendingApi()
    {
        try
        {
            var url = _configuration["Endpoints:Market:GetCryptosTrending"];
            var body = await CallMarketApiAsync(url, "x-cg-demo-api-key");

            var data = JsonSerializer.Deserialize<JsonElement>(body);
            var cryptosTrending = data.GetProperty("coins").EnumerateArray().Take(5).ToList();

            var (newCryptosTrending, newCryptos) = await CreateCryptoEntities(cryptosTrending);

            await _marketService.UpdateCryptoTrendingDatabase(newCryptosTrending);
            await _cryptoService.UpdateCryptosDatabase(newCryptos);
            return Ok("Criptomonedas en tendencia obtenidas y actualizadas en la base de datos con éxito.");
        }
        catch (Exception ex)
        {
            return BadRequest($"Error al obtener las criptomonedas en tendencia. {ex.Message}");
        }
    }

    [HttpGet("stocks-trending")]
    public async Task<IActionResult> GetStocksTrendingApi()
    {
        try
        {
            var url = _configuration["Endpoints:Market:GetStocksTrending"];
            var body = await CallMarketApiAsync(url, null, _configuration["FMPApi:ApiKey"]);

            var data = JsonSerializer.Deserialize<JsonElement>(body);
            var stocksTrending = data.EnumerateArray().TakeLast(5).ToList();

            var (newStocksTrending, newStocks) = await CreateStockEntities(stocksTrending);

            await _marketService.UpdateStockTrendingDatabase(newStocksTrending);
            await _stockService.UpdateStocksDatabase(newStocks);
            return Ok("Acciones en tendencia obtenidas y actualizadas en la base de datos con éxito.");
        }
        catch (Exception ex)
        {
            return BadRequest($"Error al obtener las acciones en tendencia. {ex.Message}");
        }
    }

    [HttpGet("stocks-gainers")]
    public async Task<IActionResult> GetStocksGainersApi()
    {
        try
        {
            var url = _configuration["Endpoints:Market:GetStocksGainers"];
            var body = await CallMarketApiAsync(url, null, _configuration["FMPApi:ApiKey"]);

            var data = JsonSerializer.Deserialize<JsonElement>(body);
            var stocksGainers = data.GetProperty("mostGainerStock").EnumerateArray().TakeLast(5).ToList();

            var (newStocksGainers, newStocks) = await CreateStockEntities(stocksGainers);

            await _marketService.UpdateStockGainerDatabase(newStocksGainers);
            await _stockService.UpdateStocksDatabase(newStocks);
            return Ok("Acciones más ganadoras obtenidas y actualizadas en la base de datos con éxito.");
        }
        catch (Exception ex)
        {
            return BadRequest($"Error al obtener las acciones más ganadoras. {ex.Message}");
        }
    }

    [HttpGet("stocks-losers")]
    public async Task<IActionResult> GetStocksLosersApi()
    {
        try
        {
            var url = _configuration["Endpoints:Market:GetStocksLosers"];
            var body = await CallMarketApiAsync(url, null, _configuration["FMPApi:ApiKey"]);

            var data = JsonSerializer.Deserialize<JsonElement>(body);
            var stocksLosers = data.GetProperty("mostLoserStock").EnumerateArray().TakeLast(5).ToList();

            var (newStocksLosers, newStocks) = await CreateStockEntities(stocksLosers);

            await _marketService.UpdateStockLoserDatabase(newStocksLosers);
            await _stockService.UpdateStocksDatabase(newStocks);
            return Ok("Acciones más perdedoras obtenidas y actualizadas en la base de datos con éxito.");  
        }
        catch (Exception ex)
        {
            return BadRequest($"Error al obtener las acciones más perdedoras. {ex.Message}");
        }
    }

    [HttpGet("stocks-most-actives")]
    public async Task<IActionResult> GetStocksMostActivesApi()
    {
        try
        {
            var url = _configuration["Endpoints:Market:GetStocksMostActives"];
            var body = await CallMarketApiAsync(url, null, _configuration["FMPApi:ApiKey"]);

            var data = JsonSerializer.Deserialize<JsonElement>(body);
            var stocksMostActives = data.EnumerateArray().Take(5).ToList();

            var (newStocksMostActives, newStocks) = await CreateStockEntities(stocksMostActives);

            await _marketService.UpdateStockMostActiveDatabase(newStocksMostActives);
            await _stockService.UpdateStocksDatabase(newStocks);
            return Ok("Acciones más activas obtenidas y actualizadas en la base de datos con éxito.");
        }
        catch (Exception ex)
        {
            return BadRequest($"Error al obtener las acciones más activas. {ex.Message}");
        }
    }

    private async Task<(List<AssetMarketDto> CryptosMarket, List<Crypto> Cryptos)> CreateCryptoEntities(List<JsonElement> cryptos)
    {
        var newCryptosMarket = new List<AssetMarketDto>();
        var newCryptos = new List<Crypto>();

        foreach (var crypto in cryptos)
        {
            var item = crypto.GetProperty("item");
            var id = item.GetProperty("id").GetString();
            var name = item.GetProperty("name").GetString();
            var symbol = item.GetProperty("symbol").GetString();
            var image = item.GetProperty("thumb").GetString();
            var price = GetDoubleOrDefault(item.GetProperty("data"), "price");
            var changePercentage = GetDoubleOrDefault(item.GetProperty("data").GetProperty("price_change_percentage_24h"), "usd");

            newCryptosMarket.Add(new AssetMarketDto
            {
                Id = id,
                Name = name,
                Symbol = symbol,
                Image = image,
                Price = price,
                ChangePercentage = changePercentage
            });

            var cryptoDetails = await GetCryptoDetails(id);
            if (cryptoDetails != null)
            {
                var marketData = cryptoDetails.Value.GetProperty("market_data");

                var sparklineIn7d = new SparklineIn7d
                {
                    Price = marketData.TryGetProperty("sparkline_7d", out var sparkline7dElement) &&
                            sparkline7dElement.TryGetProperty("price", out var priceElement)
                            ? priceElement.EnumerateArray().Select(p => p.GetDouble()).ToList()
                            : new List<double>()
                };

                newCryptos.Add(new Crypto
                {
                    Id = id,
                    Name = name,
                    Symbol = symbol,
                    Image = image,
                    Price = price,
                    MarketCap = GetDoubleOrDefault(marketData.GetProperty("market_cap"), "usd"),
                    FullyDilutedValuation = GetDoubleOrDefault(marketData.GetProperty("fully_diluted_valuation"), "usd"),
                    TotalVolume = GetDoubleOrDefault(marketData.GetProperty("total_volume"), "usd"),
                    High24h = GetDoubleOrDefault(marketData.GetProperty("high_24h"), "usd"),
                    Low24h = GetDoubleOrDefault(marketData.GetProperty("low_24h"), "usd"),
                    PriceChange24h = GetDoubleOrDefault(marketData, "price_change_24h"),
                    PriceChangePercentage24h = GetDoubleOrDefault(marketData, "price_change_percentage_24h"),
                    PriceChangePercentage1h = GetDoubleOrDefault(marketData.GetProperty("price_change_percentage_1h_in_currency"), "usd"),
                    PriceChangePercentage7d = GetDoubleOrDefault(marketData.GetProperty("price_change_percentage_7d_in_currency"), "usd"),
                    MarketCapChange24h = GetDoubleOrDefault(marketData, "market_cap_change_24h"),
                    MarketCapChangePercentage24h = GetDoubleOrDefault(marketData, "market_cap_change_percentage_24h"),
                    CirculatingSupply = GetDoubleOrDefault(marketData, "circulating_supply"),
                    TotalSupply = GetDoubleOrDefault(marketData, "total_supply"),
                    MaxSupply = GetDoubleOrDefault(marketData, "max_supply"),
                    AllTimeHigh = GetDoubleOrDefault(marketData.GetProperty("ath"), "usd"),
                    AllTimeHighChangePercentage = GetDoubleOrDefault(marketData.GetProperty("ath_change_percentage"), "usd"),
                    SparklineIn7d = sparklineIn7d
                });
            }
        }

        return (newCryptosMarket, newCryptos);
    }

    private async Task<(List<AssetMarketDto> StocksMarket, List<Stock> Stocks)> CreateStockEntities(List<JsonElement> stocks)
    {
        var newStocksMarket = new List<AssetMarketDto>();
        var newStocks = new List<Stock>();

        foreach (var stock in stocks)
        {
            var id = stock.GetProperty("ticker").GetString();
            var name = stock.GetProperty("companyName").GetString();
            var symbol = stock.GetProperty("ticker").GetString();
            var price = GetDoubleOrDefault(stock, "price");
            var changePercentage = GetDoubleOrDefault(stock, "changesPercentage");
            var stockDetails = await GetStockDetails(symbol);
            var image = stockDetails?.Image ?? string.Empty;

            newStocksMarket.Add(new AssetMarketDto
            {
                Id = id,
                Name = name,
                Symbol = symbol,
                Image = image,
                Price = price,
                ChangePercentage = changePercentage
            });

            newStocks.Add(new Stock
            {
                Id = id,
                Name = name,
                Symbol = symbol,
                Price = price,
                ChangesPercentage = changePercentage,
                MarketCap = stockDetails?.MarketCap ?? 0,
                Sector = stockDetails?.Sector,
                Industry = stockDetails?.Industry,
                LastAnnualDividend = stockDetails?.LastAnnualDividend ?? 0,
                Volume = stockDetails?.Volume ?? 0,
                Exchange = stockDetails?.Exchange,
                ExchangeShortName = stockDetails?.ExchangeShortName,
                Country = stockDetails?.Country,
                Changes = stockDetails?.Changes ?? 0,
                Currency = stockDetails?.Currency,
                Isin = stockDetails?.Isin,
                Website = stockDetails?.Website,
                Description = stockDetails?.Description,
                Ceo = stockDetails?.Ceo,
                Image = image
            });
        }

        return (newStocksMarket, newStocks);
    }

    private double GetDoubleOrDefault(JsonElement element, string propertyName)
    {
        if (element.TryGetProperty(propertyName, out var property))
        {
            if (property.ValueKind == JsonValueKind.Number)
                return property.GetDouble();

            if (property.ValueKind == JsonValueKind.String &&
                double.TryParse(property.GetString(), NumberStyles.Any, CultureInfo.InvariantCulture, out double value))
                return value;
        }

        return 0;
    }

    private async Task<JsonElement?> GetCryptoDetails(string id)
    {
        var client = _httpClientFactory.CreateClient();
        var url = string.Format(_configuration["Endpoints:Crypto:GetCryptoDetails"], id);

        var request = new HttpRequestMessage(HttpMethod.Get, url);
        request.Headers.Add("accept", "application/json");
        request.Headers.Add("x-cg-demo-api-key", _configuration["CoinGekoApi:ApiKey"]);

        try
        {
            using var response = await client.SendAsync(request);
            response.EnsureSuccessStatusCode();
            var body = await response.Content.ReadAsStringAsync();
            return JsonDocument.Parse(body).RootElement;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error al obtener los detalles de la criptomoneda {id}. {ex.Message}");
            return null;
        }
    }

    private async Task<Stock?> GetStockDetails(string symbol)
    {
        var client = _httpClientFactory.CreateClient();
        var apiKey = _configuration["FMPApi:ApiKey"];
        var url = string.Format(_configuration["Endpoints:Stock:GetStockDetails"], symbol, apiKey);

        var request = new HttpRequestMessage(HttpMethod.Get, url);
        request.Headers.Add("accept", "application/json");

        try
        {
            using var response = await client.SendAsync(request);
            response.EnsureSuccessStatusCode();
            var body = await response.Content.ReadAsStringAsync();
            var stockDetailsList = JsonSerializer.Deserialize<List<Stock>>(body);
            return stockDetailsList?.FirstOrDefault();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error al obtener los detalles de la acción con símbolo: {symbol}. {ex.Message}");
            return null;
        }
    }
}