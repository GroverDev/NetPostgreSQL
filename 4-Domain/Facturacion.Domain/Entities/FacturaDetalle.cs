namespace Facturacion.Domain;

public class FacturaDetalle: Audit
{
    public Guid Id { get; set; }
    public string ActividadEconomica { get; set; } = "";
    public int CodigoProductoSin { get; set; }
    public string CodigoProducto { get; set; }= "";
    public string Descripcion { get; set; }= "";
    public string DescripcionProducto { get; set; }= "";
    public decimal Cantidad { get; set; }
    public int UnidadMedida { get; set; }
    public decimal PrecioUnitario { get; set; }
    public decimal MontoDescuento { get; set; }
    public decimal SubTotal { get; set; }
    // Es solo para facturas de Compra y Venta
    public string NumeroSerie { get; set; }= "";
    public string NumeroImei { get; set; }= "";
    public string NombreUnidadMedida { get; set; }= "";
    public Guid IdFactura {get;set;}

}

        
