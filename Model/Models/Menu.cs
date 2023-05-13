using System;
namespace Model.Models
{
	public class Menu
	{
		public long id { get; set; }

		public long ShopId { get; set; }

		public Shop shop { get; set; }

		public List<Food> foods { get; set; } = new List<Food>();
	}
}

