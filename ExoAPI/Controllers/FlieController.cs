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

    [HttpGet("{data}")]
    public async Task<ActionResult> ReadFile(string data)
    {
        //data = data.Replace(" ", ";");
        _excelService.WriteCSV(data);
        return Ok(data);
    }
}