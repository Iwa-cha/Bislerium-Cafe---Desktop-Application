using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNetCW.Data
{
    public class GlobalState
    {
        public User CurrentUser { get; set; }
        public string AppBarTitle { get; set; }

        public List<OrderItem> CartItems { get; set; } = new();

    }
}
