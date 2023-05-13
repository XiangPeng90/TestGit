using Model.Models;
using FoodMall.Tools;
using IService;
using Microsoft.AspNetCore.Mvc;
using FoodMall.Utility.Filter;
using Entities;
using Microsoft.Extensions.Caching.Memory;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace FoodMall.Controllers
{
    public class ShopController : Controller
    {
        private static readonly string path = "wwwroot\\Images\\";
        private readonly ILogger<ShopController> _logger;
        private readonly IShopService _shopService;
        private readonly Context _context;
        private readonly IMemoryCache memoryCache;

        public ShopController(
            ILogger<ShopController> logger
            , IShopService shopService
            , Context context
            ,IMemoryCache memoryCache)
        {
            _logger = logger;
            _shopService = shopService;
            _context = context;
            this.memoryCache = memoryCache;
        }
        // GET: /<controller>/
        #region 首页
        [LoginFilterAttribute<Shop>]
        public IActionResult Index(string keywords,int page=1)
        {
            var shop = HttpContext.Session.Get<Shop>("CurrentInfo");
            if (keywords == null||keywords.Length==0)
            {
                var foods = _shopService.Search(shop!);
                var f = foods.Skip((page - 1) * 20).Take(20).ToList();
                if (f.Count == 0)
                {
                    ViewBag.Null = "您还没有任何商品";
                }
                ViewBag.pages = (foods.Count % 20 == 0) ? foods.Count / 20 : foods.Count / 20 + 1;
                ViewBag.page = page;
                return View(f);
            }
            else
            {
               List<Food>? foods = memoryCache.GetOrCreate("FoodsContains" + keywords, e =>
                {
                    e.AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(10);
                    _logger.LogInformation("从数据库中查询");
                    return _shopService.Search((int)shop!.id,page, keywords);
                });
                if (foods == null)
                    return View(new List<Food>());
                ViewBag.pages = (foods.Count % 20 == 0) ? foods.Count / 20 : foods.Count / 20 + 1;
                ViewBag.page = page;
                return View(foods);
                
            }
        }
        #endregion
        #region 注册
        [HttpPost]
        public async Task<IActionResult> Register(Shop shop)
        {
            
            if (ModelState.IsValid)
            {

                if (await _shopService.Regist(shop))
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
        public IActionResult Login(Shop shop)
        {
            var Myshop = _shopService.Login(shop);
            if (Myshop != null)
            {
                HttpContext.Session.Set("CurrentInfo", Myshop);
                return Redirect("/Shop/index");
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
        #region 登出
        public IActionResult exit()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("index","home");
        }
        #endregion
        #region 添加
        [LoginFilterAttribute<Shop>]
        [HttpPost]
        public async Task<IActionResult> Add(string f_name,double price, [FromForm(Name ="file")] IFormFile file,string? description)
        {
            if (ModelState.IsValid)
            {
                var food = new Food {f_name=f_name,price=price,description=description };
                string extension = Path.GetExtension(file.FileName);
                if (extension == ".jpeg" || extension==".jpg"||extension== ".png" || extension == ".gif")
                {
                    using var filestream = new FileStream(Path.Combine(path, file.FileName), FileMode.Create, FileAccess.Write);
                    file.CopyTo(filestream);
                    string image = "images/" + file.FileName;
                    food.img = image;
                    var shop = HttpContext.Session.Get<Shop>("CurrentInfo");
                    if (await _shopService.Add(food, shop!) is null)
                    {
                        ViewData["msg"] = "失败";
                        return View();
                    }
                    else
                    {
                        ViewData["msg"] = "成功";
                        return View();
                    }
                }
                else
                    return View();
            }
            else
            {
                return View();
            }
        }

        [LoginFilterAttribute<Shop>]
        public IActionResult Add()
        {
             return View();
        }
        #endregion
        #region 商家信息
        [LoginFilterAttribute<Shop>]
        public IActionResult ShopInfo()
        {
            var shop = HttpContext.Session.Get<Shop>("CurrentInfo");
            return View(shop);
        }
        #endregion
        #region 修改商品信息
        [LoginFilterAttribute<Shop>]
        public IActionResult update(int id,string name,double price, [FromForm(Name ="file")]IFormFile file)
        {
            var shop = HttpContext.Session.Get<Shop>("CurrentInfo");
            var menu = _context.Menus!
                .Where(m=>m.ShopId==shop!.id)
                .Include(m=>m.foods).First();
            var food = menu.foods
                .Where(f => f.id == id)
                .First();
            food.f_name = (name != null) ? name : food.f_name;
            food.price = (price != 0) ? price : food.price;
            if(file != null)
            {
                using var filestream = new FileStream(Path.Combine(path, file.FileName), FileMode.Create, FileAccess.Write);
                file.CopyTo(filestream);
                food.img = "images/" + file.FileName;
            }
            _context.SaveChanges();
            return RedirectToAction("index",menu.foods);
        }
        #endregion
        #region 修改商家信息
        [LoginFilterAttribute<Shop>]
        public IActionResult edit(string name, string address,string phone_number,[FromForm(Name = "file")] IFormFile file)
        {
            var currentshop = HttpContext.Session.Get<Shop>("CurrentInfo");
            var shop = _context.Shops!.Where(s => s.id == currentshop!.id).First();
            shop.s_name = name ?? shop.s_name;
            shop.address = address ?? shop.address;
            shop.phone_number = phone_number ?? shop.phone_number;
            shop.picture = (file != null) ? "images/" + file.FileName : shop.picture;
            _context.SaveChanges();
            HttpContext.Session.Set("CurrentInfo",shop);
            return RedirectToAction("ShopInfo", shop);
        }
        #endregion
        #region 删除菜品
        [LoginFilterAttribute<Shop>]
        public IActionResult delete(int id,int page)
        {
            _shopService.delete(id);
            var shop = HttpContext.Session.Get<Shop>("CurrentInfo");
            var menu = _context.Menus!.Where(m => m.ShopId == shop!.id).Include(m=>m.foods).First();
            ViewBag.pages = (menu.foods.Count % 20 == 0) ? menu.foods.Count / 20 : menu.foods.Count / 20 + 1;
            ViewBag.page = page;
            return RedirectToAction("index", menu.foods.Skip((page - 1) * 20).Take(20).ToList());
        }
        #endregion
        #region 订单信息
        [LoginFilter<Shop>]
        public IActionResult Orders()
        {
            var shop = HttpContext.Session.Get<Shop>("CurrentInfo");
            var orders = memoryCache.GetOrCreate("orders" + shop.id, factory =>
            {
                factory.AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(10);
               return  _context.Orders!
                .Where(o => (o.ShopId == shop!.id && o.status != Status.已删除))
                .Include(o => o.user)
                .Include(o => o.Foods)
                .ToList();
            });
            return View(orders);
        }
        #endregion
        #region
        [LoginFilter<Shop>]
        public IActionResult Cancel(int id)
        {
            var shop = HttpContext.Session.Get<Shop>("CurrentInfo");
            var order = _context.Shops!
                .Where(s => s.id == shop.id)
                .Include(s=>s.orders)
                .SingleOrDefault()
                .orders!
                .Where(o=>o.id==id)
                .SingleOrDefault();

            var orderList = _context.Shops!
                .Where(s => s.id == shop!.id)
                .Include(s=>s.orders)
                .SingleOrDefault()
                .orders;
            orderList!.Remove(order);
            _context.SaveChanges();
            var orders = memoryCache.GetOrCreate("orders" + shop.id, factory =>
            {
                factory.AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(10);
                return _context.Orders!
                 .Where(o => (o.ShopId == shop!.id && o.status!=Status.已删除))
                 .Include(o => o.user)
                 .Include(o => o.Foods)
                 .ToList();
            });
            return View("Orders",orders);
        }
        #endregion
    }
}

