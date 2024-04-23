namespace Facturacion.Domain;

public class Parametros: Audit
{
    public Guid Id { get; set; }
    public string CodigoClasificador { get; set; } = "";
    public string CodigoTipoParametro { get; set; } = "";
    public string  Descripcion { get; set; } = "";

}
