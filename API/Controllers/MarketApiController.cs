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

    [HttpGet("total-market-cap")]
    public async Task<IActionResult> GetTotalMarketCapApi()
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

            var totalMarketCap = marketData.GetProperty("total_market_cap").GetDouble();
            var marketCapChangePercentage = marketData.GetProperty("total_market_cap_yesterday_percentage_change").GetDouble();

            var result = new CryptoIndexDto
            {
                Name = "total-market-cap",
                Value = totalMarketCap,
                ChangePercentage = marketCapChangePercentage
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

            var mostRecentValue = mostRecent.GetProperty("value").GetDouble();
            var secondMostRecentValue = secondMostRecent.GetProperty("value").GetDouble();

            var percentageChange = (mostRecentValue - secondMostRecentValue) / secondMostRecentValue * 100;

            var result = new CryptoIndexDto
            {
                Name = "CMC100-index",
                Value = mostRecentValue,
                ChangePercentage = percentageChange
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

                var data = JsonSerializer.Deserialize<JsonElement>(body);
                var cryptosTrending = data.GetProperty("coins").EnumerateArray().Take(5).ToList();

                var (newCryptosTrending, newCryptos) = await CreateCryptoEntities(cryptosTrending);

                await _marketService.UpdateCryptoTrendingDatabase(newCryptosTrending);
                await _cryptoService.UpdateCryptosDatabase(newCryptos);
                return Ok("Criptomonedas en tendencia obtenidas y actualizadas en la base de datos con éxito.");
            }
        }
        catch (Exception ex)
        {
            return BadRequest($"Error al obtener las criptomonedas en tendencia. {ex.Message}");
        }
    }

    [HttpGet("stocks-trending")]
    public async Task<IActionResult> GetStocksTrendingApi()
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

                var data = JsonSerializer.Deserialize<JsonElement>(body);
                var stocksTrending = data.EnumerateArray().TakeLast(5).ToList();

                var (newStocksTrending, newStocks) = await CreateStockEntities(stocksTrending);

                await _marketService.UpdateStockTrendingDatabase(newStocksTrending);
                await _stockService.UpdateStocksDatabase(newStocks);
                return Ok("Acciones en tendencia obtenidas y actualizadas en la base de datos con éxito.");
            }
        }
        catch (Exception ex)
        {
            return BadRequest($"Error al obtener las acciones en tendencia. {ex.Message}");
        }
    }

    [HttpGet("stocks-gainers")]
    public async Task<IActionResult> GetStocksGainersApi()
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

                var data = JsonSerializer.Deserialize<JsonElement>(body);
                var stocksGainers = data.GetProperty("mostGainerStock").EnumerateArray().TakeLast(5).ToList();

                var (newStocksGainers, newStocks) = await CreateStockEntities(stocksGainers);

                await _marketService.UpdateStockGainerDatabase(newStocksGainers);
                await _stockService.UpdateStocksDatabase(newStocks);
                return Ok("Acciones más ganadoras obtenidas y actualizadas en la base de datos con éxito.");
            }
        }
        catch (Exception ex)
        {
            return BadRequest($"Error al obtener las acciones más ganadoras. {ex.Message}");
        }
    }

    [HttpGet("stocks-losers")]
    public async Task<IActionResult> GetStocksLosersApi()
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

                var data = JsonSerializer.Deserialize<JsonElement>(body);
                var stocksLosers = data.GetProperty("mostLoserStock").EnumerateArray().TakeLast(5).ToList();
                
                var (newStocksLosers, newStocks) = await CreateStockEntities(stocksLosers);

                await _marketService.UpdateStockLoserDatabase(newStocksLosers);
                await _stockService.UpdateStocksDatabase(newStocks);
                return Ok("Acciones más perdedoras obtenidas y actualizadas en la base de datos con éxito.");
            }
        }
        catch (Exception ex)
        {
            return BadRequest($"Error al obtener las acciones más perdedoras. {ex.Message}");
        }
    }

    [HttpGet("stocks-most-actives")]
    public async Task<IActionResult> GetStocksMostActivesApi()
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

                var data = JsonSerializer.Deserialize<JsonElement>(body);
                var stocksMostActives = data.EnumerateArray().Take(5).ToList();

                var (newStocksMostActives, newStocks) = await CreateStockEntities(stocksMostActives);

                await _marketService.UpdateStockMostActiveDatabase(newStocksMostActives);
                await _stockService.UpdateStocksDatabase(newStocks);
                return Ok("Acciones más activas obtenidas y actualizadas en la base de datos con éxito.");
            }
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

            var newCryptoMarket = new AssetMarketDto
            {
                Id = id,
                Name = name,
                Symbol = symbol,
                Image = image,
                Price = price,
                ChangePercentage = changePercentage
            };

            newCryptosMarket.Add(newCryptoMarket);

            var cryptoDetails = await GetCryptoDetails(id);

            if (cryptoDetails != null)
            {
                var marketData = cryptoDetails.Value.GetProperty("market_data");

                var marketCap = GetDoubleOrDefault(marketData.GetProperty("market_cap"), "usd");
                var fullyDilutedValuation = GetDoubleOrDefault(marketData.GetProperty("fully_diluted_valuation"), "usd");
                var totalVolume = GetDoubleOrDefault(marketData.GetProperty("total_volume"), "usd");
                var high24h = GetDoubleOrDefault(marketData.GetProperty("high_24h"), "usd");
                var low24h = GetDoubleOrDefault(marketData.GetProperty("low_24h"), "usd");
                var priceChange24h = GetDoubleOrDefault(marketData, "price_change_24h");
                var priceChangePercentage24h = GetDoubleOrDefault(marketData, "price_change_percentage_24h");
                var priceChangePercentage1h = GetDoubleOrDefault(marketData.GetProperty("price_change_percentage_1h_in_currency"), "usd");
                var priceChangePercentage7d = GetDoubleOrDefault(marketData.GetProperty("price_change_percentage_7d_in_currency"), "usd");
                var marketCapChange24h = GetDoubleOrDefault(marketData, "market_cap_change_24h");
                var marketCapChangePercentage24h = GetDoubleOrDefault(marketData, "market_cap_change_percentage_24h");
                var circulatingSupply = GetDoubleOrDefault(marketData, "circulating_supply");
                var totalSupply = GetDoubleOrDefault(marketData, "total_supply");
                var maxSupply = GetDoubleOrDefault(marketData, "max_supply");
                var allTimeHigh = GetDoubleOrDefault(marketData.GetProperty("ath"), "usd");
                var allTimeHighChangePercentage = GetDoubleOrDefault(marketData.GetProperty("ath_change_percentage"), "usd");

                var sparklineIn7d = new SparklineIn7d
                {
                    Price = marketData.TryGetProperty("sparkline_7d", out var sparkline7dElement)
                        ? sparkline7dElement.TryGetProperty("price", out var priceElement) 
                            ? priceElement.EnumerateArray().Select(p => p.GetDouble()).ToList() 
                            : new List<double>() 
                        : new List<double>()
                };

                var newCrypto = new Crypto
                {
                    Id = id,
                    Name = name,
                    Symbol = symbol,
                    Image = image,
                    Price = price,
                    MarketCap = marketCap,
                    FullyDilutedValuation = fullyDilutedValuation,
                    TotalVolume = totalVolume,
                    High24h = high24h,
                    Low24h = low24h,
                    PriceChange24h = priceChange24h,
                    PriceChangePercentage24h = priceChangePercentage24h,
                    PriceChangePercentage1h = priceChangePercentage1h,
                    PriceChangePercentage7d = priceChangePercentage7d,
                    MarketCapChange24h = marketCapChange24h,
                    MarketCapChangePercentage24h = marketCapChangePercentage24h,
                    CirculatingSupply = circulatingSupply,
                    TotalSupply = totalSupply,
                    MaxSupply = maxSupply,
                    AllTimeHigh = allTimeHigh,
                    AllTimeHighChangePercentage = allTimeHighChangePercentage,
                    SparklineIn7d = sparklineIn7d
                };

                newCryptos.Add(newCrypto);
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

            var newStockMarket= new AssetMarketDto
            {
                Id = id,
                Name = name,
                Symbol = symbol,
                Image = image,
                Price = price,
                ChangePercentage = changePercentage
            };

            newStocksMarket.Add(newStockMarket);

            var newStock = new Stock
            {
                Id = id,
                Name = name,
                Symbol = symbol,
                Price = price,
                MarketCap = stockDetails?.MarketCap ?? 0,
                Sector = stockDetails?.Sector,
                Industry = stockDetails?.Industry,
                LastAnnualDividend = stockDetails?.LastAnnualDividend ?? 0,
                Volume = stockDetails?.Volume ?? 0,
                Exchange = stockDetails?.Exchange,
                ExchangeShortName = stockDetails?.ExchangeShortName,
                Country = stockDetails?.Country,
                Changes = stockDetails?.Changes ?? 0,
                ChangesPercentage = changePercentage,
                Currency = stockDetails?.Currency,
                Isin = stockDetails?.Isin,
                Website = stockDetails?.Website,
                Description = stockDetails?.Description,
                Ceo = stockDetails?.Ceo,
                Image = image,
            };

            newStocks.Add(newStock);
        }

        return (newStocksMarket, newStocks);
    }

    private double GetDoubleOrDefault(JsonElement element, string propertyName)
    {
        if (element.TryGetProperty(propertyName, out var property))
        {
            if (property.ValueKind == JsonValueKind.Number)
            {
                return property.GetDouble();
            }
            else if (property.ValueKind == JsonValueKind.String)
            {
                if (double.TryParse(property.GetString(), NumberStyles.Any, CultureInfo.InvariantCulture, out double value))
                {
                    return value;
                }
                else
                {
                    return 0;
                }
            }
        }
        return 0;
    }

    [HttpGet("crypto-details/{id}")]
    public async Task<JsonElement?> GetCryptoDetails(string id)
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
                JsonDocument doc = JsonDocument.Parse(body);
                return doc.RootElement;
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error al obtener los detalles de la criptomoneda {id}. {ex.Message}");
            return null;
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
}
