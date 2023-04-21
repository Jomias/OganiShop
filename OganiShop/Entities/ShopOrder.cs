using System;
using System.Collections.Generic;

namespace OganiShop.Entities;

public partial class ShopOrder
{
    public int Id { get; set; }

    public decimal? TotalPrice { get; set; }

    public string Account { get; set; } = null!;

    public int AddressId { get; set; }

    public DateTime? CreatedDate { get; set; }

    public DateTime? UpdatedDate { get; set; }

    public string? CreatedBy { get; set; }

    public string? UpdatedBy { get; set; }

    public int? Status { get; set; }

    public bool? IsDeleted { get; set; }

    public virtual ShippingAddress Address { get; set; } = null!;

    public virtual ICollection<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();
}
