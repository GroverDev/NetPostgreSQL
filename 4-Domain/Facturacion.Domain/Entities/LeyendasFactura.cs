namespace Facturacion.Domain;

public class LeyendasFactura: Audit
{
    public Guid Id { get; set; }
    public string CodigoActividad { get; set; } ="";
    public  string DescripcionLeyenda { get; set; }="";
}
