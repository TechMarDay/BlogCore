using Autofac;
using Common;
using Common.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using System.Collections.Generic;
using Users.UI.Interfaces.Queries;

namespace Users.UI.Extensions
{
    public static class UserExtension
    {
        public static void LoadSession(this IApplicationBuilder application, IHostingEnvironment environment)
        {

            var userAccessTokenQueries = GlobalConfiguration.Container.Resolve<IAuthenQueries>();
            var accessTokens = userAccessTokenQueries.Gets().Result;

            Dictionary<string, UserSession> dicSession = new Dictionary<string, UserSession>();
            foreach (var item in accessTokens)
            {
                dicSession.Add(item.AccessToken, item);
            }
        }
    }
}
