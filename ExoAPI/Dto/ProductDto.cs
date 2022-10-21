using ExoAPI.Type;

namespace ExoAPI.Dto;

public class ProductDto
{
    public int Id { get; set; }
    public string Origin { get; set; }
    public string Name { get; set; }
    public int Quantite  { get; set; }
    public Usages Usage { get; set; }
}