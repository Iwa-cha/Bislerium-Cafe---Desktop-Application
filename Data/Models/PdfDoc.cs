using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNetCW.Data
{
    public class PdfDoc
    {
        public List<Order> Orders { get; set; }
        public string ReportType { get; set; }
        public double TotalRevenue { get; set; } = 0;
        public List<OrderItem> Coffees { get; set; }
        public List<OrderItem> AddIns { get; set; }

        public string PdfGeneratedDate { get; set; }
    }
}
