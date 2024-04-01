using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNetCW.Data
{
    public class Member
    {
        public Guid ID { get; set; } = Guid.NewGuid();
        public string Name { get; set; }
        public string PhoneNum { get; set; }
        public int OrderCount { get; set; } = 1;

        public int CoffeeRedeemed { get; set; } = 0;

        public DateTime MembershipJoinDate { get; set; } = DateTime.Now;

        public int RedeemedCoffeeCount = 0;
    }
}
