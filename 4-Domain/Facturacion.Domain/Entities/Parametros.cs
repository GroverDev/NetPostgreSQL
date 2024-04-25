namespace Facturacion.Domain;

public class Parametros: Audit
{
    public Guid Id { get; set; }
    public int CodigoClasificador { get; set; } = 0;
    public string CodigoTipoParametro { get; set; } = "";
    public string  Descripcion { get; set; } = "";

}
