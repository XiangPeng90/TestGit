using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Entities
{
    internal class ContextFactory : IDesignTimeDbContextFactory<Context>
    {
        public Context CreateDbContext(string[] args)
        {
            var optionBuider = new DbContextOptionsBuilder<Context>();
            optionBuider.UseMySql("Server=localhost;Database=FoodMall;port=3306;Uid=root;Pwd=12345678;",
                              ServerVersion.AutoDetect("Server=localhost;Database=FoodMall;port=3306;Uid=root;Pwd=12345678;"));
            return new Context(optionBuider.Options);
        }
    }
}
