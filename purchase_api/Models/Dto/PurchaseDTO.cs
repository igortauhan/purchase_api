using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace purchase_api.Models.Dto
{
    public class PurchaseDTO
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public double Value { get; set; }
        public DateTime BuyDate { get; set; }

        public PurchaseDTO()
        {
        }

        public PurchaseDTO(long id, string name, double value, DateTime buyDate)
        {
            Id = id;
            Name = name;
            Value = value;
            BuyDate = buyDate;
        }
    }
}
