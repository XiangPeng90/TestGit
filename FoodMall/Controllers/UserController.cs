using Model.Models;
using FoodMall.Tools;
using FoodMall.Utility.Filter;
using IService;
using Microsoft.AspNetCore.Mvc;
using Service;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Caching.Memory;
using Entities;
using Microsoft.AspNetCore.Identity;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace FoodMall.Controllers
{
    public class UserController : Controller
    {
        private readonly ILogger<UserController> _logger;
        private readonly IUserService _userService;
        private readonly IMemoryCache _memoryCache;
        private readonly Context _context;

        public UserController(
            ILogger<UserController> logger
            , IUserService userService
            , IMemoryCache memoryCache
            ,Context context)
        {
            _logger = logger;
            _userService = userService;
            _memoryCache = memoryCache;
            _context = context;
        }
        #region 首页
        // GET: /<controller>/
        [LoginFilterAttribute<User>]
        public IActionResult Index()
        {
            var shops = _userService.ShopInfo();
            return View(shops);
        }
        #endregion
        #region 注册
        [HttpPost]
        public async Task<IActionResult> Register(User user)
        {

            if (ModelState.IsValid)
            {

                if (await _userService.Regist(user))
                {
                    ViewBag.Message = "注册成功";
                }
                else
                {
                    ViewBag.Message = "已有改账户";
                }
            }
            else
                ViewBag.Message = "请按要求填写";

            return View();
        }

        public IActionResult Register()
        {
            return View();
        }
        #endregion
        #region 登录
        [HttpPost]
        public IActionResult Login(User user)
        {
            if (_userService.Login(user) != null)
            {
                HttpContext.Session.Set("CurrentInfo", _userService.Login(user));
                return Redirect("/User/index");
            }
            else
            {
                ViewBag.Message = "密码或账号错误";
                return View();
            }
        }

        public IActionResult Login()
        {
            return View();
        }
        #endregion
        #region 等出
        public IActionResult exit()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("index", "home");
        }
        #endregion
        #region 进入店铺
        public IActionResult enterShop(int id,int foodid, int page = 1)
        {
            //搜索
            if (foodid != 0)
            {
                var parm =  int.Parse(s: HttpContext.Request.Path.Value!.Substring(16,1));
                var food = _memoryCache.GetOrCreate("foos" + foodid, (e) =>
                {
                    e.AbsoluteExpirationRelativeToNow =TimeSpan.FromSeconds(10);
                    return _userService.Search(foodid, parm);
                });
                ViewBag.Foods = food;
                return View();
            }
            //直接进入
            else
            {
                var foods = _userService.EntryShop(id).Skip(30 * (page - 1)).Take(30).ToList();
                if (foods.Count % 30 == 0)
                {
                    ViewBag.pages = foods.Count / 10;
                }
                else
                {
                    ViewBag.pages = foods.Count / 10 + 1;
                }
                ViewBag.shop = id;
                return View(foods);
            }
            
        }
        #endregion
        #region 加入购物车
        [HttpPost]
        public IActionResult AddTrolley(long shopId, int foodId)
        {
            var menu = _context.Menus!.Where(m=>m.ShopId==shopId).Include(f=>f.foods).SingleOrDefault();
            var food =  menu!.foods.Where(f => f.id == foodId).SingleOrDefault();
            var user = HttpContext.Session.Get<User>("CurrentInfo");
            var trolley = _context.Trolleys
                .Where(t => t.userId == user.id)
                .Include(t=>t.foods)
                .SingleOrDefault();
            if (!trolley.foods.Contains(food))
            {
                trolley.foods.Add(food);
            }
            else
                food.count += 1;
            _context.SaveChanges();
            _memoryCache.Set("trolley"+user.id + shopId, trolley,TimeSpan.FromSeconds(5));
           
            return View("enterShop",menu.foods);  
        }
        #endregion
        #region 购物车
        [LoginFilter<User>]
        public IActionResult Trolley()
        {
            var shopid = Request.Path.ToString().Substring(Request.Path.ToString().Length-1);
            var user = HttpContext.Session.Get<User>("CurrentInfo");
            var trolley = _memoryCache.GetOrCreate("trolley" + user.id + shopid, entry =>
            {
                entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(4);
                return _context.Trolleys!.Where(u => u.userId == user.id).Include(t => t.foods).SingleOrDefault();
            });
            try
            {
                var menu = _context.Menus.Where(m => m.ShopId == int.Parse(shopid)).SingleOrDefault();
                return View(trolley.foods.Where(f => f.menuId == menu.id).ToList());
            }
            catch (Exception ex)
            {
                return View("UserError");
            }
            
            
        }
        #endregion
        #region 结算
        [LoginFilter<User>]
        public IActionResult Check()
        {
            var user = HttpContext.Session.Get<User>("CurrentInfo");
            var shopid=Request.Path.ToString().Substring(Request.Path.ToString().Length-1);
            var trolley = _memoryCache.GetOrCreate("trolley" + user!.id + shopid, entry =>
            {
                entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(4);
                return _context.Trolleys!.Where(u => u.userId == user.id).Include(t => t.foods).SingleOrDefault();
            });
            var shop = _context.Menus.Where(m => m.id == trolley.foods.FirstOrDefault().menuId).Include(m => m.shop).SingleOrDefault().shop;
            if (trolley.foods != null && trolley.foods.Count != 0)
            {
                var Foods = new List<Food>();
                foreach (var food in trolley.foods)
                {
                    Foods.Add(food);
                }
                _context.Users.Where(s => s.id == user.id).SingleOrDefault().orders.Add(new Order
                {
                    ordertime = DateTime.Now,
                    Foods = Foods,
                    shop = shop,
                });
                _context.SaveChanges();
                var foods = trolley.foods.Where(f => f.count > 1).ToList();
                for (int i = 0; i < foods.Count; i++)
                {
                    foods[i].count = 1;
                }
                for (int i = 0; i < trolley.foods.Count; i++)
                {
                    _context.Trolleys.Where(t => t.userId == user.id).SingleOrDefault().foods.Remove(trolley.foods[i]);
                }
                _memoryCache.Remove("trolley" + user.id + shopid);
            }
            else
                ViewBag.Error = false;
            _context.SaveChanges();
            return RedirectToAction("Trolley");
        }
        #endregion
    }
}

