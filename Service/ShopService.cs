using Entities;
using Model.Models;
using IService;
using Microsoft.EntityFrameworkCore;

namespace Service
{
    public class ShopService : BaseService, IShopService
    {
        private readonly Context _context;
        public ShopService(Context context) : base(context)
        {
            _context = context;
        }

        public async Task<IQueryable<List<Food>>?> Add(Food food, Shop shop)
        {
            Menu menu = _context.Menus!.Where(m => m.ShopId == shop.id).Single();
            menu.foods.Add(food);
            await _context.SaveChangesAsync();
            return _context.Menus!.Where(m => m.ShopId == shop.id).Select(m => m.foods).Where(f => f.Contains(food));
        }

        public Shop? Login(Shop shop)
        {
            Shop s = Login<Shop>(s => s.s_account == shop.s_account && s.s_password == shop.s_password).SingleOrDefault();
            return s;
        }

        public async Task<bool> Regist(Shop shop)
        {
            shop.s_regist_time = DateTime.Now;
            shop.menu = new Menu { ShopId=shop.id};
            var s = await Regist(shop, s => s.s_account == shop.s_account);
            if (s is null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public List<Food> Search(Shop shop)
        {
           Shop? s = _context.Shops!.Include(s => s.menu).Where(s=>s.id==shop.id).First();
           List<Food>? foods = _context.Foods!.Where(f => f.menuId == s.menu!.id).ToList();
           return foods;
        }
        public List<Food> Search(int id, int page,string keywords)
        {
            Menu? menu = _context.Menus!.Where(m => m.id==id).Include(m=>m.foods).First();
            List<Food>? foods = menu.foods.Where(f => f.f_name.Contains(keywords)).Skip((page - 1) * 20).Take(20).ToList();
            return foods;
        }

        public Food Search(int id)
        {
            Food? food = _context.Foods!.Where(f => f.id == id).SingleOrDefault();
            if (food is null)
            {
                return new Food(){id = 0};
            }
            return food;
        }
        public void delete(int id)
        {
            var food = _context.Foods!.Where(f => f.id == id).First();
            _context.Foods!.Remove(food);
            _context.SaveChanges();
            File.Delete("wwwroot/"+food.img!);
        }

        public void update(int id)
        {
            var food = _context.Foods!.Where(f => f.id == id).Single();
            
            throw new NotImplementedException();
        }
    }
}

