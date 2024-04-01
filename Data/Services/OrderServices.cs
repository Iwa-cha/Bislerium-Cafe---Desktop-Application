
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace DotNetCW.Data
{
    public class OrderServices
    {
        public List<Order> GetOrders()
        {
            string filePath = Utils.GetOrdersPath();

            if (!File.Exists(filePath))
            {
                return new List<Order>();
            }

            var json = File.ReadAllText(filePath);

            return JsonSerializer.Deserialize<List<Order>>(json);
        }

        public void PurchaseOrder(Order order)
        {
            List<Order> orders = GetOrders();
            orders.Add(order);

            string filePath = Utils.GetOrdersPath();

            var json = JsonSerializer.Serialize(orders);

            File.WriteAllText(filePath, json);
        }


        public void AddItemToCart(List<OrderItem> items, Guid itemID, string itemName, String itemType, Double itemPrice)
        {

            OrderItem item = items.FirstOrDefault(x => x.ItemID.ToString() == itemID.ToString() && x.ItemType == itemType);

            if (item != null)
            {
                item.Quantity++;
                item.TotalPrice = item.Quantity * itemPrice;

                return;
            }

            item = new()
            {
                ItemID = itemID,
                ItemName = itemName,
                ItemType = itemType,
                Quantity = 1,
                Price = itemPrice,
                TotalPrice = itemPrice
            };

            items.Add(item);

        }

        public void RemoveFromCart(List<OrderItem> items, Guid orderItemID)
        {
            OrderItem orderItem = items.FirstOrDefault(x => x.ID == orderItemID);

            if (orderItem != null)
            {
                items.Remove(orderItem);
            }
        }

        public void AdjustOrderItemQuantity(List<OrderItem> items, Guid orderItemID, string action)
        {
            OrderItem item = items.FirstOrDefault(x => x.ID == orderItemID);

            if (item != null)
            {
                if (action == "add")
                {
                    item.Quantity++;
                    item.TotalPrice = item.Quantity * item.Price;
                }
                else if (action == "subtract" && item.Quantity > 1)
                {
                    item.Quantity--;
                    item.TotalPrice = item.Quantity * item.Price;
                }
            }
        }


        public double CalcGrandTotal(IEnumerable<OrderItem> Elements)
        {
            double amount = 0;

            foreach (var item in Elements)
            {
                amount += item.TotalPrice;
            }
            return amount;
        }

        public Dictionary<string, double> RedeemFreeCoffees(int totalFreeCoffeeCount, List<OrderItem> cartOrderItems)
        {
    
            int totalRedeemedCoffeeCount = 0;
            double totalDiscountAmount = 0;


            if (cartOrderItems.Count == 0 || totalFreeCoffeeCount <= 0)
            {
                return new Dictionary<string, double>();
            }

            //Caluclating total quantity of cart
            int totalItemsQuantityInCart = cartOrderItems
                                                         .Where(item => item.ItemType == "Coffee")
                                                          .Sum(item => item.Quantity);

            // Filter and order coffee items in the cart by price in descending order.
            var coffeeItems = cartOrderItems
                .Where(item => item.ItemType == "Coffee")
                .OrderByDescending(item => item.Price)
                .ToList();

            foreach (var orderItem in coffeeItems)
            {
                // Calculate the new quantity of the coffee item after applying free coffee redemption.
                int diffBetweenCartQuantityAndFreeCoffeeCount = Math.Max(0, orderItem.Quantity - totalFreeCoffeeCount);

                int reedeemedItemQuantity = diffBetweenCartQuantityAndFreeCoffeeCount == 0 ? orderItem.Quantity : diffBetweenCartQuantityAndFreeCoffeeCount;

                // Calculate the number of redeemed coffees based on the new quantity.
                totalRedeemedCoffeeCount += reedeemedItemQuantity;

                // Calculate the discount amount for the coffee item.
                totalDiscountAmount += (orderItem.Price * reedeemedItemQuantity);

                // Update the remaining free coffee count.
                totalFreeCoffeeCount -= reedeemedItemQuantity;

                // Break the loop if no more free coffees or no more coffee items in the cart.
                if (totalFreeCoffeeCount <= 0)
                {
                    break;
                }
            }

            // Return a dictionary containing the total discount amount and the count of redeemed coffees.
            return new Dictionary<string, double>
            {
                { "discount", totalDiscountAmount },
                { "redeemedCoffeeCount", totalRedeemedCoffeeCount }
            };
        }



    }
}
