using Model.Models;
using FoodMall.Tools;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace FoodMall.Utility.Filter
{
    public class LoginFilterAttribute<T> : Attribute, IAuthorizationFilter where T : class
    {

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var httpContext = context.HttpContext;
            if(httpContext.Session.Get<T>("CurrentInfo") is null)
            {
                switch (httpContext.Session.Get<T>("CurrentInfo"))
                {
                    case not User:
                        context.Result = new RedirectResult("/Home/Index");
                        break;
                    case not Shop:
                        context.Result = new RedirectResult("/Shop/Login");
                        break;
                }
            }
        }
    }
}

