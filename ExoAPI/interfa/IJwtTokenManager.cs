namespace ExoAPI.interfa;

public interface IJwtTokenManager
{
    string Authenticate(string userName, string password);
}