using Common.Utilities.Exceptions;
using Dapper;
using Facturacion.Domain;

namespace Facturacion.Infrastructure;

public class CufdRepository(FacturacionDbContext _context) : ICufdRepository
{
    public async Task<bool> CreateCufd(Cufd cufd)
    {
        bool ok=false;
        using var db = _context.CreateConnection;
        try
        {
            db.Open();
            using var transaction = db.BeginTransaction();
            try
            {
                string sqlQuery = @"
                    INSERT INTO siat.cufd
                              (id, codigo, codigo_control, fecha_inicio, fecha_vigencia, vigente, id_punto_venta, state, created_by, created, modified_by, modified)
                        VALUES(@Id, @Codigo, @CodigoControl, @FechaInicio, @FechaVigencia, @Vigente, @IdPuntoVenta, @State, @CreatedBy, @Created, @ModifiedBy, @Modified);
                    ";

                var result = await db.ExecuteAsync(sqlQuery, cufd);
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
