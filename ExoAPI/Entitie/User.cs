using ExoAPI.Type;
namespace ExoAPI.Entitie
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] passwordSalt { get; set; }
        public Grades Grade { get; set; }
    }
}
