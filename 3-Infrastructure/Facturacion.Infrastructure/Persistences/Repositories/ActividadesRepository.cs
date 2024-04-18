
using Common.Utilities.Exceptions;
using Dapper;
using Facturacion.Domain;

namespace Facturacion.Infrastructure;

public class ActividadesRepository : IActividadesRepository
{
    private readonly FacturacionDbContext _context;

    public ActividadesRepository(FacturacionDbContext context )
    {
        _context = context;
    }

    public async Task<bool> CreateActividad(Actividades actividad)
    {
        bool ok;
        using var db = _context.CreateConnection;
        try
        {
                db.Open();
                using var transaction = db.BeginTransaction();
                try
                {
                    string sqlQuery = @"
                    INSERT INTO siat.actividades
                          ( id, codigo_caeb, descripcion, tipo_actividad, state, created_by, created, modified_by, modified)
                    VALUES(@Id, @CodigoCaeb, @Descripcion, @TipoActividad, @State, @CreatedBy, @created, @ModifiedBy, @Modified);
                    ";

                    var result = await db.ExecuteAsync(sqlQuery, actividad);
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

    public async Task<bool> DeleteAllActividades()
    {
        using var connection = _context.CreateConnection;
        var query = @"DELETE FROM siat.actividades";
        var actividades = await connection.QueryAsync(query);
        return true;
    }

    public async Task<Actividades> GetActividadByCodigo(string CodigoCaeb)
    {
        using var db = _context.CreateConnection;
        try
        {
            db.Open();
            var query = @"SELECT  * FROM siat.actividades WHERE codigo_caeb = @CodigoCaeb";
            var actividad = await db.QueryFirstOrDefaultAsync<Actividades>(query,new {CodigoCaeb = CodigoCaeb});
            actividad ??= new Actividades();
            return actividad;     
        }
        catch (CustomException ex) { throw new CustomException(ex.Message, ex); }
        catch (Exception ex) { throw new Exception(ex.Message, ex); }
        finally {db.Close(); }
       
    }
}
