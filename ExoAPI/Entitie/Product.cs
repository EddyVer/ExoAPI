using ExoAPI.Type;

namespace ExoAPI.Entitie;

public class Product
{
    public int Id { get; set; }
    public string Origin { get; set; }
    public string Name { get; set; }
    public int Quantite  { get; set; }
    public Usages Usage { get; set; }
}