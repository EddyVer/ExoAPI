using System.IdentityModel.Tokens.Jwt;
using System.Runtime.CompilerServices;
using System.Security.Claims;
using System.Security.Cryptography;
using AutoMapper;
using ExoAPI.Context;
using ExoAPI.Dto;
using ExoAPI.Entitie;
using ExoAPI.Mapping;
using ExoAPI.Service.UserService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.IdentityModel.Tokens;
using ExoAPI.Token;

namespace ExoAPI.Controllers
{ 
    [ApiController]
    [Route("[Controller]")]
    
    public class UsersController : Controller
    {
        private readonly IMapper _mapper;
        private readonly BusinessContext _businessContext;
        private readonly IConfiguration _configuration;
        private readonly IUserService _userService;
        public UsersController(IMapper mapper, BusinessContext businessContext, IConfiguration configuration, IUserService userService)
        {
            _userService= userService;
            _mapper = mapper;
            _businessContext = businessContext;
            _configuration = configuration;
        }
        [HttpGet, Authorize]
        public IActionResult GetUser()
        {
            string userName = _userService.GetUserName();
            return Ok(userName);
        }
        [HttpPost("register")]
        public async Task<ActionResult<string>> Register(UserDto dto)
        {
            User user = _mapper.Map<User>(dto);
            CreatePasswordHash(dto.Password, out  byte [] passwordHash,out byte[]passwordSalt);
            user.PasswordHash = passwordHash;
            user.passwordSalt = passwordSalt;
            user.Grade = "user";
            _businessContext.Users.Add(user);
            _businessContext.SaveChanges();
            return Ok(dto);
        }

        [HttpPost("login")]
        public async Task<ActionResult<string>> Login(UserDto dto)
        {
            User user = _businessContext.Users.First(x => x.Name == dto.Name);
            if ( user.Name != dto.Name )
            {
                return BadRequest("user not found");
            }
            if (!CheckPassword(user, dto.Password))
            {
                return BadRequest("password not correct");
            }
            dto.Name= user.Name;
            dto.Grade= user.Grade;
            string token = CreateToken(dto);

            var refreshToken = GenerateRefreshToken();
            SetRefreshToken(refreshToken,dto);    
            return Ok(token);
        }
        [HttpPost("refresh-token")]
        public async Task<ActionResult<string>> RefreshToken(UserDto dto)
        {

            var refreshToken = Request.Cookies["refreshToken"];
            if (dto.RefreshToken.Equals(refreshToken))
            {
                return Unauthorized("Invalid Refresh Token");
            }
            //else if (dto.TokenExpires < DateTime.Now)
            //{
            //    return Unauthorized("Token expired");
            //}

            string token = CreateToken(dto);
            var newRefreshToken = GenerateRefreshToken();
            SetRefreshToken(newRefreshToken,dto);
            return Ok(token);
        }
        private  RefreshToken GenerateRefreshToken()
        {
            var refreshToken = new RefreshToken
            {
                Token = Convert.ToBase64String(RandomNumberGenerator.GetBytes(64)),
                Expires = DateTime.Now.AddDays(2),
                Created= DateTime.Now
            };
            return refreshToken;
        }
        private void SetRefreshToken(RefreshToken newRefreshToken, UserDto dto)
        {
            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,
                Expires = newRefreshToken.Expires,
            };
            Response.Cookies.Append("refreshToken",newRefreshToken.Token,cookieOptions);
            dto.RefreshToken = newRefreshToken.Token;
            dto.DateCreated = newRefreshToken.Created;
            dto.TokenExpires = newRefreshToken.Expires;

        }
        private string CreateToken(UserDto user)
        {
            List<Claim> claims = new List<Claim> { 
                new Claim(ClaimTypes.Name , user.Name),
                new Claim(ClaimTypes.Role, user.Grade)
            };
            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(_configuration.GetSection("AppSettings:Token").Value));
            var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature);
            var token = new JwtSecurityToken(
                user.Name,
                user.Grade,
                claims: claims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials:cred
                ) ;
            var jwt = new JwtSecurityTokenHandler().WriteToken(token); 
            return jwt;
        }
        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte [] passwordSalt)
        {
            using (var hmac = new HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }
        private bool CheckPassword(User user, string password)
        {
            using(var hmac = new HMACSHA512(user.passwordSalt))
            {
                var computHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                return computHash.SequenceEqual(user.PasswordHash);
            }
        }
        [HttpGet("GetByName/{name}")]
        public IActionResult FindUser(string name)
        {
            return Ok(_mapper.Map<UserDto>(_businessContext.Users.First(x=> x.Name == name)));
        }
    }

    //internal class RefreshToken
    //{
    //    public string Token { get; set; }
    //    public DateTime Expires { get; set; }
    //    public DateTime Created { get; set; }
    //}
}
