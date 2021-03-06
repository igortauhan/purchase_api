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
        public string Secret { get; set; }

        public Purchase()
        {
        }

        public Purchase(long id, string name, double value, DateTime buyDate, string secret)
        {
            Id = id;
            Name = name;
            Value = value;
            BuyDate = buyDate;
            Secret = secret;
        }
    }
}
