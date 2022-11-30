using ExoAPI.Service.CSVService;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;

namespace ExoAPI.Controllers;
[ApiController]
[Route("[Controller]")]
public class FileController : ControllerBase
{
    private readonly IExcelService _excelService;
    public FileController(IExcelService excelService)
    {
        _excelService = excelService;
    }
    [HttpGet("files/{name}")]
    public async Task<PhysicalFileResult> DownloadFile(string name)
    {
        string filePath = $"Download/{name}.txt"; // Here, you should validate the request and the existance of the file.
        var provider = new FileExtensionContentTypeProvider();
        if (!provider.TryGetContentType(filePath, out var contentType))
        {
            contentType = "application/octet-stream";
        }
       return PhysicalFile(filePath, contentType);
    }
    //"{data}"string data data
    [HttpGet()]
    public async Task<ActionResult> ReadFile()
    {
        //var testColumns = data.Split(' ');
        Test[] test = new[] { new Test(){
        Id= 1,
        Name= "test1"
        },
        new Test(){ Id= 2,Name= "test2"}
        }; 
        //data = data.Replace(" ", ";");
        _excelService.WriteCSV(test );
        return Ok();
    }
}
public class Test
{
    public int Id { get; set; }
    public string Name { get; set; }
}