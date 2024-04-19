namespace Facturacion.Domain;

public class ActividadesDocumentoSector: Audit
{
    public Guid Id { get; set; }
    public string CodigoActividad { get; set; }="";
    public int CodigoDocumentoSector { get; set; } = 0;
    public string TipoDocumentoSector { get; set; }="";
	
}
