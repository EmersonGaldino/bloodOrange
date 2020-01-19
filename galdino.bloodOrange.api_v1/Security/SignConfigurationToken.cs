using Microsoft.IdentityModel.Tokens;
using System.Security.Cryptography;

namespace galdino.bloodOrange.api_v1.Security
{
    public class SignConfigurationToken
    {
        public SecurityKey Key { get; }
        public SigningCredentials SigningCredentials { get; }

        public SignConfigurationToken()
        {
            using (var provider = new RSACryptoServiceProvider(2048))
            {
                Key = new RsaSecurityKey(provider.ExportParameters(true));
            }

            SigningCredentials = new SigningCredentials(
                Key, SecurityAlgorithms.RsaSha256Signature);
        }
    }
}
