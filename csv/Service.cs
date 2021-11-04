using Serilog;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace csv
{
    class Service
    {
        public void ExecuteAsync()
        {
            /*
             * (InputData) ->  Faz a leitura dos dados no banco para geração do layout 
             */
            Repo r = new Repo();

            var result =  r.GetDatabaseDataAsync();
            Console.WriteLine(result);
            /*
             * (Validation, Formatting) ->  Faz a validação e formatação dos dados recebidos de acordo com o layout do clientr
             */
            var csvData = new StringBuilder();
            

            foreach (var line in result)
            {
                csvData.AppendFormat(
                    "\t{0};", line.DataUsar.ToString("dd-MM-yyyy")
                   

                );
                csvData.AppendLine();

                Console.WriteLine("Processando linha {linha}" + csvData.ToString());
            }

            /*
             * (Processing) => Salva os dados dos registros no banco de dados
             */         

            /*
             * (OutputData) => Gera o arquivo no formato esperado
             */
            var fileName =
                @"C:\workspace\pocs\Logs\OPTUM_BENEFICIARIOS_" + DateTime.Now.ToString("MM-yyyy") + ".CSV";


            File.WriteAllText(
                fileName,
                csvData.ToString());

            Log.Information("Gerou o arquivo  => {filename}", fileName);
        }
    }
}
