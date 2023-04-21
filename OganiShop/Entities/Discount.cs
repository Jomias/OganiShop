using System;
using System.Collections.Generic;

namespace OganiShop.Entities;

public partial class Discount
{
    public int Id { get; set; }

    public int ProductId { get; set; }

    public int DiscountType { get; set; }

    public decimal DiscountValue { get; set; }

    public DateTime? StartDate { get; set; }

    public DateTime EndDate { get; set; }

    public DateTime? CreatedDate { get; set; }

    public DateTime? UpdatedDate { get; set; }

    public string? CreatedBy { get; set; }

    public string? UpdatedBy { get; set; }

    public int? Status { get; set; }

    public bool? IsDeleted { get; set; }

    public virtual Product Product { get; set; } = null!;
}
