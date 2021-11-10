using Serilog;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using OfficeOpenXml;
using System.Globalization;

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

            

            var fileName =
               @"C:\workspace\pocs\Logs\OPTUM_BENEFICIARIOS_" + DateTime.Now.ToString("MM-yyyy") + ".CSV";
          

            foreach (var line in result)
            {
                csvData.AppendFormat(
                    "{0};{1}",  line.DataUsar.ToString("dd-MM-yyyy"), line.DataUsar.ToString("dd-MM-yyyy")


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
           

               

           

            

            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            //Creating empty Excel Package...
            var package = new ExcelPackage();

            //Referencing the Workbook...
            var workbook = package.Workbook;

            var format = new ExcelTextFormat()
            {
                Delimiter = ';',
                Culture = CultureInfo.GetCultureInfo("pt-BR"),

                // Escape character for values containing the Delimiter
                // ex: "A,Name",1 --> two cells, not three
                TextQualifier = '"'

                // Other properties
                // EOL, DataTypes, Encoding, SkipLinesBeginning/End
            };

            //Adding a new sheet named "SheetName"...
            var sheet = workbook.Worksheets.Add("OPTUM_BENEFICIARIOS_" + DateTime.Now.ToString("MM - yyyy") + ".CSV");

            //Formatting column D to show values as Datetime
            sheet.Cells["A:A"].Style.Numberformat.Format = "dd-MM-yyyy";
            sheet.Cells["B:B"].Style.Numberformat.Format = "MM-yyyy";

            sheet.Cells.LoadFromText(csvData.ToString(), format);

            package.SaveAs(fileName);


            Log.Information("Gerou o arquivo  => {filename}", fileName);


        }
    }
}
