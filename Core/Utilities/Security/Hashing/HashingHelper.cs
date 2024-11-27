using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Utilities.Security.Hashing
{
    public static class HashingHelper
    {
        public static void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)//Register olurken kullanıcaz. Dışarıya hash ve salt cıkaracak. .Net'in cryptoghraphy sınıflarından yararlanıcaz, disposable pattern ile
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;//başka bişey de verebilirsin, standart olmalı, şifreyi çözerken de kullanıcaz
                passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));//Byte array alıyor, string'in getBytes karşılığını veriyoruz
            }
        }
        public static bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)//kullanıcının gönderdiği şifreyi yine aynı algoritmayı kullanarak hash'lersem veri tabanındaki değer çıkar mı?
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512(passwordSalt))//Doğrulama yapıcaz, bu sefer tuzunu da koy
            {
                var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));//hesaplanan hash, oluşan değer byte array

                for (int i = 0; i < computedHash.Length; i++)
                {
                    if (computedHash[i] != passwordHash[i]) //veri tabanından gelen passwordHash ile karşılaştır.
                    {
                        return false;
                    }
                }
                return true;
            }
        }
    }
}
