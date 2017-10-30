using HerbTrans.Infrastructure.Enums;

namespace HerbTrans.Infrastructure.Models
{
    public class Price
    {
        public int Id { get; set; }
        public string Service { get; set; }
        public decimal UnitPrice { get; set; }
        public ProductCategory Category { get; set; }
        public int ShowRate { get; set; }
    }
}
