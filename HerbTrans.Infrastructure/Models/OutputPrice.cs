using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HerbTrans.Infrastructure.Enums;

namespace HerbTrans.Infrastructure.Models
{
    public class OutputPrice
    {
        public int Id { get; set; }
        public int BatchId { get; set; }
        public string Service { get; set; }
        public decimal UnitPrice { get; set; }
        public ProductCategory Category { get; set; }

    }
}
