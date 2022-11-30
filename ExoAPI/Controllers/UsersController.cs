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
using Microsoft.EntityFrameworkCore;
using Org.BouncyCastle.Asn1.Ocsp;
using System.Net;
using System.Net.Http.Headers;
using Microsoft.AspNetCore.StaticFiles;
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

        [HttpPost("modifPassword/{id}"), Authorize]
        public async Task<ActionResult<string>> ModifPassword(int id,[FromBody] UserDto dto)
        {
            User user = _businessContext.Users.First(x => x.Id == id);
            CreatePasswordHash(dto.Password, out byte [] passwordHash, out byte[]passwordSalt);
            user.PasswordHash = passwordHash;
            user.passwordSalt = passwordSalt;
            _businessContext.Users.Update(user);
            _businessContext.SaveChanges();
            return Ok(_mapper.Map<UserDto>(user));
        }
        [HttpPost("register")]
        public async Task<ActionResult<string>> Register(SignUPDto dto)
        {
            User user = new User();
            CreatePasswordHash(dto.Password, out  byte [] passwordHash,out byte[]passwordSalt);
            user.Name = dto.Name;
            user.PasswordHash = passwordHash;
            user.passwordSalt = passwordSalt;
            user.Grade = "user";
            _businessContext.Users.Add(user);
            _businessContext.SaveChanges();
            return Ok(dto);
        }

        [HttpPost("addEntrepotUser/{id}"), Authorize]
        public IActionResult NewEntrepot(int id, [FromBody]EntrepotDto dto)
        {
            Entrepot entrepot = _mapper.Map<Entrepot>(dto);
            User user = _businessContext.Users.First(x => x.Id == id);
            if (user.Entrepots == null)
            {
                user.Entrepots = new List<Entrepot>();
            }
            user.Entrepots.Add(entrepot);
            _businessContext.Users.Update(user);
            _businessContext.SaveChanges();
            UserDto userDto = _mapper.Map<UserDto>(user);
            return Ok(userDto);
        }
        [HttpPost("login")]
        public async Task<ActionResult<string>> Login([FromBody] LoginDto loginDto)
        {
            User user = await _businessContext.Users.FirstOrDefaultAsync(x => x.Name == loginDto.Username);
            if (user == null)
            {
                return NotFound();
            }
            if (!CheckPassword(user, loginDto.Password))
            {
                return BadRequest("password not correct");
            }
            string name= user.Name;

            UserDto dto = _mapper.Map<UserDto>(user);
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


}
