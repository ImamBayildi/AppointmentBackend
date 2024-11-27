using Core.Utilities.IoC;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Core.CrossCuttingConcerns.Caching.Microsoft
{
    public class MemoryCacheManager : ICacheManager//Direk kullanmak yerine Adapter pattern kullanıyorum
    {
        IMemoryCache _memoryCache;//Microsoft.Extensions.Caching.Memory; //Her iki interface için ServiceTool ile injection yapılacak (CoreModule)
        public MemoryCacheManager()
        {
                _memoryCache= ServiceTool.ServiceProvider.GetService<IMemoryCache>();//inject edilmiş bütün interfacelerin karşılığını alabilirim
        }

        public void Add(string key, object value, int duration)
        {
            _memoryCache.Set(key,value,TimeSpan.FromMinutes(duration));
        }

        public T Get<T>(string key)
        {
            return _memoryCache.Get<T>(key);
        }

        public object Get(string key)//boxing
        {
            return _memoryCache.Get(key);
        }

        public bool IsAdd(string key)
        {
            return _memoryCache.TryGetValue(key, out _);//dış parametreye atama, sadece değeri kontrol et. Bu metot benden out parameter istiyor ama ben onu istemiyorum
        }

        public void Remove(string key)
        {
            _memoryCache.Remove(key);
        }

        public void RemoveByRegexp(string pattern)//verilen pattern'a göre silme işlemi yap. Runtime'da bellekten sil
        {
            var cacheEntriesCollectionDefinition = typeof(MemoryCache).GetProperty("EntriesCollection", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);//Bellekte MemoryCache türünde olan EntriesCollection'ı bul
            var cacheEntriesCollection = cacheEntriesCollectionDefinition.GetValue(_memoryCache) as dynamic;//Definition'ı _memoryCache olanları bul
            List<ICacheEntry> cacheCollectionValues = new List<ICacheEntry>();//Listeye at

            foreach (var cacheItem in cacheEntriesCollection)
            {
                ICacheEntry cacheItemValue = cacheItem.GetType().GetProperty("Value").GetValue(cacheItem, null);
                cacheCollectionValues.Add(cacheItemValue);
            }

            var regex = new Regex(pattern, RegexOptions.Singleline | RegexOptions.Compiled | RegexOptions.IgnoreCase);//System.Text.RegularExpressions;
            var keysToRemove = cacheCollectionValues.Where(d => regex.IsMatch(d.Key.ToString())).Select(d => d.Key).ToList();

            foreach (var key in keysToRemove)
            {
                _memoryCache.Remove(key);
            }
        }
    }
}
