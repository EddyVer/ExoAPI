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
       List<Product> result = Products.FindAll(delegate(Product product) 
           { return product.Usage == fonction; });
       return result;
    }

    public void NewProduct(Product product)
    {
        product.Id = Products.Count + 1;
        Products.Add(product);
    }

    public void EditProduct(int id, Product product)
    {
        var mProduct = Products.First(x => x.Id == id);
        mProduct.Origin = product.Origin;
        mProduct.Name = product.Name;
        mProduct.Quantite = product.Quantite;
        mProduct.Usage = product.Usage;

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
            Name = "",
            Quantite = 5,
            Usage = "Informatique"
        });
    }
}