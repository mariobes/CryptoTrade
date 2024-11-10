using Microsoft.AspNetCore.Mvc;
using CryptoTrade.Business;
using CryptoTrade.Models;
using Microsoft.AspNetCore.Authorization;

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

    [Authorize(Roles = Roles.Admin)]
    [HttpPost]
    public IActionResult CreateCrypto([FromBody] CryptoCreateUpdateDTO cryptoCreateUpdateDTO)
    {
        if (!ModelState.IsValid)  {return BadRequest(ModelState); } 

        try 
        {
            var crypto = _cryptoService.RegisterCrypto(cryptoCreateUpdateDTO);
            return CreatedAtAction(nameof(GetCrypto), new { cryptoId = crypto.Id }, crypto);
        }     
        catch (Exception ex)
        {
            return BadRequest($"Error al registrar la criptomoneda. {ex.Message}");
        }
    }

    [HttpGet(Name = "GetAllCryptos")] 
    public ActionResult<IEnumerable<Crypto>> GetAllCryptos([FromQuery] CryptoQueryParameters cryptoQueryParameters)
    {
        try 
        {
            var cryptos = _cryptoService.GetAllCryptos(cryptoQueryParameters);
            return Ok(cryptos);
        }
        catch (Exception ex)
        {
            return BadRequest(ex);
        }
    }

    [Authorize(Roles = Roles.Admin + "," + Roles.User)]
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

    [Authorize(Roles = Roles.Admin)]
    [HttpPut("{cryptoId}")]
    public IActionResult UpdateCrypto(int cryptoId, CryptoCreateUpdateDTO cryptoCreateUpdateDTO)
    {
        if (!ModelState.IsValid)  {return BadRequest(ModelState); } 

        try 
        {
            _cryptoService.UpdateCrypto(cryptoId, cryptoCreateUpdateDTO);
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
    public IActionResult DeleteCrypto(int cryptoId)
    {
        try
        {
            // if (!_transactionService.IsCryptoPurchased(cryptoId))
            // {
                _cryptoService.DeleteCrypto(cryptoId);
            // }
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
