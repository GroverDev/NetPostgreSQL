﻿using Common.Utilities.Exceptions;
using Dapper;
using Facturacion.Domain;

namespace Facturacion.Infrastructure;

public class ParametrosRepository(FacturacionDbContext _context) : IParametrosRepository
{
    public async Task<bool> CreateParametro(Parametros parametro)
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
                    INSERT INTO siat.parametros
                          (id, codigo_clasificador, codigo_tipo_parametro, descripcion, state, created_by, created, modified_by, modified)
                    VALUES(@id, @CodigoClasificador, @CodigoTipoParametro, @Descripcion, @State, @CreatedBy, @created, @ModifiedBy, @Modified);
                    ";

                var result = await db.ExecuteAsync(sqlQuery, parametro);
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

    public async Task<bool> DisableAllParametros(ParametrosEnum parametrosEnum)
    {
        bool ok;
        using var db = _context.CreateConnection;
        try
        {
            db.Open();
            using var transaction = db.BeginTransaction();
            try
            {
                string sqlQuery = @" UPDATE siat.parametros
                                        SET state = @State, modified = @Modified 
                                      WHERE state AND codigo_tipo_parametro = @CodigoTipoParametro";
                var result = await db.ExecuteAsync(sqlQuery, new { State = false, Modified =  DateTime.Now, CodigoTipoParametro = Enum.GetName(parametrosEnum) });
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

    public async Task<bool> EnableParametro(Parametros parametro)
    {
        bool ok=false;
        using var db = _context.CreateConnection;
        try
        {
            db.Open();
            using var transaction = db.BeginTransaction();
            try
            {
                string sqlQuery = @" UPDATE siat.parametros
                                        SET state = @State, modified = @Modified
                                      WHERE id = @Id
                                   ";

                var result = await db.ExecuteAsync(sqlQuery, parametro );
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

    public async Task<Parametros> GetParametroByCodigo(int CodigoClasificador, string Descripcion, ParametrosEnum parametrosEnum)
    {
         using var db = _context.CreateConnection;
        try
        {
            db.Open();
            var query = @"SELECT  * 
                            FROM siat.parametros
                           WHERE codigo_clasificador= @CodigoClasificador AND descripcion = @Descripcion AND codigo_tipo_parametro = @CodigoTipoParametro ";
            var parametro = await db.QueryFirstOrDefaultAsync<Parametros>(query, new { CodigoClasificador, Descripcion, CodigoTipoParametro = Enum.GetName(parametrosEnum) });
            parametro ??= new Parametros();
            return parametro;
        }
        catch (CustomException ex) { throw new CustomException(ex.Message, ex); }
        catch (Exception ex) { throw new Exception(ex.Message, ex); }
        finally { db.Close(); }
    }
}