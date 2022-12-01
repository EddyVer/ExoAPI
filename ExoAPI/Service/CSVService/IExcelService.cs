namespace ExoAPI.Service.CSVService;

public interface IExcelService
{
    //, string records
    public void WriteCSV<T>(T[] element) ;
}