using Common.Utilities.Exceptions;
using Dapper;
using Facturacion.Domain;

namespace Facturacion.Infrastructure;

public class ProductosServiciosRepository(FacturacionDbContext _context) : IProductosServiciosRepository
{
    public async Task<bool> CreateProductoServicio(ProductosServicios producto)
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
                    INSERT INTO siat.productos_servicios
                           (id, codigo_actividad,   codigo_producto, descripcion_producto, state, created_by, created, modified_by, modified)
                    VALUES (@Id, @CodigoActividad, @CodigoProducto, @DescripcionProducto, @State, @CreatedBy, @created, @ModifiedBy, @Modified);
                    ";

                var result = await db.ExecuteAsync(sqlQuery, producto);
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

    public async Task<bool> DisableAllProductosServicios()
    {
        bool ok;
        using var db = _context.CreateConnection;
        try
        {
            db.Open();
            using var transaction = db.BeginTransaction();
            try
            {
                string sqlQuery = @" UPDATE siat.productos_servicios
                                        SET state = @State, modified = @Modified 
                                      WHERE state ";
                var result = await db.ExecuteAsync(sqlQuery, new { State = false, Modified =  DateTime.Now });
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

    public async Task<bool> EnableProductoServicio(ProductosServicios producto)
    {
        bool ok=false;
        using var db = _context.CreateConnection;
        try
        {
            db.Open();
            using var transaction = db.BeginTransaction();
            try
            {
                string sqlQuery = @" UPDATE siat.productos_servicios
                                        SET state = @State, modified = @Modified
                                      WHERE id = @Id
                                   ";

                var result = await db.ExecuteAsync(sqlQuery, producto );
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

    public async Task<ProductosServicios> GetProductoServicioByCodigo(string CodigoActividad, long CodigoProducto)
    {
         using var db = _context.CreateConnection;
        try
        {
            db.Open();
            var query = @"SELECT  * 
                            FROM siat.productos_servicios
                           WHERE codigo_actividad= @CodigoActividad AND codigo_producto = @CodigoProducto ";
            var producto = await db.QueryFirstOrDefaultAsync<ProductosServicios>(query, new { CodigoActividad, CodigoProducto });
            producto ??= new ProductosServicios();
            return producto;
        }
        catch (CustomException ex) { throw new CustomException(ex.Message, ex); }
        catch (Exception ex) { throw new Exception(ex.Message, ex); }
        finally { db.Close(); }
    }
}
