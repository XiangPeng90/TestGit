using FoodMall.Tools;
using Microsoft.AspNetCore.Mvc;
using Model.Models;

namespace FoodMall.Components
{
    public class ShopInfo : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            Shop shop = HttpContext.Session.Get<Shop>("CurrentInfo");
            return View(shop);
        }
    }
}
