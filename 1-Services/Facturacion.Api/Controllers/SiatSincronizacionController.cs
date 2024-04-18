using Microsoft.AspNetCore.Mvc;

using Common.Utilities;
using Facturacion.Application;

namespace Facturacion.Api;


[Route("api/[controller]")]
[ApiController]

public class SiatSincronizacionController: ControllerBase
{
    private readonly IActividadesApplication _actividadesApplication;

    public SiatSincronizacionController(IActividadesApplication actividadesApplication)
    {
        _actividadesApplication = actividadesApplication;
    }

    [HttpGet("VerificaComunicacion")]
    public async Task<ActionResult<Response<bool>>> GetVerifica()
    {
        // var response =  await _actividadesApplication.OkComunnication();
        // return Ok(response);

        var response = await _actividadesApplication.UpdateActividades(1);
        return Ok(response);
    }
}

