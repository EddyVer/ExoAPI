namespace ExoAPI.Context;
using ExoAPI.Entitie;
using ExoAPI.Dto;
public class ProduitContext
{
    public List<Product> Products { get; set; }
    public List<ProductDto> ProductDtos { get; set; }
    public List<Product> list() => Products;

    public List<Product> FonctionGet(string fonction)
    {
        return Products.Where(x => x.Usage == fonction).ToList();
    }

    public void NewProduct(ProductDto productDto)
    {
        productDto.Id = ProductDtos.Count + 1;
        ProductDtos.Add(productDto);
    }

    public void EditProduct(int id, ProductDto productDto)
    {
        var mProduct = Products.First(x => x.Id == id);
        mProduct.Origin = productDto.Origin;
        mProduct.Name = productDto.Name;
        mProduct.Quantite = productDto.Quantite;
        mProduct.Usage = productDto.Usage;

    }
    
    public ProduitContext()
    {
        Products = new List<Product>();
        Products.Add(new Product()
        {
            Id = 1,
            Origin = "Frensh",
            Name = "Cable Eternet",
            Quantite = 25,
            Usage = "Informatique"
        });
        Products.Add(new Product()
        {
            Id = 2,
            Origin = "Belguim",
            Name = "Grafique Card",
            Quantite = 60,
            Usage = "Informatique"
        });
        Products.Add(new Product()
        {
            Id = 3,
            Origin = "Taiwan",
            Name = "Trottinette Electric",
            Quantite = 4,
            Usage = "Transport"
            
        });
        Products.Add(new Product()
        {
            Id = 4,
            Origin = "China",
            Name = "UnTrucUtile",
            Quantite = 5,
            Usage = "Informatique"
        });
    }
}