using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace Rastreio
{



    class Program
    {

        static void Main(string[] args)
        {

            var usuario = "ECT";
            var senha = "SRO";
            var tipo = "L";
            var resultado = "T";
            var lingua = "101";
            var objetos = "DV529144490BR";

            var correios = new Correios();

            var result = correios.ConsultarCodigoRastreio(usuario,senha,tipo,resultado,lingua,objetos);
        }

    }
}
