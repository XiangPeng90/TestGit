namespace Model.Models
{
    public class ShopInfo
    {
        public long id { get; set; }
        public long ShopId { get; set; }
        public Shop Shop  { get; set; }
        public Star FavorableRate { get; set; }

    }
    public enum Star
    {
        one,two, three, four, five,
    }
}
