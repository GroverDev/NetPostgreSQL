using Microsoft.AspNetCore.Mvc;

using Common.Utilities;
using Facturacion.Application;

namespace Facturacion.Api;


[Route("api/[controller]")]
[ApiController]

public class SiatSincronizacionController(IProductosServiciosApplication _productosServiciosApplication) : ControllerBase
{
     [HttpGet("VerificaComunicacion")]
    public async Task<ActionResult<Response<bool>>> GetVerifica()
    {
        // var response =  await _actividadesApplication.OkComunnication();
        // return Ok(response);

        // var response = await _actividadesApplication.UpdateActividadesDocumentoSector(1);
        // return Ok(response);

        // var response = await _leyendasFacturaApplication.UpdateLeyendasFactura(1);
        // return Ok(response);

        // var response = await _parametrosApplication.UpdateParametros(1);
        // return Ok(response);

        var response = await _productosServiciosApplication.UpdateProductos(1);
        return Ok(response);

    }
}

