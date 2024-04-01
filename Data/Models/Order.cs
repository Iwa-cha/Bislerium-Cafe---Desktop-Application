using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNetCW.Data
{
    public class Order
    {
        public Guid ID { get; set; } = Guid.NewGuid();
        public Guid MemberID { get; set; }
        public string MemberName { get; set; }
        public string MemberPhoneNum { get; set; }
        public DateTime OrderDate { get; set; } = DateTime.Now;
        public List<OrderItem> OrderItems { get; set; }
        public double GrandTotal { get; set; }
        public double Discount { get; set; } = 0;

    }
}
