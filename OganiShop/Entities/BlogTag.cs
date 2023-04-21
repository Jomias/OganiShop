using System;
using System.Collections.Generic;

namespace OganiShop.Entities;

public partial class BlogTag
{
    public int Id { get; set; }

    public int BlogId { get; set; }

    public int TagId { get; set; }

    public DateTime? CreatedDate { get; set; }

    public DateTime? UpdatedDate { get; set; }

    public string? CreatedBy { get; set; }

    public string? UpdatedBy { get; set; }

    public int? Status { get; set; }

    public bool? IsDeleted { get; set; }

    public virtual Blog Blog { get; set; } = null!;

    public virtual Tag Tag { get; set; } = null!;
}
