namespace Facturacion.Domain;

public class Actividades: Audit
{
    public Guid Id { get; set; }
    public string  CodigoActividad {get;set;} = "";
    public string Descripcion{get;set;} = "";
    public string TipoActividad { get; set; } = "";


    
}
