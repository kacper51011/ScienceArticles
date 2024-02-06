namespace ScienceArticles.Application.Settings
{
    public class JWTSettings
    {
        public string Issuer { get; set; } = null!;
        public string Audience { get; set; } = null!;
        public string SigningKey { get; set; } = null!;

    }
}
