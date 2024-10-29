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

    [HttpPost]
    public IActionResult CreateCrypto([FromBody] CryptoCreateUpdateDTO cryptoCreateUpdateDTO)
    {
        if (!ModelState.IsValid)  {return BadRequest(ModelState); } 

        try {
            var crypto = _cryptoService.RegisterCrypto(cryptoCreateUpdateDTO);
            return CreatedAtAction(nameof(GetCrypto), new { cryptoId = crypto.Id }, crypto);
        }     
        catch (Exception ex)
        {
            return BadRequest($"Error al registrar la criptomoneda. {ex.Message}");
        }
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
