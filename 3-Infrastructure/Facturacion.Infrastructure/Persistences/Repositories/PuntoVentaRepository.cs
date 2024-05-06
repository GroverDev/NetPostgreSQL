using Common.Utilities.Exceptions;
using Dapper;
using Facturacion.Domain;

namespace Facturacion.Infrastructure;

public class PuntoVentaRepository(FacturacionDbContext _context) : IPuntoVentaRepository
{
    public async Task<PuntoVenta> GetPuntoVentaByCodigo(int Codigo)
    {
        using var db = _context.CreateConnection;
        try
        {
            db.Open();
            var query = @"SELECT  * 
                            FROM siat.punto_venta
                           WHERE codigo= @Codigo ";
            var puntoVenta = await db.QueryFirstOrDefaultAsync<PuntoVenta>(query, new { Codigo });
            puntoVenta ??= new PuntoVenta();
            return puntoVenta;
        }
        catch (CustomException ex) { throw new CustomException(ex.Message, ex); }
        catch (Exception ex) { throw new Exception(ex.Message, ex); }
        finally { db.Close(); }
    }
}
