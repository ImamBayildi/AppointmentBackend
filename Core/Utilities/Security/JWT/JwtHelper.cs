using Core.Entities.Security;
using Core.Extensions;
using Core.Utilities.Security.Encryption;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Core.Utilities.Security.JWT
{
    public class JwtHelper : ITokenHelper//Main class. Chain'e dahil edilecek
    {
        public IConfiguration Configuration { get; }//Config Injection appSetting.Json, dosyasını okumak için .Net'ten aldık : Microsoft.Extensions.Configuration
        private TokenOptions _tokenOptions;//Okunan değerleri TokenOptions nesnesine atıcaz : !Microsoft.Extensions.Identity.Core'dan almadık!
        private DateTime _accessTokenExpiration;
        public JwtHelper(IConfiguration configuration)
        {
            Configuration = configuration;
            _tokenOptions = Configuration.GetSection("TokenOptions").Get<TokenOptions>();//dotnet add package Microsoft.Extensions.Configuration.Binder
            //GetSEction  : adı TokenOptions olan {} bölümünü al

        }
        public AccessToken CreateToken(User user, List<OperationClaim> operationClaims)//User ve claimler ile bir token oluştur
        {
            _accessTokenExpiration = DateTime.Now.AddMinutes(_tokenOptions.AccessTokenExpiration);//Bu token ne zaman bitecek, configten al
            var securityKey = SecurityKeyHelper.CreateSecurityKey(_tokenOptions.SecurityKey);//Anahtar değerini git TokenOptions(appSettings.Json)'dan al, CreateSecurityKey ile formatla.
            var signingCredentials = SigningCredentialsHelper.CreateSigningCredentials(securityKey);//Hangi anahtar ve algoritmayı kullanacak
                                         //tokenOption'lar, kullanıcı bilgisi,neyi kullanarak yapacak ,claimleri neler?
            var jwt = CreateJwtSecurityToken(_tokenOptions, user, signingCredentials, operationClaims);//Jwt üret, Jwt helper ile
            var jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
            var token = jwtSecurityTokenHandler.WriteToken(jwt);

            return new AccessToken
            {
                Token = token,
                ExpirationTime = _accessTokenExpiration
            };

        }
                                     
        //System.Identitymodel.Tokens.Jwt, Microsoft.IdentityModel.Token
        public JwtSecurityToken CreateJwtSecurityToken(TokenOptions tokenOptions, User user,SigningCredentials signingCredentials, List<OperationClaim> operationClaims)
        {
            var jwt = new JwtSecurityToken(//Bilgiler ile jwtToken oluştur
                issuer: tokenOptions.Issuer,
                audience: tokenOptions.Audience,
                expires: _accessTokenExpiration,
                notBefore: DateTime.Now,
                claims: SetClaims(user, operationClaims),//
                signingCredentials: signingCredentials
            );
            return jwt;
        }

        private IEnumerable<Claim> SetClaims(User user, List<OperationClaim> operationClaims)//Claim listesi oluşturur
        {
            var claims = new List<Claim>();//System.Security.Claim
            claims.AddNameId(user.Id.ToString());//.Net'te var olan bir nesneye yeni metotlar eklenecek => 
            claims.AddEmail(user.Email);
            claims.AddName($"{user.FirstName} {user.LastName}");
            claims.AddRoles(operationClaims.Select(c => c.Name).ToArray());

            return claims;
        }
    }
}
