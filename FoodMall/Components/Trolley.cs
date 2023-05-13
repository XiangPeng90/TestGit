using Entities;
using FoodMall.Tools;
using Microsoft.AspNetCore.Mvc;
using Model.Models;

namespace FoodMall.Components
{
    public class Trolley : ViewComponent
    { 
        private readonly Context _context;

        public Trolley(Context context)
        {
            _context = context;
        }
        public IViewComponentResult Invoke()
        {
            var shopid = Request.Path.ToString().Substring(Request.Path.ToString().Length-1);
            User user = HttpContext.Session.Get<User>("CurrentInfo");
            return View(user);
        }
    }
}
