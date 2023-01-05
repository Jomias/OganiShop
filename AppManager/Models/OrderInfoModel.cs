using System.Collections.Generic;

namespace AppManager.Models
{
    public class OrderInfoModel
    {
        public ShopOrderModel ShopOrder { get; set; }
        public List<OrderDetailModel> ListOrderDetail { get; set; }
    }
}
