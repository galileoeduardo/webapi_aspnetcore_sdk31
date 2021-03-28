using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace Domain.Security
{
    public class SignConfigurations
    {
        public SignConfigurations()
        {
            using (var provider = new RSACryptoServiceProvider(2048))
            {
                Key = new RsaSecurityKey(provider.ExportParameters(true));
            }

            SigningCredentials = new SigningCredentials(Key, SecurityAlgorithms.RsaSha256Signature);
        }
        public SecurityKey Key { get; set; }
        public SigningCredentials SigningCredentials { get; set; }
    }
}
