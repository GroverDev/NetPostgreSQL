using Common.Utilities.Exceptions;
using Dapper;
using Facturacion.Domain;

namespace Facturacion.Infrastructure;

public class LeyendasFacturaRepository(FacturacionDbContext _context) : ILeyendasFacturaRepository
{
    public async Task<bool> CreateLeyendaFactura(LeyendasFactura leyenda)
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
                    INSERT INTO siat.leyendas_factura
                              (id, codigo_actividad, descripcion_leyenda, state, created_by, created, modified_by, modified)
                        VALUES(@Id, @CodigoActividad, @DescripcionLeyenda, @State, @CreatedBy, @created, @ModifiedBy, @Modified);
                    ";

                var result = await db.ExecuteAsync(sqlQuery, leyenda);
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

    public async Task<bool> DisableAllLeyendasFactura()
    {
        bool ok;
        using var db = _context.CreateConnection;
        try
        {
            db.Open();
            using var transaction = db.BeginTransaction();
            try
            {
                string sqlQuery = @" UPDATE siat.leyendas_factura
                                     SET state = @State, modified = @Modified WHERE state";
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

    public async Task<bool> EnableLeyendaFactura(LeyendasFactura leyenda)
    {
        bool ok=false;
        using var db = _context.CreateConnection;
        try
        {
            db.Open();
            using var transaction = db.BeginTransaction();
            try
            {
                string sqlQuery = @" UPDATE siat.leyendas_factura
                                        SET state = @State, modified = @Modified
                                      WHERE id = @Id
                                   ";

                var result = await db.ExecuteAsync(sqlQuery, leyenda );
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

    public async Task<LeyendasFactura> GetLeyendaFacturaByCodigo(string CodigoActividad, string DescripcionLeyenda)
    {
         using var db = _context.CreateConnection;
        try
        {
            db.Open();
            var query = @"SELECT  * 
                            FROM siat.leyendas_factura
                           WHERE codigo_actividad= @CodigoActividad AND descripcion_leyenda = @DescripcionLeyenda ";
            var leyenda = await db.QueryFirstOrDefaultAsync<LeyendasFactura>(query, new { CodigoActividad, DescripcionLeyenda });
            leyenda ??= new LeyendasFactura();
            return leyenda;
        }
        catch (CustomException ex) { throw new CustomException(ex.Message, ex); }
        catch (Exception ex) { throw new Exception(ex.Message, ex); }
        finally { db.Close(); }
    }
}
