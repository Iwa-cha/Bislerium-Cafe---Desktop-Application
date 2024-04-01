using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNetCW.Data
{
    public class Item
    {
        public Guid ItemID { get; set; } = Guid.NewGuid();
        public string ItemName { get; set; }
        public double ItemPrice { get; set; }
        public string ItemType { get; set; }
    }
}
