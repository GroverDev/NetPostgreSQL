namespace Facturacion.Domain;

public class PuntoVenta: Audit
{
    public Guid Id { get; set; }
    public int Codigo { get; set; }
    public string Nombre { get; set; } ="";
    public Guid IdSucursal { get; set; }

}