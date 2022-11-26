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
            return Ok(_mapper.Map<List<EntrepotDto>>(_businessContext.Entrepots.Include(x => x.Products).ToList()));
        }

        [HttpGet("userEntrepot/{id}")]
        public async Task<ActionResult<string>> ShowEntrepotUser(int id)
        {
            User user = _businessContext.Users.Include(x => x.Entrepots).First(x => x.Id == id);
            UserDto dto = _mapper.Map<UserDto>(user);
            return Ok(dto.Entrepots);
        }
        [HttpGet("ById/{id}")]
        public IActionResult ShowById(int id)
        {
            return Ok(_mapper.Map<EntrepotDto>(_businessContext.Entrepots.Include(x=>x.Products).First(x => x.Id == id)));
        }
        [HttpGet("ByProduct/{name}")]
        public IActionResult GetEntrepotByProduct(string name)
        {
            return Ok(_mapper.Map<List<EntrepotDto>>(_businessContext.Entrepots.Include(x => x.Products).Where(x => x.Name == name).ToList()));
        }
        [HttpPost("AddEntrepot")]
        public IActionResult AddEntrepot([FromBody] EntrepotDto entrepotDto)
        {
            Entrepot entrepot = _mapper.Map<Entrepot>(entrepotDto);
            _businessContext.Entrepots.Add(entrepot);
            _businessContext.SaveChanges();
            return Ok(entrepotDto);
        }
        [HttpPut("AddProductEntrepot/{id}")]
        public IActionResult Addproduct(int id, [FromBody] ProductDto productDto)
        {
            Product product = _mapper.Map<Product>(productDto);
            Entrepot entrepot = _businessContext.Entrepots.Include(x=>x.Products).First(x => x.Id == id);
            if (entrepot.Products == null)
            {
                entrepot.Products = new List<Product>();
            }
            entrepot.Products.Add(product);
            _businessContext.Entrepots.Update(entrepot);
            _businessContext.SaveChanges();
            EntrepotDto entrepotDto = _mapper.Map<EntrepotDto>(entrepot);
            return Ok(entrepotDto);
        }

        [HttpPut("modifProductByEntrepot/{idEntrepot}/{idProduct}")]
        public IActionResult ProductModif(int idProduct,int idEntrepot, ProductDto productDto)
        {
            Product product = _mapper.Map<Product>(productDto);
            Entrepot entrepot = _businessContext.Entrepots.Include(x => x.Products).First(x => x.Id == idEntrepot);
            Product productEntrepot = entrepot.Products.First(x => x.Id == idProduct);
            productEntrepot.Origin = product.Origin;
            productEntrepot.Name = product.Name;
            productEntrepot.Quantite = product.Quantite;
            productEntrepot.Usage = product.Usage;
            _businessContext.Entrepots.Update(entrepot);
            _businessContext.SaveChanges();
            return Ok(_mapper.Map<List<EntrepotDto>>(entrepot));
        }

        [HttpDelete("deleteProduct/{idEntrepot}/{idProduct}")]
        public IActionResult DelProduct(int idEntrepot,int idProduct)
        {
            Entrepot entrepot = _businessContext.Entrepots.Include(x => x.Products).First(x => x.Id == idEntrepot);
            Product product = entrepot.Products.First(x => x.Id == idProduct);
            entrepot.Products.Remove(product);
            _businessContext.Products.Remove(product);
            _businessContext.Entrepots.Update(entrepot);
            _businessContext.SaveChanges();
            EntrepotDto entrepotDto = _mapper.Map<EntrepotDto>(entrepot);
            return Ok(entrepotDto);
        }
        [HttpDelete("DeleteEntrepot/{id}")]
        public IActionResult DeleteEntrepot(int id)
        {
           Entrepot entrepot = _businessContext.Entrepots.First(x => x.Id == id);
            _businessContext.Entrepots.Remove(entrepot);
            _businessContext.SaveChanges();
            return Ok(_mapper.Map<List<EntrepotDto>>(_businessContext.Entrepots.ToList()));
        }
    }
    
}
