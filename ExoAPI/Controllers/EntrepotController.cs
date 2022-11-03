using AutoMapper;
using ExoAPI.Context;
using ExoAPI.Dto;
using ExoAPI.Entitie;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ExoAPI.Controllers
{
    [ApiController]
    [Route("[Controller]")]
    public class EntrepotController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly BusinessContext _businessContext;
        public EntrepotController(IMapper mapper, BusinessContext businessContext)
        {
            _mapper = mapper;
            _businessContext = businessContext;
        }

        [HttpGet("ShowEntrepot")]
        public IActionResult ShowEntrepot()
        {
            return Ok(_mapper.Map<List<EntreportDto>>(_businessContext.Entrepots.Include(x => x.products).ToList()));
        }
        [HttpGet("ById/{id}")]
        public IActionResult ShowById(int id)
        {
            return Ok(_mapper.Map<EntreportDto>(_businessContext.Entrepots.First(x => x.Id == id)));
        }
        [HttpGet("ByProduct/{name}")]
        public IActionResult GetEntrepotByProduct(string name)
        {
            return Ok(_mapper.Map<List<EntreportDto>>(_businessContext.Entrepots.Include(x => x.products).Where(x => x.Name == name).ToList()));
        }
        [HttpPost("AddEntrepot")]
        public IActionResult AddEntrepot([FromBody] EntreportDto entreportDto)
        {
            Entrepot entrepot = _mapper.Map<Entrepot>(entreportDto);
            _businessContext.Entrepots.Add(entrepot);
            _businessContext.SaveChanges();
            return Ok(entreportDto);
        }
        [HttpPut("ModifEntrepot/{id}")]
        public IActionResult ModifEntrepot(int id, [FromBody] EntreportDto entreportDto)
        {
            Entrepot entrepot = _mapper.Map<Entrepot>(entreportDto);
            _businessContext.Entrepots.Update(entrepot);
            _businessContext.SaveChanges();
            return Ok(entreportDto);
        }
        [HttpDelete("DeleteEntrepot/{id}")]
        public IActionResult DeleteEntrepot(int id)
        {
           Entrepot entrepot = _businessContext.Entrepots.First(x => x.Id == id);
            _businessContext.Entrepots.Remove(entrepot);
            _businessContext.SaveChanges();
            return Ok(_mapper.Map<List<ProductDto>>(_businessContext.Products.ToList()));
        }
    }
    
}
