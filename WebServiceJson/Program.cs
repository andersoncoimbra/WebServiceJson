using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace WebServiceJson
{
    class Program
    {
        static void Main(string[] args)
        {
            carragadorDadosRemotoAssicrono();
                      
            Console.ReadKey();


            

        }

        public static async Task carragadorDadosRemotoAssicrono()
        {
            var client = new HttpClient();
            client.MaxResponseContentBufferSize = 1024 * 1024;//limitede conteudo por 1Mb
            //json.txt deve esta codificada em UTF-8 caso contrario pode apresentar erro de leitura
            //[{"nome":"Anderson", "idade":"24", "cidade":"Ananindeua"},{"nome":"José", "idade":"34", "cidade":"Belém"},{"nome":"Maria", "idade":"54", "cidade":"Rio de janeiro"},{"nome":"Roberto", "idade":"54", "cidade":"Rio de janeiro"},{"nome":"Pedro", "idade":"54", "cidade":"Rio de janeiro"},{"nome":"Chico", "idade":"54", "cidade":"Rio de janeiro"}]
            var response = await client.GetAsync(new Uri("http://localhost/webservice/json.txt"));
            var result = await response.Content.ReadAsStringAsync();
            if (result.Length < 2)
            {
                Console.WriteLine("Nenhum cadastro");
                Console.WriteLine(result.Length);
            }
            
            Debug.WriteLine("------  "+result);
            List<Pessoas> pessoas = JsonConvert.DeserializeObject<List<Pessoas>>(result);
            Console.WriteLine("Total de registros "+pessoas.Count);
            Console.WriteLine("~~~~~~~~~~~~~~");
            int n = 0;
            while (n < pessoas.Count)
            {
                Console.WriteLine(pessoas[n].Nome);
                Console.WriteLine(pessoas[n].Idade);
                Console.WriteLine(pessoas[n].Cidade);
                Console.WriteLine("~~~~~~~~~~~~~~");
                n++;
            }
            string type;
            while (Console.ReadKey().ToString() != "i") {
                type = Console.ReadKey().ToString();
            }

           
        }

       

        public static string toUTF8(string response)
        {
            string junk = response;  // Unicode

            // Turn string back to bytes using the original, incorrect encoding.
            byte[] bytes = Encoding.GetEncoding(1252).GetBytes(junk);

            // Use the correct encoding this time to convert back to a string.
            string good = Encoding.ASCII.GetString(bytes);
            //Console.WriteLine(good);
            return good.ToString();
        }


        
    }
}
