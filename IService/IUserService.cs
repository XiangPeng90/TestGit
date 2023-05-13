using Model.Models;

namespace IService
{
    public interface IUserService : IBaseService
    {
        public Task<string> Order();
        public User? Login(User user);
        public Task<bool> Regist(User user);
        public List<Shop> ShopInfo();

        public List<Food> EntryShop(int id);
        public Food Search(int id,int parm);
        public List<Food> Search(int parm);
    }
}

