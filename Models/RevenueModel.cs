using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStoreWeb.Models
{
    public class RevenueModel
    {
        public List<string> Labels { get; set; } = new List<string>();
        public List<double> Values { get; set; } = new List<double>();

        public double Total { get; set; } = 0;

        public double MaxValue { get; set; } = 0;
    }
}
