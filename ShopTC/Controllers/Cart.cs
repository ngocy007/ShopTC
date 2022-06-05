using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShopTC.Controllers
{
    public class Cart
    {
        string name, photo, price, quantity;

        public Cart(string name, string photo, string price, string quantity)
        {
            this.name = name;
            this.photo = photo;
            this.price = price;
            this.quantity = quantity;
        }
    }
}