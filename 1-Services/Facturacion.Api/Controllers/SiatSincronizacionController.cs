using Microsoft.AspNetCore.Mvc;

using Common.Utilities;
using Facturacion.Application;

namespace Facturacion.Api;


[Route("api/[controller]")]
[ApiController]

public class SiatSincronizacionController(IActividadesDocumentoSectorApplication actividadesApplication) : ControllerBase
{
    private readonly IActividadesDocumentoSectorApplication _actividadesApplication = actividadesApplication;

    [HttpGet("VerificaComunicacion")]
    public async Task<ActionResult<Response<bool>>> GetVerifica()
    {
        // var response =  await _actividadesApplication.OkComunnication();
        // return Ok(response);

        var response = await _actividadesApplication.UpdateActividadesDocumentoSector(1);
        return Ok(response);
    }
}

