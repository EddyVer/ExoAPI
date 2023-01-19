using AutoMapper;
using ExoAPI.Dto;
using ExoAPI.Entitie;

namespace ExoAPI.Context
{
    
    public class UsersContext
    {
        private readonly IMapper _mapper;
        public List<User> Users { get; set; }
        public List<UserDto> UsersDto { get; set; }
        public User GetByName(string name) => Users.First(x => x.Name == name);

        public static Dictionary<string, string> DicUsers = new Dictionary<string, string>()
        {
            {"admin","admin"},
            {"user","user"}
        };
        public UsersContext(IMapper mapper)
        {
            _mapper = mapper;


        }
    }
}
