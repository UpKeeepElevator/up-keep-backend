namespace UpKeep.Data.Configuration;

public class JwtOptions
{
    public string Issuer { get; set; }
    public string Audience { get; set; }
    public string Key { get; set; }

    public JwtOptions(string issuer, string audience, string key)
    {
        Issuer = issuer;
        Audience = audience;
        Key = key;
    }
}