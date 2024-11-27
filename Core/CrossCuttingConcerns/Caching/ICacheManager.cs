using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.CrossCuttingConcerns.Caching
{
    public interface ICacheManager//RedisCache, elasticSearch, vb. olabilir
    {
        T Get<T> (string key);//getirilecek parametre object, liste ve ya her hangi bir şey olabilir
        object Get (string key); //Generic olmayan vversiyonda type casting gerekir
        void Add(string key, object value, int duration);
        bool IsAdd(string key);//cache'de var mı
        void Remove  (string key);
        void RemoveByRegexp(string pattern);//isminde "set" olanları uçur gibi.
    }
}
