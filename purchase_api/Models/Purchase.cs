using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace purchase_api.Models
{
    public class Purchase
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public double Value { get; set; }
        public DateTime BuyDate { get; set; }
    }
}
