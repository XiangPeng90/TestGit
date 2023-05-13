using FoodMall.Tools;
using Microsoft.AspNetCore.Mvc;
using Model.Models;

namespace FoodMall.Components
{
    public class UserInfo:ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            User shop = HttpContext.Session.Get<User>("CurrentInfo");
            return View(shop);
        }
    }
}
