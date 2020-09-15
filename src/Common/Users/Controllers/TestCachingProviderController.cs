using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Web.Controllers;
using Web.Controls;

namespace Users.Controllers
{
    [Route("api/[controller]")]
    [EnableCors("AllowSpecificOrigin")]
    public class TestCachingProviderController : BaseController
    {

        public TestCachingProviderController() { }

        [HttpGet]
        [Route("get")]
        public ApiResult Get(string key, string hashkey)
        {
            CacheProvider.TryGetValue(key, hashkey, out string value);
            return new ApiResult()
            {
                Result = 0,
                Data = value
            };
        }

        [HttpGet]
        [Route("set")]
        public ApiResult Set(string key, string hashkey, string value)
        {
            return new ApiResult()
            {
                Result = CacheProvider.TrySetValue(key, hashkey, value) ? 0 : -1
            };
        }

        [HttpGet]
        [Route("update")]
        public ApiResult Update(string key, string hashkey, string value)
        {
            return new ApiResult()
            {
                Result = CacheProvider.TrySetValue(key, hashkey, value) ? 0 : -1
            };
        }

        [HttpGet]
        [Route("delete")]
        public ApiResult Delete(string key, string hashkey)
        {
            CacheProvider.TryRemoveValue<string>(key, hashkey);
            return new ApiResult()
            {
                Result = 0
            };
        }
    }
}
