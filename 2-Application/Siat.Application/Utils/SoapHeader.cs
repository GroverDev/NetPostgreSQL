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
        string signature = "qi6wYI27OtvIb0gHDK4_lAs_K0BVicGdCjlGKNVfl46oHdveqnHyOoNE8Xo_72xhHMFxWVhkSloaPQdE3y1y7w";
                            
        httpRequestProperty.Headers.Add("apikey", "TokenApi " + header+"."+payload+"."+signature);
        OperationContext.Current.OutgoingMessageProperties[HttpRequestMessageProperty.Name] = httpRequestProperty;

    }
}

