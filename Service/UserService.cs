using Entities;
using Model.Models;
using IService;
using Microsoft.EntityFrameworkCore;

namespace Service
{
    public class UserService : BaseService, IUserService
    {
        private readonly Context _context;
        public UserService(Context context) : base(context)
        {
            _context = context;
        }

        public User? Login(User user)
        {
            try
            {
                User u = Login<User>(u => u.u_account == user.u_account &&
                                     u.u_password == user.u_password)
                                    .Single();
                if (u != null)
                {
                    return u;
                }
                else
                {
                    return null;
                }
            }
            catch
            {
                return null;
            }
            
        }

        public async Task<bool> Regist(User user)
        {
            user.u_regist_time = DateTime.Now;
            user.trolley=new Trolley { user = user,Id=user.id };
            var u = await Regist(user, u => u.u_account == user.u_account);
            if (u is null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public Task<string> Order()
        {
            throw new NotImplementedException();
        }

        public List<Shop> ShopInfo()
        {
            List<Shop> shops = _context.Shops!.Where(s=>s.s_name!="").ToList();
            return shops;
        }
        public List<Food> EntryShop(int id)
        {
            Menu menu = _context.Menus!.Where(m => m.ShopId == id).Include(m => m.foods).First();
            return menu.foods;
        }

        public Food Search(int id,int parm)
        {
            Menu menu = _context.Menus!.Where(m => m.ShopId == parm).Include(m => m.foods).First();
            Food food = menu.foods.Where(f => f.id == id).FirstOrDefault();
            return food;
        }
        public List<Food> Search(int parm)
        {
            Menu menu = _context.Menus!.Where(m => m.ShopId == parm).Include(m => m.foods).First();
            return menu.foods;
        }
    }
}

