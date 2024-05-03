using Microsoft.AspNetCore.Mvc;

using Common.Utilities;
using Facturacion.Application;

namespace Facturacion.Api;


[Route("api/[controller]")]
[ApiController]

public class SiatSincronizacionController(ICufdApplication _cufdApplication) : ControllerBase
{
     [HttpGet("VerificaComunicacion")]
    public async Task<ActionResult<Response<bool>>> GetVerifica()
    {
        // var response =  await actividadesApplication.OkComunnication();
        // return Ok(response);

        // var response =  await actividadesApplication.UpdateActividades(1);
        // return Ok(response);

        // var response = await _actividadesDocumentoSectorApplication.UpdateActividadesDocumentoSector(1);
        // return Ok(response);

        // var response = await _leyendasFacturaApplication.GetLeyendaFacturaAleatoria("521020");
        // return Ok(response);

        var response = await _cufdApplication.UpdateCufd(1);
        return Ok(response);

        // var response = await _parametrosApplication.UpdateParametros(1);
        // return Ok(response);

        // var response = await _productosServiciosApplication.UpdateProductos(1);
        // return Ok(response);

    }
}

