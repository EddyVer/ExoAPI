using ExoAPI.Entitie;
using ExoAPI.Type;

namespace ExoAPI.Dto
{
    public class UserDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public string Grade { get; set; }
        public List<EntrepotDto> Entrepots { get; set; }
        public string RefreshToken { get; set; } = string.Empty;
        public DateTime DateCreated { get; set; }
        public DateTime TokenExpires { get; set; }

    }
}
