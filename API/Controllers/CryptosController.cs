using Microsoft.AspNetCore.Mvc;
using CryptoTrade.Business;
using CryptoTrade.Models;

namespace CryptoTrade.API.Controllers;

[ApiController]
[Route("[controller]")]
public class CryptosController : ControllerBase
{
    private readonly ICryptoService _cryptoService;

    public CryptosController(ICryptoService cryptoService)
    {
        _cryptoService = cryptoService;
    }

    [HttpGet(Name = "GetAllCryptos")] 
    public ActionResult<IEnumerable<Crypto>> GetAllCryptos()
    {
        try 
        {
            var cryptos = _cryptoService.GetAllCryptos();
            return Ok(cryptos);
        }
        catch (Exception ex)
        {
            return BadRequest(ex);
        }
    }

    [HttpGet("{cryptoId}")]
    public IActionResult GetCrypto(int cryptoId)
    {
        try
        {
            var crypto = _cryptoService.GetCryptoById(cryptoId);
            return Ok(crypto);
        }
        catch (KeyNotFoundException knfex)
        {
           return NotFound($"No se ha encontrado la criptomoneda con ID: {cryptoId}. {knfex.Message}");
        }
        catch (Exception ex)
        {
            return BadRequest($"Error al obtener la criptomoneda con ID: {cryptoId}. {ex.Message}");
        }
    }

}
