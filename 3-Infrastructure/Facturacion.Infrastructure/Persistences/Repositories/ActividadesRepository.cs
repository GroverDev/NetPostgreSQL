
using Common.Utilities.Exceptions;
using Dapper;
using Facturacion.Domain;

namespace Facturacion.Infrastructure;

public class ActividadesRepository : IActividadesRepository
{
    private readonly FacturacionDbContext _context;

    public ActividadesRepository(FacturacionDbContext context)
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
                          ( id, codigo_actividad, descripcion, tipo_actividad, state, created_by, created, modified_by, modified)
                    VALUES(@Id, @CodigoActividad, @Descripcion, @TipoActividad, @State, @CreatedBy, @created, @ModifiedBy, @Modified);
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

    public async Task<bool> DisableAllActividades()
    {
        bool ok;
        using var db = _context.CreateConnection;
        try
        {
            db.Open();
            using var transaction = db.BeginTransaction();
            try
            {
                string sqlQuery = @" UPDATE siat.actividades
                                     SET state = @State, modified = @Modified WHERE state";
                var result = await db.ExecuteAsync(sqlQuery, new { State = false, Modified = DateTime.Now });
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

    public async Task<Actividades> GetActividadByCodigo(string CodigoActividad)
    {
        using var db = _context.CreateConnection;
        try
        {
            db.Open();
            var query = @"SELECT  * FROM siat.actividades WHERE codigo_actividad = @CodigoActividad";
            var actividad = await db.QueryFirstOrDefaultAsync<Actividades>(query, new { CodigoActividad });
            actividad ??= new Actividades();
            return actividad;
        }
        catch (CustomException ex) { throw new CustomException(ex.Message, ex); }
        catch (Exception ex) { throw new Exception(ex.Message, ex); }
        finally { db.Close(); }

    }

    public async Task<bool> EnableActividad(Actividades actividad)
    {
        bool ok = false;
        using var db = _context.CreateConnection;
        try
        {
            db.Open();
            using var transaction = db.BeginTransaction();
            try
            {
                string sqlQuery = @" UPDATE siat.actividades
                                        SET state = @State, modified = @Modified
                                      WHERE id = @Id
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
}
