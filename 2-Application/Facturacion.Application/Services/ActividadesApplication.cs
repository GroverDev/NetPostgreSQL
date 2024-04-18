using Common.Utilities;
using Siat.Application;
using Facturacion.Infrastructure;
using AutoMapper;
using Facturacion.Domain;
using Common.Utilities.Exceptions;


namespace Facturacion.Application;
public class ActividadesApplication(
    ISincronizacionApplication sincronizacionApplication,
    IActividadesRepository actividadesRepository,
    IMapper mapper) : IActividadesApplication
{
    private readonly ISincronizacionApplication _sincronizacionApplication = sincronizacionApplication;
    private readonly IActividadesRepository _actividadesRepository = actividadesRepository;
    private readonly IMapper _mapper = mapper;


    public async Task<Response<bool>> OkComunnication()
    {
        return await _sincronizacionApplication.OkComunnication();
    }

    public async Task<Response<bool>> UpdateActividades(int createdBy)
    {
        var response = new Response<bool>();
        int codigoPutnoDeVenta = 0;
        int codigoSucursal = 5;
        string codigoCuis = "E813D239";
        try
        {
            var resp = await _sincronizacionApplication.GetActividades(codigoPutnoDeVenta, codigoSucursal, codigoCuis);
            if (resp.Ok)
            {
                foreach (var actividadSiat in resp.Data)
                {
                    var actividadFacturacion = await _actividadesRepository.GetActividadByCodigo(actividadSiat.codigoCaeb);
                    if (actividadFacturacion.CodigoCaeb == "")
                    {
                        var fechaActual = DateTime.Now;
                        var actividad = _mapper.Map<Actividad>(actividadSiat);
                        actividad.Id = Guid.NewGuid();
                        actividad.Created = actividad.Modified = fechaActual;
                        actividad.CreatedBy = actividad.ModifiedBy = createdBy;
                        await _actividadesRepository.CreateActividad(actividad);
                        response.Ok = true;
                    }
                }
            }
        }
        catch (CustomException ex) { response.Message.SetMessage(MessageTypes.Warning, ex.Message); }
        catch (Exception ex) {  response.Message.SetLogMessage(MessageTypes.Error, "Ocurrio un error, por favor comuniquese con Sistemas.", ex);  }

        return response;
    }
}
