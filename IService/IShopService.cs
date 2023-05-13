using Model.Models;

namespace IService
{
    public interface IShopService : IBaseService
    {
        public Shop? Login(Shop shop);
        public Task<bool> Regist(Shop shop);
        public Task<IQueryable<List<Food>>?> Add(Food food, Shop shop);
        public List<Food> Search(Shop shop);
        public Food Search(int id);
        public List<Food> Search(int id, int page, string keywords);
        public void delete(int id);
        public void update(int id);
    }
}

