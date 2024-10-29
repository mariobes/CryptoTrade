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
    public ActionResult<IEnumerable<Crypto>> GetCryptos()
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

}
