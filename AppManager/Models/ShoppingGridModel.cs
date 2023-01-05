using System.Collections.Generic;

namespace AppManager.Models
{
    public class ShoppingGridModel
    {
        public List<ProductDiscountModel> ListDiscount { get; set; }
        public List<ProductModel> ListProduct { get; set; }
        public int Count { get; set; }
    }
}
