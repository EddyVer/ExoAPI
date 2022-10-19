using ExoAPI.Entitie;
using ExoAPI.Dto;
using AutoMapper;
using ExoAPI.Type;

namespace ExoAPI.Context;

public class ProduitContext
{
    private readonly IMapper _mapper;
    public List<Product> Products { get; set; }
    public List<ProductDto> ProductDtos { get; set; }
    public List<Product> list() => Products;
    public List<UsagesCollection> Collects { get; set; }
    public List<UsagesCollectionDto> CollectDtos { get; set; }

    public List<Product> FonctionGet(Usages usage)
    {
        return Products.Where(x => x.Usage == usage).ToList();
    }

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

    public void EditProduct(int id, ProductDto productDto)
    {
        var mProduct = Products.First(x => x.Id == id);
        //mProduct.Origin = productDto.Origin;
        //mProduct.Name = productDto.Name;
        ////mProduct.Quantite = productDto.Quantite;
        ////string value = productDto.Usage;
        ////if(Enum.IsDefined(typeof(Usages),value) == false )
        ////{
        ////    int total = Enum.GetValues(typeof(Usages)).Length;
        ////    Dictionary<int,string> addEnum = new Dictionary<int,string>();
        ////    addEnum.Add(total,productDto.Usage);
           
        ////}
        //mProduct.Usage = productDto.Usage;
        mProduct = _mapper.Map<Product>(productDto);
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
            Usage =(Usages)0
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