namespace Model.Models
{
//Test Git
	public class Food
	{
		public long id { get; set; }

		public string f_name { get; set; }

        public string img { get; set; }

		public double price { get; set; }

		public Menu menu { get; set; }

		public int count { get; set; } = 1;
        public string? description { get; set; }

        public long menuId { get; set; }
		public List<Order>? orders { get; set; }
		public List<Trolley>? trolleys { get; set; }
	}
}

