using Microsoft.AspNetCore.Mvc;

using Common.Utilities;
using Facturacion.Application;

namespace Facturacion.Api;


[Route("api/[controller]")]
[ApiController]

public class SiatSincronizacionController: ControllerBase
{
    private readonly ISincronizacionApplication _sincronizacionApplication;

    public SiatSincronizacionController(ISincronizacionApplication sincronizacionApplication)
    {
        _sincronizacionApplication = sincronizacionApplication;
    }

    [HttpGet("VerificaComunicacion")]
    public async Task<ActionResult<Response<bool>>> GetVerifica()
    {
        var response =  await _sincronizacionApplication.OkComunnication();
        //return Ok(SiatSincronizacionBLL.VerificaComunicacion());
        return Ok(response);
    }
}

