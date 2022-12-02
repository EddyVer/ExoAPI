namespace ExoAPI.Service.CSVService;

public interface IExcelService { 
    public void WriteCSV<T>(T[] element) ;
}