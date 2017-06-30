//Classe baseada no link: https://stackoverflow.com/questions/11958825/c-sharp-creating-instance-of-class-and-set-properties-by-name-in-string

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Rastreio
{
    public class Evento
    {
        public String Tipo { get; set; }
        public String Status { get; set; }
        public String Data { get; set; }
        public String Hora { get; set; }
        public String Descricao { get; set; }
        public String Local { get; set; }
        public String Codigo { get; set; }
        public String Cidade { get; set; }
        public String UF { get; set; }
    }

    public class Objeto
    {
        public String Numero { get; set; }
        public String Sigla { get; set; }
        public String Nome { get; set; }
        public String Categoria { get; set; }
        public Evento evento { get; set; }
        public String Erro { get; set; }
    }

    public class Retorno
    {
        public string Versao { get; set; }
        public string Qtd { get; set; }
        public Objeto objeto { get; set; }
    }
        

    public class DeserializeRastreio
    {
        public Retorno retorno { get; set; }
        
        public DeserializeRastreio(String xml)
        {
            string[] stringSeparators = new string[] { "><" };
            string[] lines = xml.Split(stringSeparators, StringSplitOptions.None);
            

            var objetoReturn = RecuperarObjetoReturn(lines);

            this.retorno = objetoReturn;
        }

        private Retorno RecuperarObjetoReturn(string[] lines)
        {
            object instance = null;
            Boolean stop = false;
            for (int i = 0; i < lines.Count(); i++)
            {
                if(lines[i].Contains("return"))
                {
                    Type type = Type.GetType("Rastreio.Retorno", true);
                    instance = Activator.CreateInstance(type);
                    int qtd = i + 1;
                    while(!lines[qtd].Contains("return"))
                    {
                        string line = lines[qtd];
                        if(line.Contains("versao"))
                        {                            
                            PropertyInfo prop = type.GetProperty("Versao");
                            prop.SetValue(instance, GetValue(line), null);                            
                        }

                        if (line.Contains("qtd"))
                        {
                            PropertyInfo prop = type.GetProperty("Qtd");
                            prop.SetValue(instance, GetValue(line), null);                            
                        }

                        if(line.Contains("objeto"))
                        {
                            var tuplaObjeto = DeserializeObjeto(lines, instance, qtd);
                            ((Retorno)instance).objeto = tuplaObjeto.Item1;
                            qtd = tuplaObjeto.Item2;
                        }

                        qtd++;
                    }

                    stop = true;
                }

                if (stop)
                    break;

                
            }

            return ((Retorno)instance);
        }

        private Tuple<Objeto,int> DeserializeObjeto(string[] lines, object instance, int qtd)
        {
            Type typeObjeto = Type.GetType("Rastreio.Objeto", true);
            object instanceObjeto = Activator.CreateInstance(typeObjeto);
            int qtdObjeto = qtd + 1;
            while (!lines[qtdObjeto].Contains("objeto"))
            {
                string lineObjeto = lines[qtdObjeto];
                if (lineObjeto.Contains("numero"))
                {
                    PropertyInfo prop = typeObjeto.GetProperty("Numero");
                    prop.SetValue(instanceObjeto, GetValue(lineObjeto), null);

                }
                if (lineObjeto.Contains("sigla"))
                {
                    PropertyInfo prop = typeObjeto.GetProperty("Sigla");
                    prop.SetValue(instanceObjeto, GetValue(lineObjeto), null);

                }
                if (lineObjeto.Contains("nome"))
                {
                    PropertyInfo prop = typeObjeto.GetProperty("Nome");
                    prop.SetValue(instanceObjeto, GetValue(lineObjeto), null);

                }
                if (lineObjeto.Contains("categoria"))
                {
                    PropertyInfo prop = typeObjeto.GetProperty("Categoria");
                    prop.SetValue(instanceObjeto, GetValue(lineObjeto), null);

                }
                if (lineObjeto.Contains("erro"))
                {
                    PropertyInfo prop = typeObjeto.GetProperty("Erro");
                    prop.SetValue(instanceObjeto, GetValue(lineObjeto), null);

                }

                if (lineObjeto.Contains("evento"))
                {
                    var tuplaEvento = DeserializeEvento(lines, qtdObjeto);
                    ((Objeto)instanceObjeto).evento = tuplaEvento.Item1;
                    qtdObjeto = tuplaEvento.Item2;
                }

                qtdObjeto++;

            }

           Objeto objeto = ((Objeto)instanceObjeto);
            return new Tuple<Objeto,int>(objeto, qtdObjeto);
        }

        private Tuple<Evento, int> DeserializeEvento(string[] lines, int qtd)
        {
            Type typeObjeto = Type.GetType("Rastreio.Evento", true);
            object instance = Activator.CreateInstance(typeObjeto);
            int qtdEvento = qtd + 1;
            while (!lines[qtdEvento].Contains("evento"))
            {
                string lineObjeto = lines[qtdEvento];
                if (lineObjeto.Contains("tipo"))
                {
                    PropertyInfo prop = typeObjeto.GetProperty("Tipo");
                    prop.SetValue(instance, GetValue(lineObjeto), null);

                }
                if (lineObjeto.Contains("status"))
                {
                    PropertyInfo prop = typeObjeto.GetProperty("Status");
                    prop.SetValue(instance, GetValue(lineObjeto), null);

                }
                if (lineObjeto.Contains("data"))
                {
                    PropertyInfo prop = typeObjeto.GetProperty("Data");
                    prop.SetValue(instance, GetValue(lineObjeto), null);

                }
                if (lineObjeto.Contains("hora"))
                {
                    PropertyInfo prop = typeObjeto.GetProperty("Hora");
                    prop.SetValue(instance, GetValue(lineObjeto), null);

                }
                if (lineObjeto.Contains("descricao"))
                {
                    PropertyInfo prop = typeObjeto.GetProperty("Descricao");
                    prop.SetValue(instance, GetValue(lineObjeto), null);

                }
                if (lineObjeto.Contains("local"))
                {
                    PropertyInfo prop = typeObjeto.GetProperty("Local");
                    prop.SetValue(instance, GetValue(lineObjeto), null);

                }
                if (lineObjeto.Contains("codigo"))
                {
                    PropertyInfo prop = typeObjeto.GetProperty("Codigo");
                    prop.SetValue(instance, GetValue(lineObjeto), null);

                }
                if (lineObjeto.Contains("cidade"))
                {
                    PropertyInfo prop = typeObjeto.GetProperty("Cidade");
                    prop.SetValue(instance, GetValue(lineObjeto), null);

                }
                if (lineObjeto.Contains("uf"))
                {
                    PropertyInfo prop = typeObjeto.GetProperty("UF");
                    prop.SetValue(instance, GetValue(lineObjeto), null);

                }

                qtdEvento++;

            }

            Evento objeto = ((Evento)instance);
            return new Tuple<Evento, int>(objeto, qtdEvento);
        }

        private string GetValue(string line)
        {
            int[] indexes = recuperarIndexes(line);
            var value = line.Substring(indexes[0], indexes[1]);
            return value;
        }

        private int[] recuperarIndexes(string line)
        {
            int start = line.IndexOf(">") + 1;
            int qtd =  line.IndexOf("<") - start;

            return new int[] { start, qtd };
        }

    }
}
