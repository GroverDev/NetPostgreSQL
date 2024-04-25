using AutoMapper;
using Common.Utilities;
using Common.Utilities.Exceptions;
using Facturacion.Domain;
using Facturacion.Infrastructure;
using Siat.Application;

namespace Facturacion.Application;

public class ProductosServiciosApplication (
    ISincronizacionApplication _sincronizacionApplication,
    IProductosServiciosRepository _productosRepository,
    IMapper _mapper
) : IProductosServiciosApplication
{
    public async Task<Response<bool>> UpdateProductos(int createdBy)
    {
         var response = new Response<bool>();
        int codigoPutnoDeVenta = 0;
        int codigoSucursal = 5;
        string codigoCuis = "E8465CDD";
        try
        {
            var resp = await _sincronizacionApplication.GetProductosServicios(codigoPutnoDeVenta, codigoSucursal, codigoCuis);
            if (resp.Ok)
            {
                if (await _productosRepository.DisableAllProductosServicios())
                {
                    foreach (var productoServicioSiat in resp.Data)
                    {
                        var productoServicioDB = await _productosRepository.GetProductoServicioByCodigo(productoServicioSiat.codigoActividad, productoServicioSiat.codigoProducto);
                        if (productoServicioDB.CodigoActividad == "")
                        {
                            var productoServicioNuevo = _mapper.Map<ProductosServicios>(productoServicioSiat);
                            productoServicioNuevo.Id = Guid.NewGuid();
                            productoServicioNuevo.Created = productoServicioNuevo.Modified = DateTime.Now;
                            productoServicioNuevo.CreatedBy = productoServicioNuevo.ModifiedBy = createdBy;
                            productoServicioNuevo.State = true;
                            await _productosRepository.CreateProductoServicio(productoServicioNuevo);
                        } else {
                            productoServicioDB.Modified = DateTime.Now;
                            productoServicioDB.ModifiedBy = createdBy;
                            productoServicioDB.State = true;
                            await _productosRepository.EnableProductoServicio(productoServicioDB);
                        }
                    }
                    response.Ok = response.Data = true;
                } else throw new CustomException("No se pudo deshabilitar las leyendas");
            }
        }
        catch (CustomException ex) { response.SetMessage(MessageTypes.Warning, ex.Message); }
        catch (Exception ex) { response.SetLogMessage(MessageTypes.Error, "Ocurrio un error, por favor comuniquese con Sistemas.", ex); }

        return response;
    }
}
