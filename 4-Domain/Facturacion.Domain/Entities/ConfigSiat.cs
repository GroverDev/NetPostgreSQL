namespace Facturacion.Domain;

public class ConfigSiat
{
    public string Nit { get; set; } = "";
    public string CodigoAmbiente { get; set; } ="";
    public string CodigoModalidad { get; set; } ="";
    public string CodigoSistema { get; set; } ="";
    public string RazonSocial { get; set; } ="";
    public string ApiKey { get; set; } ="";

    public string FirmaPathKeyPrivada { get; set; } ="";
    public string FirmaPathCertificado { get; set; } ="";
    public string FirmaPathXsd { get; set; } ="";
    public string FacturaPathStorage { get; set; } ="";
}

