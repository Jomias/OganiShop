using System;
using System.Collections.Generic;

namespace OganiShop.Entities;

public partial class OrderDetail
{
    public int Id { get; set; }

    public int ShopOrderId { get; set; }

    public int ProductId { get; set; }

    public int? Quantity { get; set; }

    public decimal? Price { get; set; }

    public DateTime? CreatedDate { get; set; }

    public DateTime? UpdatedDate { get; set; }

    public string? CreatedBy { get; set; }

    public string? UpdatedBy { get; set; }

    public int? Status { get; set; }

    public bool? IsDeleted { get; set; }

    public virtual Product Product { get; set; } = null!;

    public virtual ShopOrder ShopOrder { get; set; } = null!;
}
