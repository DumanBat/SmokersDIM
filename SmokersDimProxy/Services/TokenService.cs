public interface ITokenService
{
    string AccessToken { get; set; }
}

public class TokenService : ITokenService
{
    public string AccessToken { get; set; }
}