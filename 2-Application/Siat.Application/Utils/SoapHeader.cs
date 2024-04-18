using System.ServiceModel;
using System.ServiceModel.Channels;

namespace Siat.Application;


public static class SoapHeader
{
    public static void Create()
    {

        HttpRequestMessageProperty httpRequestProperty = new();
        string header = "eyJ0eXAiOiJKV1QiLCJhbGciOiJIUzUxMiJ9";//Comun.Herramientas.Appsettings.GetValor("JwtSIAT", "header");
        string payload = "eyJzdWIiOiJjc2JwMjAxOSIsImNvZGlnb1Npc3RlbWEiOiI3MjM3OTlCRDE5NDg3REZENUYzNjAzNiIsIm5pdCI6Ikg0c0lBQUFBQUFBQUFETTBNREl3TXpZMU1MSUFBUHFYVHpjS0FBQUEiLCJpZCI6MTA3ODY3OCwiZXhwIjoxNzQyNDg5ODgyLCJpYXQiOjE3MTA5NjgyNTIsIm5pdERlbGVnYWRvIjoxMDIwNjM1MDI4LCJzdWJzaXN0ZW1hIjoiU0ZFIn0";//Comun.Herramientas.Appsettings.GetValor("JwtSIAT", "payload");
        string signature = "qi6wYI27OtvIb0gHDK4_lAs_K0BVicGdCjlGKNVfl46oHdveqnHyOoNE8Xo_72xhHMFxWVhkSloaPQdE3y1y7w";//Comun.Herramientas.Appsettings.GetValor("JwtSIAT", "signature");
        httpRequestProperty.Headers.Add("apikey", "TokenApi " + header+"."+payload+"."+signature);
        OperationContext.Current.OutgoingMessageProperties[HttpRequestMessageProperty.Name] = httpRequestProperty;

    }
}

internal  class SiatParameters
{
    public SiatParameters()
    {
        //var parametro = "SiatParametrosSistemas"; 
        this.nit = 1020625028;//Convert.ToInt32(Comun.Herramientas.Appsettings.GetValor(parametro, "nit") ?? "0");
        this.codigoAmbiente = 2;//Convert.ToInt32(Comun.Herramientas.Appsettings.GetValor(parametro, "codigoAmbiente") ?? "0");
        this.codigoModalidad = "1";//Comun.Herramientas.Appsettings.GetValor(parametro, "codigoModalidad") ?? "";
        this.codigoSistema = "723799BD19487DFD5F36036";//Comun.Herramientas.Appsettings.GetValor(parametro, "codigoSistema") ?? "";
        //this.codigoDocumentoFiscal = Convert.ToInt32(Comun.Herramientas.Appsettings.GetValor(parametro, "codigoDocumentoFiscal") ?? "1");
        this.razonSocial = "Caja Dde Salud de la Banca Privada";//Comun.Herramientas.Appsettings.GetValor(parametro, "razonSocial") ?? "";
    }

    public int nit { get; set; }
    public int codigoAmbiente { get; set; }

    public string codigoModalidad { get; set; }

    public string codigoSistema { get; set; }
    //public int codigoDocumentoFiscal { get; set; }
    public string razonSocial { get; set; }
}
   

