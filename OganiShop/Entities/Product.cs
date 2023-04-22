using System;
using System.Collections.Generic;

namespace OganiShop.Entities;

public partial class Product
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string Slug { get; set; } = null!;

    public decimal? Price { get; set; }

    public int? Discount { get; set; }

    public string? Description { get; set; }

    public string? Summary { get; set; }

    public double? Weight { get; set; }

    public string? Unit { get; set; }

    public string? Image { get; set; }

    public int? Quantity { get; set; }

    public int CategoryId { get; set; }

    public DateTime? CreatedDate { get; set; }

    public DateTime? UpdatedDate { get; set; }

    public string? CreatedBy { get; set; }

    public string? UpdatedBy { get; set; }

    public int? Status { get; set; }

    public bool? IsDeleted { get; set; }

    public virtual Category Category { get; set; } = null!;

    public virtual ICollection<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();

    public virtual ICollection<ProductImage> ProductImages { get; set; } = new List<ProductImage>();

    public virtual ICollection<ShoppingCart> ShoppingCarts { get; set; } = new List<ShoppingCart>();
}
