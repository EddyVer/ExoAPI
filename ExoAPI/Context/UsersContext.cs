using AutoMapper;
using ExoAPI.Entitie;
using ExoAPI.Dto;
using ExoAPI.Type;
namespace ExoAPI.Context
{
    public class UsersContext
    {
        private readonly IMapper _mapper;
        public List<User> Users { get; set; }
        public List<UserDto> UsersDto { get; set; }
        public User GetByName(string name, string password) => Users.First(x => x.Name == name && x.Password == password);
        public UsersContext(IMapper mapper)
        {
            _mapper = mapper;
            Users = new List<User>();
            Users.Add(new User()
            {
                Id = 1,
                Name ="admin",
                Password = "admin",
                Grade = Grades.admin
            });
            Users.Add(new User() { 
                Id = 2,
                Name = "user",
                Password = "user",
                Grade = Grades.user
            });

        }
    }
}
