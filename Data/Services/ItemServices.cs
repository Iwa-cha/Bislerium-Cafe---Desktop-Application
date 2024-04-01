
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace DotNetCW.Data
{
    public class ItemServices
    {

        private readonly List<Item> _itemList = new()
        {
            new () { ItemName = "Cappuccino", ItemPrice = 150.0, ItemType = "Coffee" },
            new ()  { ItemName = "Latte", ItemPrice = 170.0, ItemType = "Coffee" },
            new ()  { ItemName = "Espresso", ItemPrice = 120.0, ItemType = "Coffee" },
            new ()  { ItemName = "Americano", ItemPrice = 140.0, ItemType = "Coffee" },
            new () { ItemName = "Mocha", ItemPrice = 180.0, ItemType = "Coffee" },
            new () { ItemName = "Macchiato", ItemPrice = 160.0, ItemType = "Coffee" },
            new () { ItemName = "Flat White", ItemPrice = 160.0, ItemType = "Coffee" },

            new ()  { ItemName = "Milk", ItemPrice = 20.0, ItemType = "Add-in" },
            new () { ItemName = "Sugar", ItemPrice = 10.0, ItemType = "Add-in" },
            new () { ItemName = "Vanilla Syrup", ItemPrice = 30.0, ItemType = "Add-in" },
            new () { ItemName = "Caramel Syrup", ItemPrice = 25.0, ItemType = "Add-in" },
            new ()  { ItemName = "Whipped Cream", ItemPrice = 15.0, ItemType = "Add-in" }
        };

        public void SaveItems(List<Item> coffeeList)
        {

            string dataPath = Utils.GetDesktopPath();

            string itemsPath = Utils.GetItemsPath();


            var json = JsonSerializer.Serialize(coffeeList);

            File.WriteAllText(itemsPath, json);
        }

            
        public List<Item> GetItemsFromFile()
        {
            string itemsPath = Utils.GetItemsPath();


            if (!File.Exists(itemsPath))
            {
                return new List<Item>();
            }

            var json = File.ReadAllText(itemsPath);

            return JsonSerializer.Deserialize<List<Item>>(json);
        }

            
        public void SeedItems()
        {
            List<Item> itemsList = GetItemsFromFile();

            if (itemsList.Count == 0)
            {
                SaveItems(_itemList);
            }
        }


        public Item GetItemByID(string itemID)
        {

            List<Item> items = GetItemsFromFile();
            Item item = items.FirstOrDefault(i => i.ItemID.ToString() == itemID);

            System.Diagnostics.Debug.WriteLine(itemID);

            return item;
        }


        public void UpdateItem(string itemID, double itemPrice)
        {
            List<Item> items = GetItemsFromFile();

            Item itemToUpdate = items.FirstOrDefault(i => i.ItemID.ToString() == itemID.ToString());

            itemToUpdate.ItemPrice = Math.Round(itemPrice, 2);

            SaveItems(items);
        }

        public void AddItem(Item item)
        {
            List<Item> items = GetItemsFromFile();

            items.Add(item);

            SaveItems(items);
        }

        public void DeleteItem(string itemID)
        {
            List<Item> items = GetItemsFromFile();

            Item itemToDelete = items.FirstOrDefault(i => i.ItemID.ToString() == itemID.ToString());

            items.Remove(itemToDelete);

            SaveItems(items);
        }
    }
}
