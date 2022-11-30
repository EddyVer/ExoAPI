using System.Globalization;
using System.Reflection;
using CsvHelper;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace ExoAPI.Service.CSVService;

public class ExcelService : IExcelService
{
    //,string records
    public void WriteCSV<T>(T [] element)
    {
        PropertyInfo[] fields = typeof(T).GetProperties();
        using (System.IO.StreamWriter writer = new System.IO.StreamWriter("C:\\Users\\Vervo\\source\\repos\\ExoAPI\\ExoAPI\\Download\\file.csv", true))
        //using ( CsvWriter csv = new CsvWriter(writer, CultureInfo.InvariantCulture)) 
         {
             //var columns = records.Split(" ");
             writer.WriteLine(string.Join(";", fields.Select(x=>x.Name).GetType()));
          // writer.WriteLine(string.Join(";",fields.GetValue()));
         }
    }
}