using System;
using System.Collections.Generic;

namespace OganiShop.Entities;

public partial class ProductImage
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string Path { get; set; } = null!;

    public bool? IsAvatar { get; set; }

    public int ProductId { get; set; }

    public DateTime? CreatedDate { get; set; }

    public DateTime? UpdatedDate { get; set; }

    public string? CreatedBy { get; set; }

    public string? UpdatedBy { get; set; }

    public int? Status { get; set; }

    public bool? IsDeleted { get; set; }

    public virtual Product Product { get; set; } = null!;
}
