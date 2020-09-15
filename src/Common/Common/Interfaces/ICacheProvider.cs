using Microsoft.AspNetCore.Http;

namespace Common.Interfaces
{
    public interface ICacheProvider
    {
        bool TryGetValue<T>(string key, string hashkey, out T value, HttpContext instance = null);

        bool TrySetValue<T>(string key, string hashkey, T value, HttpContext instance = null);

        bool TryRemoveValue<T>(string key, string hashkey, HttpContext instance = null);
    }
}