using AutoMapper;
using ExoAPI.Context;
using ExoAPI.Dto;
using ExoAPI.Entitie;
using ExoAPI.Mapping;
using ExoAPI.Type;
using Microsoft.AspNetCore.Mvc;

namespace ExoAPI.Controllers;
    [ApiController] 
    [Route("[Controller]")]
public class ProductsController : ControllerBase
{
    private readonly IMapper _mapper;
    private readonly ProduitContext _context;
    public ProductsController(ProduitContext context, IMapper mapper)
    {
        _mapper = mapper;
        _context = context;
    }

    [HttpGet("ShowStock")]
    public IActionResult ShowStock()
    {
        return Ok(_mapper.Map<List<ProductDto>>(_context.list()));
    }

    [HttpGet("GetById/{id}")]
    public IActionResult GetProductID(int id)
    {
        return Ok( _mapper.Map<ProductDto>( _context.FonctionGet(id)));
    }

    [HttpPost("addProduct")]
    public IActionResult AddProduit([FromBody] ProductDto productDto)
    {
        _context.NewProduct(productDto);
        return Ok(productDto);
    }
    [HttpPost("addType")]
    public IActionResult AddType([FromBody]UsagesCollectionDto dto)
    {
        _context.NewType(dto);
        return Ok(dto);
    }
    [HttpPut("EditProduct/{id}")]
    public IActionResult ProductEdit(int id, ProductDto productDto)
    {
        _context.EditProduct(id,productDto);
        return Ok(productDto);
    }
}