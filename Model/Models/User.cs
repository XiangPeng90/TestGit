using System;
using System.ComponentModel.DataAnnotations;

namespace Model.Models
{
	public class User
	{
        public long id { get; set; }

        public string u_name { get; set; }

        public string u_account { get; set; }

        public string u_password { get; set; }
        [Compare("u_password")]
        public string u_Confirmpassword { get; set; }

        public string? phone_number { get; set; }

        public string? avatar { get; set; }= "images/User/th.jpg";

        public DateTime u_regist_time { get; set; }

        public List<Order>? orders { get; set; } = new List<Order>();

        public Trolley? trolley { get; set; }

    }
}

