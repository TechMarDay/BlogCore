using Common;
using Common.Interfaces;
using Common.Models;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Linq;
using System.Reflection;

namespace Web.Filters
{
    public class SessionFilter : IActionFilter
    {
        public void OnActionExecuted(ActionExecutedContext context)
        {
            /*Constructor Initialization*/
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            UserSession userSession = null;
            if (context.HttpContext.Items.TryGetValue("UserSession", out object value))
            {
                userSession = (UserSession)value;
            }

            if (context.HttpContext.Request.Headers.Keys.Any(k => k.ToLower() == Constant.LanguageHeaderKey.ToLower()))
            {
                var headerLanguage = context.HttpContext.Request.Headers.FirstOrDefault(h => h.Key.ToLower() == Constant.LanguageHeaderKey.ToLower());
                var language = headerLanguage.Value.ToString();
                // Set Language to Controller
                context.Controller.GetType().GetProperty("LanguageCode")?.SetValue(context.Controller, language);
            }

            // Set LoginSession to Controller
            context.Controller.GetType().GetProperty("LoginSession")?.SetValue(context.Controller, userSession);

            // With add Login session to CommandBase
            foreach (var item in context.ActionArguments)
            {
                var type = item.Value?.GetType().BaseType;
                if (type != null && type.Name.Contains("BaseCommand"))
                {
                    PropertyInfo[] infors = item.Value.GetType().GetProperties();
                    infors.ToList().Find(i => i.Name == "LoginSession")?.SetValue(context.ActionArguments[item.Key], userSession); //Get login session in parameter CommandBase of Request and set data on it
                }
            }

            // With add Login session to QueriesBase: Check with to GetRuntimeFields and GetFields
            foreach (var item in context.Controller.GetType().GetFields())
            {
                var interf = item.FieldType.GetInterfaces().ToList().Find(i => i == typeof(IBaseQueries));
                if (interf != null)
                {
                    var field = item.GetValue(context.Controller);
                    ((IBaseQueries)field).LoginSession = userSession;
                }
            }
            foreach (var item in context.Controller.GetType().GetRuntimeFields())
            {
                var interf = item.FieldType.GetInterfaces().ToList().Find(i => i == typeof(IBaseQueries));
                if (interf != null)
                {
                    var field = item.GetValue(context.Controller);
                    ((IBaseQueries)field).LoginSession = userSession;
                }
            }
        }
    }
}