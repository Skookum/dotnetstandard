using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Caching;

namespace DotNetStandard.CacheStrategies
{
    /// <summary>
    /// Normally its not good practice to implement Singleton patterns
    /// as they make unit testing difficult.  In this case though we need
    /// to be sure that there is only one method of access to this data
    ///
    /// Implemented to be thread-safe
    /// Do not unseal and do not remove the
    /// initialition of TokenAuthenticationCache
    /// </summary>
    public sealed class TokenCacheStrategy
    {
        private readonly Cache _cache;
        private static readonly TokenCacheStrategy _instance = new TokenCacheStrategy();
        private int _expirationTime;

        private TokenCacheStrategy()
        {
            _cache = HttpRuntime.Cache;
            _expirationTime = 15;
        }

        public static TokenCacheStrategy Instance
        {
            get { return _instance; }
        }

        public void SetExpirationTimeout(int expirationTimeout)
        {
            _expirationTime = expirationTimeout;
        }

        public void Insert(string key)
        {
            _cache.Insert(key, key, null,
                 Cache.NoAbsoluteExpiration, TimeSpan.FromMinutes(_expirationTime));
        }

        public void Insert(string key, object objectToCache)
        {
            _cache.Insert(key, objectToCache, null,
                 Cache.NoAbsoluteExpiration, TimeSpan.FromMinutes(_expirationTime));
        }

        public bool Validate(string token)
        {
            return _cache.Get(token) != null;
        }

        public object GetCachedObject(string token)
        {
            return _cache.Get(token);
        }

        public void Delete(string key)
        {
            _cache.Remove(key);
        }
    }
}
