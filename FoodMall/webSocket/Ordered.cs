using Microsoft.AspNetCore.SignalR;
using Model.Models;
using Newtonsoft.Json;

namespace FoodMall.webSocket
{
    public class Ordered:Hub
    {
        public  Task SendMessage(Order order,string msg="您有新的订单!")
        {
            return Clients.All.SendAsync("ReceiveMessage",msg,order);
        }
    }
}
