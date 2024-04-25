namespace Facturacion.Domain;

public class ProductosServicios: Audit
{
    public Guid Id { get; set; }
    public string CodigoActividad { get; set; }="";
    public long CodigoProducto { get; set; }
    public string DescripcionProducto { get; set; } ="";
}
