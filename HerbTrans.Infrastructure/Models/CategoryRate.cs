using HerbTrans.Infrastructure.Enums;

namespace HerbTrans.Infrastructure.Models
{
    public class CategoryRate
    {
        public ProductCategory Category { get; set; }
        public int Rate { get; set; }
    }
}
