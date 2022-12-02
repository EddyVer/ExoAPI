using ExoAPI.Entitie;

namespace ExoAPI.Dto
{
    public class EntrepotDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double Longitude { get; set; }
        public double Latitude { get; set; }
        public List<Product> products { get; set; }
    }
}
