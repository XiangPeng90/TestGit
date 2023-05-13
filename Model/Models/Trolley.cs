namespace Model.Models
{
    public class Trolley
    {
        public long Id { get; set; }
        public User user { get; set; }
        public long userId{ get; set; }
        public List<Food>? foods { get; set; }=new List<Food>();    
    }
}
