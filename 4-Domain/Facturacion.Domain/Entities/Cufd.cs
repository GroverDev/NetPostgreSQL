namespace Facturacion.Domain;

public class Cufd:Audit
{
    public Guid Id { get; set; }
    public string Codigo { get; set; } = "";
    public string CodigoControl { get; set; } = "";
    public DateTime FechaInicio { get; set; }
  	public DateTime FechaVigencia { get; set; }
    public bool Vigente { get; set; }
	public Guid IdPuntoVenta { get; set; }
}
