﻿using Common.Utilities;
using Siat.Application;
using Facturacion.Infrastructure;
using AutoMapper;
using Facturacion.Domain;
using Common.Utilities.Exceptions;


namespace Facturacion.Application;
public class ActividadesApplication(
    ISincronizacionApplication _sincronizacionApplication,
    IActividadesRepository _actividadesRepository,
    ISincronizacionRequestRepository _sincronizacionRequestRepository,
    IMapper _mapper
    ) : IActividadesApplication
{
   
    public async Task<Response<bool>> OkComunnication()
    {
        return await _sincronizacionApplication.OkComunnication();
    }

    public async Task<Response<bool>> UpdateActividades(int createdBy)
    {
        var response = new Response<bool>();
        var sinc = await _sincronizacionRequestRepository.GetSincronizacionRequest(0);
        try
        {
            var resp = await _sincronizacionApplication.GetActividades(sinc.CodigoPuntoVenta, sinc.CodigoSucursal, sinc.CodigoCUIS);
            if (resp.Ok)
            {
                if (await _actividadesRepository.DisableAllActividades())
                {
                    foreach (var actividadSiat in resp.Data)
                    {
                        var actividadDB = await _actividadesRepository.GetActividadByCodigo(actividadSiat.codigoCaeb);
                        if (actividadDB.CodigoActividad == "")
                        {
                            var actividad = _mapper.Map<Actividades>(actividadSiat);
                            actividad.Id = Guid.NewGuid();
                            actividad.Created = actividad.Modified = DateTime.Now;
                            actividad.CreatedBy = actividad.ModifiedBy = createdBy;
                            actividad.State = true;
                            await _actividadesRepository.CreateActividad(actividad);
                        } else {
                            actividadDB.Modified = DateTime.Now;
                            actividadDB.ModifiedBy = createdBy;
                            actividadDB.State = true;
                            await _actividadesRepository.EnableActividad(actividadDB);
                        }
                    }
                    response.Ok = response.Data = true;
                } else throw new CustomException("No se pudo deshabiliatr los documentos sector");
            }
        }
        catch (CustomException ex) { response.SetMessage(MessageTypes.Warning, ex.Message); }
        catch (Exception ex) { response.SetLogMessage(MessageTypes.Error, "Ocurrio un error, por favor comuniquese con Sistemas.", ex); }

        return response;
    }
}
