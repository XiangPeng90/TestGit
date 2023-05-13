namespace Model.Models
{
    public class View
    {
        public long id { get; set; }

        public string view { get; set; }

        public long OrderId { get; set; }

        public Order? order { get; set; }

    }
}

