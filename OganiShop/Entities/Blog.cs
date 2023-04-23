using System;
using System.Collections.Generic;

namespace OganiShop.Entities;

public partial class Blog
{
    public int Id { get; set; }

    public string Title { get; set; } = null!;

    public string Slug { get; set; } = null!;

    public string? Description { get; set; }

    public string? Content { get; set; }

    public string Image { get; set; } = null!;

    public int CategoryBlogId { get; set; }

    public DateTime? CreatedDate { get; set; }

    public DateTime? UpdatedDate { get; set; }

    public string? CreatedBy { get; set; }

    public string? UpdatedBy { get; set; }

    public int? Status { get; set; }

    public bool? IsDeleted { get; set; }

    public virtual ICollection<BlogTag> BlogTags { get; set; } = new List<BlogTag>();

    public virtual CategoryBlog CategoryBlog { get; set; } = null!;
}
