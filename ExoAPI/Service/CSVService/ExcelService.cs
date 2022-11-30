using System.Globalization;
using CsvHelper;

namespace ExoAPI.Service.CSVService;

public class ExcelService : IExcelService
{
    public void WriteCSV(string records)
    {
        using (System.IO.StreamWriter writer = new System.IO.StreamWriter("/home/melonde-zeus/RiderProjects/ExoAPI/ExoAPI/Download/file.csv",true))
        using ( CsvWriter csv = new CsvWriter(writer, CultureInfo.InvariantCulture)) 
         {
             var columns = records.Split(" ");
             writer.WriteLine(string.Join(",", columns));
         }
    }


}