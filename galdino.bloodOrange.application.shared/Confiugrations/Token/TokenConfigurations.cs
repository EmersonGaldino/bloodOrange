namespace galdino.bloodOrange.application.shared.Confiugrations.Token
{
    public class TokenConfigurations
    {
        public string Audience { get; set; }
        public string Issuer { get; set; }
        public int ExpireIn { get; set; }
        public string SigningKey { get; set; }
    }
}
