using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Utilities.Security.Encryption
{
    public class SecurityKeyHelper//Şifreleme olan sistemlerde herşeyi byte array formatında vermemiz gerekiyor. Json web Token servislerinin anlayacağı hale getir
    {
        //uyduruk string'lerle encryption'a parametre geçemezsin. Onu byte array haline getir
        public static SecurityKey CreateSecurityKey(string securityKey)//appconfig.json'daki securityKey'i parametre ver, bende sana onun securityKey karşılığını vereyim : Microsoft.IdentityModel.token
        {
            //Byte'ını alıp onu bir simetric Security anahtarı haline getir
            return new SymmetricSecurityKey(Encoding.UTF8.GetBytes(securityKey));//Anahtarlar simetrik ve asimetrik olarak ayrılır. Biz simetrik bir securityKey kullanıyoruz
        }
    }
}

//Nuget: Microsoft.IdentityModel.token   ,   