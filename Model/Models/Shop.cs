using System;
using System.ComponentModel.DataAnnotations;

namespace Model.Models
{
	public class Shop
	{
        public long id { get; set; }

        public string s_name { get; set; }

        public string s_account { get; set; }

        public string s_password { get; set; }

        [Compare("s_password",ErrorMessage ="两次密码不一样")]
        public string s_Confirmpassword { get; set; }

        public string? address { get; set; }= "您还未填写";

        public string? phone_number { get; set; } = "您还未填写";

        public string? picture { get; set; } = "images/Shop/OIP-C (2).jpg";

        public DateTime? s_regist_time { get; set; }

        public List<Order>? orders { get; set; } = new List<Order>();

        public Menu? menu { get; set; }
        public ShopInfo? ShopInfo { get; set; }

        public Shop()
        {

        }

    }
}

