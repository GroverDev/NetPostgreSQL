using System.Net.Http.Headers;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Security.Cryptography.Xml;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Schema;
using Common.Utilities;
using Common.Utilities.Exceptions;
using Facturacion.Domain;
using Microsoft.Extensions.Options;

namespace Facturacion.Application;

public class XmlApplication(IOptions<ConfigSiat> configSiat)
{
    private readonly  ConfigSiat _configSiat = configSiat.Value;
    public static Response<bool> ArmaFirmaValidaYGuardaXML(Factura factura)
    {
        var resp = new Response<bool>();
        Response<XmlDocument> respuesta = new Response<XmlDocument>();
        XmlDocument Xml = new XmlDocument();
        XmlDocument XmlDocFirmado = new XmlDocument();

        XDocument XmlDoc; // = new XDocument();            
        XElement Cuerpo, Cabecera, Detalle;
        XNamespace ns = "http://www.w3.org/2001/XMLSchema-instance";
        string nombreDocumentoSector = "factura.esquemaDocumentoSector"; // todo
        string mensaje = "";
        string nombre = "";

        try
        {
            XAttribute[] attArray = { new XAttribute(XNamespace.Xmlns + "xsi", ns.NamespaceName), new XAttribute(ns + "noNamespaceSchemaLocation", nombreDocumentoSector + ".xsd") };
            XAttribute nil = new XAttribute(ns + "nil", "true");

            #region "Armamos el XML deacuerdo al documento sector"
            XmlDoc = new XDocument(new XDeclaration("1.0", "utf-8", ""));
            Cuerpo = new XElement(nombreDocumentoSector, attArray);
            // Generamos la Cabecera
            Cabecera = new XElement("cabecera");
            Cabecera.Add(new XElement("nitEmisor", factura.NitEmisor));
            Cabecera.Add(new XElement("razonSocialEmisor", factura.RazonSocialEmisor));
            Cabecera.Add(new XElement("municipio", factura.Municipio));
            Cabecera.Add(factura.Telefono == "" ? new XElement("telefono", nil) : new XElement("telefono", factura.Telefono));
            Cabecera.Add(new XElement("numeroFactura", factura.NumeroFactura));
            Cabecera.Add(new XElement("cuf", factura.Cuf));
            Cabecera.Add(new XElement("cufd", factura.Cufd));
            Cabecera.Add(new XElement("codigoSucursal", factura.CodigoSucursal));
            Cabecera.Add(new XElement("direccion", factura.Direccion));
            Cabecera.Add(factura.CodigoPuntoVenta < 0 ? new XElement("codigoPuntoVenta", nil) : new XElement("codigoPuntoVenta", factura.CodigoPuntoVenta));
            Cabecera.Add(new XElement("fechaEmision", factura.FechaEmision));
            Cabecera.Add(factura.NombreRazonSocial == "" ? new XElement("nombreRazonSocial", nil) : new XElement("nombreRazonSocial", factura.NombreRazonSocial));
            Cabecera.Add(new XElement("codigoTipoDocumentoIdentidad", factura.CodigoTipoDocumentoIdentidad));
            Cabecera.Add(new XElement("numeroDocumento", factura.NumeroDocumento));
            Cabecera.Add(factura.Complemento == "" ? new XElement("complemento", nil) : new XElement("complemento", factura.Complemento));
            Cabecera.Add(new XElement("codigoCliente", factura.CodigoCliente));
            // Solo para las factura de tipo inmueble
            // Todo
            // if (factura.CodigoDocumentoSector == TipoDocumentoSector.Alquiler || factura.codigoDocumentoSector == TipoDocumentoSector.ZonaFrancaAlquiler)
            // {
            //     Cabecera.Add(new XElement("periodoFacturado", factura.periodoFacturado));
            // }
            Cabecera.Add(new XElement("codigoMetodoPago", factura.CodigoMetodoPago));
            Cabecera.Add(factura.NumeroTarjeta == "" ? new XElement("numeroTarjeta", nil) : new XElement("numeroTarjeta", factura.NumeroTarjeta));
            Cabecera.Add(new XElement("montoTotal", factura.MontoTotal));
            Cabecera.Add(new XElement("montoTotalSujetoIva", factura.MontoTotalSujetoIva));
            Cabecera.Add(new XElement("codigoMoneda", factura.CodigoMoneda));
            Cabecera.Add(new XElement("tipoCambio", factura.TipoCambio));
            Cabecera.Add(new XElement("montoTotalMoneda", factura.MontoTotalMoneda));
            // TOdo
            // Solo para zona franca
            // if (factura.codigoDocumentoSector == TipoDocumentoSector.ZonaFranca)
            // {
            //     Cabecera.Add((factura.numeroParteRecepcion == "" ? new XElement("numeroParteRecepcion", nil) : new XElement("numeroParteRecepcion", factura.numeroParteRecepcion)));
            // }
            // // Solo para factura de compra y Venta y Zona Franca
            // if (factura.codigoDocumentoSector == TipoDocumentoSector.CompraYVenta || factura.codigoDocumentoSector == TipoDocumentoSector.ZonaFranca)
            // {
            //     Cabecera.Add((factura.montoGiftCard < 0 ? new XElement("montoGiftCard", nil) : new XElement("montoGiftCard", factura.montoGiftCard)));
            // }
            Cabecera.Add(new XElement("descuentoAdicional", factura.DescuentoAdicional));
            Cabecera.Add(factura.CodigoExcepcion < 0 ? new XElement("codigoExcepcion", nil) : new XElement("codigoExcepcion", factura.CodigoExcepcion));
            Cabecera.Add(factura.Cafc == "" ? new XElement("cafc", nil) : new XElement("cafc", factura.Cafc));
            Cabecera.Add(new XElement("leyenda", factura.Leyenda));
            Cabecera.Add(new XElement("usuario", factura.Usuario));
            // todo Cabecera.Add(new XElement("codigoDocumentoSector", (int)factura.CodigoDocumentoSector));
            Cuerpo.Add(Cabecera);
            // Generamos el Detalle
            foreach (FacturaDetalle detalle in factura.Detalle)
            {
                Detalle = new XElement("detalle");
                Detalle.Add(new XElement("actividadEconomica", detalle.ActividadEconomica));
                Detalle.Add(new XElement("codigoProductoSin", detalle.CodigoProductoSin));
                Detalle.Add(new XElement("codigoProducto", detalle.CodigoProducto));
                Detalle.Add(new XElement("descripcion", detalle.DescripcionProducto));
                Detalle.Add(new XElement("cantidad", detalle.Cantidad));
                Detalle.Add(new XElement("unidadMedida", detalle.UnidadMedida));
                Detalle.Add(new XElement("precioUnitario", detalle.PrecioUnitario));
                Detalle.Add(detalle.MontoDescuento < 0 ? new XElement("montoDescuento", nil) : new XElement("montoDescuento", detalle.MontoDescuento));
                Detalle.Add(new XElement("subTotal", detalle.SubTotal));
                // todo
                // Solo para factura de compra y Venta 
                // if (factura.codigoDocumentoSector == TipoDocumentoSector.CompraYVenta)
                // {
                //     Detalle.Add((detalle.numeroSerie == "" ? new XElement("numeroSerie", nil) : new XElement("numeroSerie", detalle.numeroSerie)));
                //     Detalle.Add((detalle.numeroImei == "" ? new XElement("numeroImei", nil) : new XElement("numeroImei", detalle.numeroImei)));
                // }
                Cuerpo.Add(Detalle);
            }

            #endregion

            XmlDoc.Add(Cuerpo);

            var sb = new StringBuilder();
            var sw = new StringWriterUtf8(sb);
            XmlDoc.Save(sw);
            Xml.LoadXml(sw.ToString());

            // Firmamos el XML
            respuesta = FirmarXML(Xml);
            if (respuesta.Ok)
            {
                XmlDocFirmado = respuesta.Data ?? new XmlDocument();
                // Validamos el XML si cumple con el esquema de Impuestos Nacionales
                mensaje = ValidarXML(XmlDocFirmado, "factura.esquemaDocumentoSector"); // todo
                if (mensaje != "")
                {
                    throw new CustomException(mensaje);
                }
                //guardo el documento en el disco
                var rutaAGuardar = ""; // todo Appsettings.GetValor("FacturasXmlStorage", "Ruta").ToString();
                nombre = rutaAGuardar + "factura.nombre_xml_firmado"; // todo
                XmlTextWriter xmltw = new(nombre, new UTF8Encoding(false));
                xmltw.WriteProcessingInstruction("xml", "version=\"1.0\" encoding=\"utf-8\"");
                XmlDocFirmado.WriteTo(xmltw);
                xmltw.Close();
            }
            else
            {
                throw new CustomException(respuesta.Message.Description);
            }
            resp.Data = true;
            resp.Ok = true;
        }
        catch (CustomException exControl)
        {
            resp.SetMessage(MessageTypes.Error, exControl.Message);
        }
        catch (Exception ex)
        {
            resp.SetLogMessage(MessageTypes.Error, "Ocurrio un error al Crear el XML, por favor comuniquese con Soporte Técnico. " + ex.Message, ex);
        }

        return resp;
    }

    /// <summary>
    /// Metodo que Firma el xml
    /// </summary>
    /// <param name="xml">string del XML</param>
    /// <returns>Retorna on obeto de tipo respuesta donde en datos devuelve el string del XML firmado</returns>
    protected static Response<XmlDocument> FirmarXML(XmlDocument Xmldoc)
    {
        Response<XmlDocument> respuesta = new();
        string Certificate, private_Key, privateKey;
        // XmlDocument doc = new XmlDocument();
        RSA rsaKey = RSA.Create();

        try
        {
            Certificate = ""; // todo Comun.Herramientas.Appsettings.GetValor("Firma", "certificado") ?? "";
            private_Key = "";  // todo Comun.Herramientas.Appsettings.GetValor("Firma", "privateKey") ?? "";
            privateKey = System.IO.File.ReadAllText(private_Key);
            rsaKey.ImportFromPem(privateKey.ToCharArray());

            // --------- FIRMADO X509 ----------------
            // Create a new XML document.
            Xmldoc.PreserveWhitespace = false;            // formateo el documento para que ignore los espacios
                                                          // doc.Load(new XmlTextReader(xml));     // recargo el archivo
                                                          // Xmldoc.LoadXml(xml);

            SignedXml signedXml = new(Xmldoc) { SigningKey = rsaKey }; // a este documento firmado le agrego la llave y  // creo el objto del documento como firmado?
            Reference reference = new() { Uri = "" };      // creo una referencia para firmar
            XmlDsigEnvelopedSignatureTransform env = new();  // Creo En sobre para el XML -> env
            reference.AddTransform(env);            //a esta referencia le agrego una transformación de tipo envelope
            signedXml.AddReference(reference);      // y ahora esta referencia se la asigno al documento firmado

            KeyInfo keyInfo = new KeyInfo();        // Ahora tengo que crear un objeto para firmar

            // keyInfo.AddClause(new RSAKeyValue((RSA)rsaKey)); //agrego la firma RSA

            X509Certificate MSCert = X509Certificate.CreateFromCertFile(Certificate);   // Descargamos el certificado
            keyInfo.AddClause(new KeyInfoX509Data(MSCert));


            signedXml.KeyInfo = keyInfo;
            signedXml.ComputeSignature();
            XmlElement xmlDigitalSignatureRSA = signedXml.GetXml(); //elemento con la firma RSA y X509

            // Agrego al documento un hijo con las llaves
            Xmldoc.DocumentElement.AppendChild(Xmldoc.ImportNode(xmlDigitalSignatureRSA, true));

            if (Xmldoc.FirstChild is XmlDeclaration)
            {
                Xmldoc.RemoveChild(Xmldoc.FirstChild);
            }

            respuesta.Data = Xmldoc;
            respuesta.Ok = true;
        }
        catch (Exception ex)
        {
            respuesta.SetLogMessage(MessageTypes.Error, "Ocurrio un error al firmar el XML, por favor comuniquese con Soporte Técnico. " + ex.Message, ex);
            respuesta.Ok = false;
        }
        return respuesta;
    }

    /// <summary>
    /// Metodo que valida si la cadena XML cumple con el XSD de impuestos
    /// </summary>
    /// <param name="Xmldoc">Documento XML a ser verificado</param>
    /// <param name="esquema">Nombre del esquema con el cual se validara</param>
    /// <returns>Si todo esta correcto retorna una cadena vacia, si hay alguna observación a la validación retorna la observación.</returns>
    protected static string ValidarXML(XmlDocument Xmldoc, string esquema)
    {
        string path_XSD = ""; // todo Comun.Herramientas.Appsettings.GetValor("Firma", "path_xsd") ?? "";
        string respuesta = "";

        try
        {
            string xsdValidar = path_XSD + esquema + ".xsd";
            string xsdSignature = path_XSD + "SignatureSchema.xsd";
            Xmldoc.Schemas.Add(null, xsdValidar);
            Xmldoc.Schemas.Add("http://www.w3.org/2000/09/xmldsig#", xsdSignature);
            try
            {
                Xmldoc.Validate(null);
            }
            catch (XmlSchemaValidationException ex)
            {
                // Retorna el error en la validación
                respuesta = "La estructura XML no cumple las especificaciones XSD de Impuestos Nacionales. " + ex.Message;
            }
        }
        catch (Exception eex)
        {
            respuesta = eex.Message;
        }

        return respuesta;
    }

}

public class StringWriterUtf8(StringBuilder sb) : StringWriter(sb)
{
    public override Encoding Encoding
    {
        get { return Encoding.UTF8; }
    }
}

