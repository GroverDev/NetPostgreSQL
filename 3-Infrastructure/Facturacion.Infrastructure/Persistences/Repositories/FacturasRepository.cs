using Common.Utilities.Exceptions;
using Dapper;
using Facturacion.Domain;

namespace Facturacion.Infrastructure;

public class FacturasRepository(FacturacionDbContext _context) : IFacturasRepository
{
    public async Task<bool> CreateFactura(Factura factura)
    {
        bool ok = false;
        using var db = _context.CreateConnection;
        try
        {
            db.Open();
            using var transaction = db.BeginTransaction();
            try
            {
                string sqlQuery = @"
                    INSERT INTO siat.facturas
                          (id, cafc, codigo_cliente, codigo_documento_sector, codigo_excepcion, codigo_metodo_pago, codigo_moneda, codigo_punto_venta, codigo_sucursal, 
                           codigo_tipo_documento_identidad, complemento, cuf,    cufd, cuis,  descuento_adicional, direccion, email_cliente,  
                           @estado, fecha_emision, fecha_emision_formato_siat, leyenda,  monto_gift_card, monto_total, monto_total_moneda, monto_total_sujeto_iva, 
                           municipio,  nit_emisor, nombre_razon_social, numero_documento, numero_factura, numero_tarjeta, razon_social_emisor, telefono, tipo_cambio,
                           usuario, codigo_emision, tipo_factura_documento, codigo_recepcion, codigo_evento_significativo, id_punto_venta, 
                           state, created_by,  created, modified_by, modified)
                    VALUES(@Id,@Cafc,@CodigoCliente, @CodigoDocumentoSector,  @CodigoExcepcion, @CodigoMetodoPago,  @CodigoMoneda, @CodigoPuntoVenta,  @CodigoSucursal,
                           @CodigoTipoDocumentoIdentidad,   @Complemento, @Cuf, @Cufd, @Cuis, @DescuentoAdicional, @Direccion, @EmailCliente, 
                           @Estado, @FechaEmision, @FechaEmisionFormatoSiat,   @Leyenda, @MontoGiftCard,  @MontoTotal, @MontoTotalMoneda,  @MontoTotalSujetoIva, 
                           @Municipio, @NitEmisor, @NombreRazonSocial,  @NumeroDocumento,  @NumeroFactura, @NumeroTarjeta, @RazonSocialEmisor,  @Telefono, @TipoCambio, 
                           @Usuario, @CodigoEmision, @TipoFacturaDocumento, @CodigoRecepcion, @CodigoEventoSignificativo,  @IdPuntoVenta,  
                           @State, @CreatedBy, @Created, @ModifiedBy, @Modified);
                    ";

                var result = await db.ExecuteAsync(sqlQuery, factura);
            
                sqlQuery = @"INSERT INTO siat.facturas_detalle
                                  ( id, actividad_economica, cantidad,  codigo_producto, codigo_producto_sin, descripcion,  monto_descuento, numero_imei, numero_serie, precio_unitario, sub_total, unidad_medida, id_factura, state, created_by,  created,  modified_by, modified)
                            VALUES( @Id, @ActividadEconomica, @Cantidad, @CodigoProducto, @CodigoProductoSin,  @Descripcion, @MontoDescuento, @NumeroImei, @NumeroSerie, @PrecioUnitario, @SubTotal, @UnidadMedida, @IdFactura, @State, @CreatedBy, @Created, @ModifiedBy, @Modified);";
                foreach (var producto in factura.Detalle)
                {
                    var resultProducto = await db.ExecuteAsync(sqlQuery, producto);
                }
                transaction.Commit();
                ok = true;
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                throw new Exception(ex.Message, ex);
            }
        }
        catch (CustomException ex) { throw new CustomException(ex.Message, ex); }
        catch (Exception ex) { throw new Exception(ex.Message, ex); }
        finally { db.Close(); }
        return ok;


    }
}
