
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using DotNetCW.Data;
using System.Threading.Tasks;
using static MudBlazor.Icons;

namespace DotNetCW.Data
{
    public class MemberServices
    {
        private OrderServices _orderServices;

        public MemberServices(OrderServices orderServices) { 
            _orderServices = orderServices;
        }
        public List<Member> GetMembers()
        {
            string path = Utils.GetMembersPath();

            if (!File.Exists(path))
            {
                return new List<Member>();
            }
            var json = File.ReadAllText(path);

            return JsonSerializer.Deserialize<List<Member>>(json);

        }

        public void SaveMembers(List<Member> members)
        {
            string path = Utils.GetMembersPath();

            var json = JsonSerializer.Serialize(members);

            File.WriteAllText(path, json);
        }

        public Member GetCustomerByPhoneNum(string phoneNum)
        {
            List<Member> customers = GetMembers();
            Member customer = customers.FirstOrDefault(c => c.PhoneNum == phoneNum);
            return customer;
        }


        public void AddMember(Member _member)
        {

            List<Member> members = GetMembers();

            members.Add(_member);

            SaveMembers(members);
        }

        public void UpdateRedeemedCoffeeCount(string PhoneNum, int redeemedCoffeeCount)
        {
            List<Member> customers = GetMembers();
            Member customer = customers.FirstOrDefault(c => c.PhoneNum == PhoneNum);
            customer.CoffeeRedeemed = redeemedCoffeeCount;

            SaveMembers(customers);
        }


        public bool CheckIfCustomerIsReguralMember(string customerPhoneNum)
        {
            List<Order> orders = _orderServices.GetOrders();

            int month = DateTime.Now.Month == 1 ? 12 : DateTime.Now.Month - 1;
            int year = month == 12 ? DateTime.Now.Year - 1 : DateTime.Now.Year;

            int totalOrderCount = orders
                .Where(order => order.MemberPhoneNum == customerPhoneNum && order.OrderDate.Month == month & order.OrderDate.Year == year)
                .GroupBy(order => order.OrderDate.Day)
                .ToList().Count();

            return totalOrderCount >= 26;
        }

        public int TotalFreeCoffeeCount(string customerPhoneNum)
        {

            List<Order> orders = _orderServices.GetOrders();

            int totalOrderCount = orders
                .Where(order => order.MemberPhoneNum == customerPhoneNum)
                .ToList().Count();

            return totalOrderCount / 10;
        }

    }
}
