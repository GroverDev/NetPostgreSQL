using Common.Utilities.Exceptions;
using Dapper;
using Facturacion.Domain;

namespace Facturacion.Infrastructure;

public class ActividadesDocumentoSectorRepository(FacturacionDbContext context) : IActividadesDocumentoSectorRepository
{
    private readonly FacturacionDbContext _context = context;

    public async Task<bool> CreateActividadDocumentoSector(ActividadesDocumentoSector actividad)
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
                    INSERT INTO siat.actividades_documento_sector
                          (id, codigo_actividad,  codigo_documento_sector, tipo_documento_sector, state,  created_by, created,  modified_by, modified)
                    VALUES(@Id, @CodigoActividad, @CodigoDocumentoSector,  @TipoDocumentoSector , @State, @CreatedBy, @created, @ModifiedBy, @Modified);
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
    public async Task<ActividadesDocumentoSector> GetActividadDocumentoSectorByCodigo(int CodigoDocumentoSector, string CodigoActividad)
    {
        using var db = _context.CreateConnection;
        try
        {
            db.Open();
            var query = @"SELECT  * FROM siat.actividades_documento_sector WHERE codigo_documento_sector = @CodigoDocumentoSector AND codigo_actividad = @CodigoActividad";
            var actividad = await db.QueryFirstOrDefaultAsync<ActividadesDocumentoSector>(query, new { CodigoDocumentoSector, CodigoActividad });
            actividad ??= new ActividadesDocumentoSector();
            return actividad;
        }
        catch (CustomException ex) { throw new CustomException(ex.Message, ex); }
        catch (Exception ex) { throw new Exception(ex.Message, ex); }
        finally { db.Close(); }
    }
    public async Task<bool> DisableAllActividadDocumentoSector()
    {
        bool ok;
        using var db = _context.CreateConnection;
        try
        {
            db.Open();
            using var transaction = db.BeginTransaction();
            try
            {
                string sqlQuery = @" UPDATE siat.actividades_documento_sector
                                     SET state = @State and modified = @Modified WHERE state";
                var result = await db.ExecuteAsync(sqlQuery, new { State = false, Modified =  DateTime.Now});
                transaction.Commit();
                ok = true;
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                throw new Exception(ex.Message, ex);
            }
        }
        catch (CustomException ex) {  throw new CustomException(ex.Message, ex); }
        catch (Exception ex) { throw new Exception(ex.Message, ex); }
        finally { db.Close(); }
        return ok;
    }

    public async Task<bool> EnableActividadDocumentoSector(ActividadesDocumentoSector actividadDocumentoSector)
    {
        bool ok=false;
        using var db = _context.CreateConnection;
        try
        {
            db.Open();
            using var transaction = db.BeginTransaction();
            try
            {
                string sqlQuery = @" UPDATE siat.actividades_documento_sector
                                        SET state = @State and modified = @Modified
                                      WHERE id = @Id
                                   ";

                var result = await db.ExecuteAsync(sqlQuery, actividadDocumentoSector );
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
