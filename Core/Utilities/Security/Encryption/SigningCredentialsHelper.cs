using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Utilities.Security.Encryption
{
    public class SigningCredentialsHelper
    {
        public static SigningCredentials CreateSigningCredentials(SecurityKey securityKey) //Web api'nin kullanabileceği JWT'ları oluşturabilmek için elimizde olanlar(kullanıcı adı, şifre) : Microsoft.IdentityModel.Token
        {
            //Giriş bilgilerinde hangi key ve algoritmayı kullanıcam
            return new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha512Signature);//Hangi anahtar ve hangi algoritmayı kullanacağını ver
        }
    }
}
