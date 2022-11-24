using AutoMapper;
using ExoAPI.Context;
using ExoAPI.Dto;
using ExoAPI.Entitie;
using ExoAPI.Mapping;
using ExoAPI.Type;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ExoAPI.Controllers;
    [ApiController] 
    [Route("[Controller]")]
public class ProductsController : ControllerBase
{
    private readonly IMapper _mapper;
    private readonly ProduitContext _context;
    private readonly BusinessContext _businessContext;
    public ProductsController(ProduitContext context, IMapper mapper, BusinessContext businessContext)
    {
        _mapper = mapper;
        _context = context;
        _businessContext = businessContext;
    }

    [HttpGet("ShowStock"), Authorize]
    public IActionResult ShowStock()
    {
        return Ok(_mapper.Map<List<ProductDto>>(_businessContext.Products.ToList()));
    }
    [HttpGet("GetById/{id}")]
    public IActionResult GetProductID(int id)
    {
        return Ok(_mapper.Map<ProductDto>(_businessContext.Products.First(x=> x.Id==id)));
    }
    [HttpPost("addProduct")]
    public IActionResult AddProduit([FromBody] ProductDto productDto)
    {   
        Product product = _mapper.Map<Product>(productDto);
        _businessContext.Products.Add(product);
        _businessContext.SaveChanges();
        //_context.NewProduct(productDto);
        return Ok(productDto);
    }
    [HttpPut("EditProduct/{id}")]
    public IActionResult ProductEdit(int id, ProductDto productDto)
    {
        Product product = _mapper.Map<Product>(productDto);
        //Product mProduct = _businessContext.Products.First(x => x.Id == id);
        //mProduct.Origin = product.Origin;
        //mProduct.Name = product.Name;
        //mProduct.Quantite = product.Quantite;
        //mProduct.Usage = product.Usage;
        _businessContext.Products.Update(product);
        _businessContext.SaveChanges();
        //_context.EditProduct(id,productDto);
        return Ok(productDto);
    }
    [HttpDelete("deleteProduct/{id}")]
    public IActionResult DelProduct(int id)
    {
        Product product = _businessContext.Products.First(x => x.Id == id);
        _businessContext.Products.Remove(product);
        _businessContext.SaveChanges();
        //_context.ProductDelete(id);
        return Ok(_mapper.Map<List<ProductDto>>(_businessContext.Products.ToList()));
    }
    
}