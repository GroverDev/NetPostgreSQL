namespace Facturacion.Domain;

public class SincronizacionRequest
{
    public int CodigoPuntoVenta { get; set; } = 0;
    public int CodigoSucursal { get; set; } = 0;
    public string CodigoCUIS  { get; set; }="";

}
