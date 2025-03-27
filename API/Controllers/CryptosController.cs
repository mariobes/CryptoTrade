using Microsoft.AspNetCore.Mvc;
using CryptoTrade.Business;
using CryptoTrade.Models;
using Microsoft.AspNetCore.Authorization;
using System.Text.Json;

namespace CryptoTrade.API.Controllers;

[ApiController]
[Route("[controller]")]
public class CryptosController : ControllerBase
{
    private readonly ICryptoService _cryptoService;
    private readonly IHttpClientFactory _httpClientFactory;

    public CryptosController(ICryptoService cryptoService, IHttpClientFactory httpClientFactory)
    {
        _cryptoService = cryptoService;
        _httpClientFactory = httpClientFactory;
    }

    [Authorize(Roles = Roles.Admin)]
    [HttpPost]
    public IActionResult CreateCrypto([FromBody] CryptoCreateUpdateDTO dto)
    {
        if (!ModelState.IsValid)  {return BadRequest(ModelState); } 

        try 
        {
            var crypto = _cryptoService.RegisterCrypto(dto);
            return CreatedAtAction(nameof(GetCrypto), new { cryptoId = crypto.Id }, crypto);
        }     
        catch (Exception ex)
        {
            return BadRequest($"Error al registrar la criptomoneda. {ex.Message}");
        }
    }

    [HttpGet(Name = "GetAllCryptos")] 
    public ActionResult<IEnumerable<Crypto>> GetAllCryptos([FromQuery] CryptoQueryParameters dto)
    {
        try 
        {
            var cryptos = _cryptoService.GetAllCryptos(dto);
            return Ok(cryptos);
        }
        catch (Exception ex)
        {
            return BadRequest($"Error al obtener todas las criptomonedas. {ex.Message}");
        }
    }

    [Authorize(Roles = Roles.Admin + "," + Roles.User)]
    [HttpGet("{cryptoId}")]
    public IActionResult GetCrypto(string cryptoId)
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

    [Authorize(Roles = Roles.Admin)]
    [HttpPut("{cryptoId}")]
    public IActionResult UpdateCrypto(string cryptoId, CryptoCreateUpdateDTO dto)
    {
        if (!ModelState.IsValid)  {return BadRequest(ModelState); } 

        try 
        {
            _cryptoService.UpdateCrypto(cryptoId, dto);
            return Ok("Criptomoneda actualizada correctamente.");
        }     
        catch (KeyNotFoundException knfex)
        {
            return NotFound($"No se ha encontrado la criptomoneda con ID: {cryptoId}. {knfex.Message}");
        }
        catch (Exception ex)
        {
            return BadRequest($"Error al actualizar la criptomoneda con ID: {cryptoId}. {ex.Message}");
        }
    }

    [Authorize(Roles = Roles.Admin)]
    [HttpDelete("{cryptoId}")]
    public IActionResult DeleteCrypto(string cryptoId)
    {
        try
        {
             _cryptoService.DeleteCrypto(cryptoId);
            return Ok("Criptomoneda eliminada correctamente.");
        }
        catch (KeyNotFoundException knfex)
        {
            return NotFound($"No se ha encontrado la criptomoneda con ID: {cryptoId}. {knfex.Message}");
        }
        catch (Exception ex)
        {
            return BadRequest($"Error al eliminar la criptomoneda con ID: {cryptoId}. {ex.Message}");
        }
    }

}
