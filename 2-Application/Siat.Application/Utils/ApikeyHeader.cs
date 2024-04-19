using System.ServiceModel;
using System.ServiceModel.Channels;

namespace Siat.Application;


public static class ApikeyHeader
{

    public static void Create(string apiKey)
    {
        HttpRequestMessageProperty httpRequestProperty = new();
        httpRequestProperty.Headers.Add("apikey", $"TokenApi {apiKey}" );
        OperationContext.Current.OutgoingMessageProperties[HttpRequestMessageProperty.Name] = httpRequestProperty;

    }
}

