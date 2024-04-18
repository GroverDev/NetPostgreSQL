namespace Facturacion.Domain;

public class Actividad: Audit
{
    public Guid Id { get; set; }
    public string  CodigoCaeb {get;set;}
    public string Descripcion{get;set;}
    public string TipoActividad { get; set; }


    
}
