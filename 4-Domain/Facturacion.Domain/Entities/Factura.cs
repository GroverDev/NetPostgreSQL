namespace Facturacion.Domain;

public class Factura
{
    public Guid Id { get; set; }
    public int NitEmisor { get; set; }
    public string RazonSocialEmisor { get; set; } = "";
    public string Municipio { get; set; } = "";
    public string Telefono { get; set; } = "";
    public int NumeroFactura { get; set; } = 0;
    public string Cuf { get; set; } = "";
    public string Cufd { get; set; } = "";
    public int CodigoSucursal { get; set; }
    public string Direccion { get; set; } = "";
    public int CodigoPuntoVenta { get; set; }
    public string FechaEmision { get; set; } = "";
    public string NombreRazonSocial { get; set; } = "";
    public int CodigoTipoDocumentoIdentidad { get; set; }
    public string NumeroDocumento { get; set; } = "";
    public string Complemento { get; set; } = "";
    public string CodigoCliente { get; set; } = "";
    
    public int CodigoMetodoPago { get; set; }
    public string NumeroTarjeta { get; set; } = "";
    public decimal MontoTotal { get; set; }
    public decimal MontoTotalSujetoIva { get; set; }
    public int CodigoMoneda { get; set; }
    public decimal TipoCambio { get; set; }
    public decimal MontoTotalMoneda { get; set; }
 
    public decimal DescuentoAdicional { get; set; }        
    public int CodigoExcepcion { get; set; }
    public string Cafc { get; set; } = "";
    public string Leyenda { get; set; } = "";
    public string Usuario { get; set; } = "";
    //public TipoDocumentoSector codigoDocumentoSector { get; set; }
    public List<FacturaDetalle> Detalle { get; set; } = [];
    // Otros Campos

    // Mas Campos
    public string Estado { get; set; } = "";
    public int CodigoEmision { get; set; }
    public string NombreEmision { get; set; } = "";
    public int TipoFacturaDocumento { get; set; }
    //public string EsquemaDocumentoSector { get; set; }  = "";
    //public string NombreXmlFirmado { get; set; } = "";
    // ?public string FechaEmisionSiat { get; set; } = "";
    public string Cuis { get; set; } = "";
    public string CodigoRecepcion { get; set; } = "";
    public int IdPuntoventa { get; set; }
    public string EmailCliente { get; set; } = "";
}



   
