//Classe implementada utilizando o manual dos correios https://www.correios.com.br/para-voce/correios-de-a-a-z/pdf/rastreamento-de-objetos/manual_rastreamentoobjetosws.pdf

using System.IO;
using System.Net;
using System.Text;
using System.Xml;

namespace Rastreio
{
    public class Correios
    {
        public Retorno ConsultarCodigoRastreio(string usuario, string senha, string tipo, string resultado, string lingua, string objetos)
        {
            var _url = "http://webservice.correios.com.br/service/rastro";

            XmlDocument soapEnvelopeXml = CreateSoapEnvelope(usuario,senha,tipo,resultado,lingua,objetos);
            HttpWebRequest webRequest = CreateWebRequest(_url);
            InsertSoapEnvelopeIntoWebRequest(soapEnvelopeXml, webRequest);


            string soapResult = "";
            using (WebResponse response = webRequest.GetResponse())
            {

                using (StreamReader rd = new StreamReader(response.GetResponseStream()))
                {
                    soapResult = rd.ReadToEnd();

                    return new DeserializeRastreio(soapResult).retorno;
                }
            }

        }

        private static HttpWebRequest CreateWebRequest(string url)
        {
            HttpWebRequest webRequest = (HttpWebRequest)WebRequest.Create(url);
            webRequest.Headers.Add(@"SOAP:Action");
            webRequest.ContentType = "text/xml;charset=\"utf-8\"";
            webRequest.Accept = "text/xml";
            webRequest.Method = "POST";
            return webRequest;
        }

        private static XmlDocument CreateSoapEnvelope(string usuario, string senha, string tipo, string resultado, string lingua, string objetos)
        {

            var xml = new StringBuilder();
            var soapenv = "http://schemas.xmlsoap.org/soap/envelope/";
            var res = "http://resource.webservice.correios.com.br/";
            xml.Append(@"<?xml version=""1.0"" encoding=""utf-8""?>");
            xml.AppendFormat(@" <soapenv:Envelope xmlns:soapenv=""{0}"" xmlns:res=""{1}"">", soapenv, res);
            xml.Append(@"<soapenv:Header/> <soapenv:Body> <res:buscaEventos>");
            xml.AppendFormat(@"<usuario>{0}</usuario>",usuario);
            xml.AppendFormat(@"<senha>{0}</senha>",senha);
            xml.AppendFormat(@"<tipo>{0}</tipo>", tipo);
            xml.AppendFormat(@"<resultado>{0}</resultado>",resultado);
            xml.AppendFormat(@"<lingua>{0}</lingua>",lingua);
            xml.AppendFormat(@"<objetos>{0}</objetos>",objetos);
            xml.Append(@"</res:buscaEventos> </soapenv:Body></soapenv:Envelope>");
            XmlDocument soapEnvelop = new XmlDocument();
            soapEnvelop.LoadXml(xml.ToString());
            ;
            return soapEnvelop;
        }

        private static void InsertSoapEnvelopeIntoWebRequest(XmlDocument soapEnvelopeXml, HttpWebRequest webRequest)
        {
            using (Stream stream = webRequest.GetRequestStream())
            {
                soapEnvelopeXml.Save(stream);
            }
        }
    }
}
