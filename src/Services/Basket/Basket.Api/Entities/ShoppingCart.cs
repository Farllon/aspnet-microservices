using System.Collections.Generic;

namespace Basket.Api.Entities
{
    public class ShoppingCart
    {
        public string UserName { get; set; }

        public IList<ShoppingCartItem> Items { get; set; } = new List<ShoppingCartItem>();

        public decimal TotalPrice => GetTotalPrice();

        public ShoppingCart()
        {

        }

        public ShoppingCart(string username)
        {
            UserName = username;
        }

        private decimal GetTotalPrice()
        {
            decimal totalPrice = 0;

            foreach (var item in Items)
                totalPrice += item.Price * item.Quantity;

            return totalPrice;
        }
    }
}
