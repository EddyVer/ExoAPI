using System.IdentityModel.Tokens.Jwt;
using System.Text;
using ExoAPI.Entitie;
using ExoAPI.Context;
using ExoAPI.interfa;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;

namespace ExoAPI.Manager;

public class JwtTokenManager : IJwtTokenManager
{
    private readonly IConfiguration _configuration;
    public JwtTokenManager(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public string Authenticate(string userName, string password)
    {
        if (!UsersContext.DicUsers.Any(x => x.Key.Equals(userName) && x.Value.Equals(password)))
        {
            return null;
        }

        var key = _configuration.GetValue<string>("JwtConfig:Key");
        var keyBytes = Encoding.ASCII.GetBytes(key);

        var tokenHandler = new JwtSecurityTokenHandler();
        var tokenDescripor = new SecurityTokenDescriptor()
        {
            Subject = new ClaimsIdentity(new Claim[]
            {
                new Claim(ClaimTypes.NameIdentifier, userName)
            }),
            Expires = DateTime.UtcNow.AddMinutes(30), 
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(keyBytes), SecurityAlgorithms.HmacSha256Signature)
        };
        var token = tokenHandler.CreateToken(tokenDescripor);
        return tokenHandler.WriteToken(token);
    }
}

