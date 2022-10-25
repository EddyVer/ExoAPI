using ExoAPI.Dto;
using ExoAPI.Entitie;
using ExoAPI.Type;
using AutoMapper;

namespace ExoAPI.Context;

public class ProduitContext
{
    private readonly IMapper _mapper;
    public List<Product> Products { get; set; }
    public List<ProductDto> ProductDtos { get; set; }
    public List<Product> list() => Products;
    public List<UsagesCollection> Collects { get; set; }
    public List<UsagesCollectionDto> CollectDtos { get; set; }

    public Product FonctionGet(int id) =>  Products.First(x => x.Id == id);

    public void NewProduct(ProductDto productDto)
    {
        Product product = _mapper.Map<Product>(productDto);
        product.Id = Products.Count + 1;
        Products.Add(product);
    }

    public void NewType(UsagesCollectionDto dto)
    {
        dto.Id = CollectDtos.Count + 1 ;
        UsagesCollection usagesCollection = _mapper.Map<UsagesCollection>(dto);
        Collects.Add(usagesCollection);
    }

    public Product EditProduct(int id, ProductDto productDto)
    {
        var mProduct = Products.First(x => x.Id == id);
        mProduct = _mapper.Map<Product>(productDto);
        mProduct.Origin = productDto.Origin;
        mProduct.Name = productDto.Name;
        mProduct.Quantite = productDto.Quantite;
        mProduct.Usage = productDto.Usage;
        return mProduct;

    }
    
    public ProduitContext(IMapper mapper)
    {
        _mapper = mapper;
        Products = new List<Product>();
        Products.Add(new Product()
        {
            Id = 1,
            Origin = "Frensh",
            Name = "Cable Eternet",
            Quantite = 25,
            Usage = Usages.Informatique
        });
        Products.Add(new Product()
        {
            Id = 2,
            Origin = "Belguim",
            Name = "Grafique Card",
            Quantite = 60,
            Usage =(Usages)1
        });
        Products.Add(new Product()
        {
            Id = 3,
            Origin = "Taiwan",
            Name = "Trottinette Electric",
            Quantite = 4,
            Usage = Usages.Transport

        });
        Products.Add(new Product()
        {
            Id = 4,
            Origin = "China",
            Name = "UnTrucUtile",
            Quantite = 5,
            Usage = Usages.Recherche
        });
    }
}