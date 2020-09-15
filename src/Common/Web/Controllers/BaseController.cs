using Autofac;
using Common;
using Common.Interfaces;
using Common.Models;
using Common.ViewModels;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Linq;

namespace Web.Controllers
{
    [EnableCors("AllowSpecificOrigin")]
    public abstract class BaseController : Controller
    {
        protected ICacheProvider CacheProvider { private set; get; } = GlobalConfiguration.Container.Resolve<ICacheProvider>();

        [JsonIgnore]
        public UserSession LoginSession { set; get; }
        public string LanguageCode { set; get; } = "vi"; //default
        private readonly int languageId = 0;
        public int LanguageId
        {
            get
            {
                if (languageId != 0)
                {
                    return languageId;
                }
                if (CacheProvider.TryGetValue(CachingConstant.LANUAGE, LanguageCode, out Language language) && language != null)
                {
                    return language.Id;

                }
                return 1; //default VN
            }
        }

        [ApiExplorerSettings(IgnoreApi = true)]
        public string GetCaption(string key)
        {
            if (CacheProvider.TryGetValue(CachingConstant.CAPTION_LANGUAGE, key, out CaptionViewModel caption) && caption != null)
            {

                var captionLanguage = caption.Languages.FirstOrDefault(cl => cl.LanguageId == LanguageId);
                if (captionLanguage != null)
                {
                    return captionLanguage.Caption;
                }
                else
                {
                    return caption.DefaultDescription;
                }

            }
            return $"#{key}";
        }
        [ApiExplorerSettings(IgnoreApi = true)]
        public UserSession GetUserSession(string accessToken)
        {
            if (CacheProvider.TryGetValue(CachingConstant.USER_LOGIN, accessToken, out UserSession session))
            {
                return session;
            }
            return null;
        }
        [ApiExplorerSettings(IgnoreApi = true)]
        public void SetUserSession(UserSession userSession)
        {
            CacheProvider.TrySetValue(CachingConstant.USER_LOGIN, userSession.AccessToken, userSession);
        }
        [ApiExplorerSettings(IgnoreApi = true)]
        public void RemoveUserSession(UserSession userSession)
        {
            CacheProvider.TryRemoveValue<UserSession>(CachingConstant.USER_LOGIN, userSession.AccessToken);
        }
    }
}
