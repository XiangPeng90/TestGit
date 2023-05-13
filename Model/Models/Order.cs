using System;
namespace Model.Models
{
	public class Order
	{
		public long id { get; set; }

		public DateTime ordertime { get; set; }

		public Shop shop { get; set; }

		public User user { get; set; }

		public long UserId { get; set; }

        public long ShopId { get; set; }

		public Status status { get; set; }= 0;
        public List<Food> Foods { get; set; } = new List<Food>();
        public List<View> views { get; set; } = new List<View>();
    }

	public enum Status{
		未处理,
		已删除,
		已取消
	}
}

