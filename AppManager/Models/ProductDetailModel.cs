using System.Collections.Generic;

namespace AppManager.Models
{
    public class ProductDetailModel
    {
        public ProductModel Detail { get; set; }
        public List<ProductImageModel> ListImages { get; set; }
        public List<ProductModel> RelatedProducts { get; set; }
    }
}
