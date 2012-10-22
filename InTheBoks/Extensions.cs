using System.Collections.Generic;
using System.Linq;

namespace InTheBoks
{
    public static class Extensions
    {
        public static Dictionary<K, T> OrderByKey<K, T>(this Dictionary<K, T> dicionario)
        {
            return dicionario.OrderBy(p => p.Key).ToDictionary(p => p.Key, p => p.Value);
        }

        public static Dictionary<K, T> OrderByKeyDescending<K, T>(this Dictionary<K, T> dicionario)
        {
            return dicionario.OrderByDescending(p => p.Key).ToDictionary(p => p.Key, p => p.Value);
        }

        public static Dictionary<K, T> OrderByValue<K, T>(this Dictionary<K, T> dicionario)
        {
            return dicionario.OrderBy(p => p.Value).ToDictionary(p => p.Key, p => p.Value);
        }

        public static Dictionary<K, T> OrderByValueDescending<K, T>(this Dictionary<K, T> dicionario)
        {
            return dicionario.OrderByDescending(p => p.Value).ToDictionary(p => p.Key, p => p.Value);
        }
    }
}