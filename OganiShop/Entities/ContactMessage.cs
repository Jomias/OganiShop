using System;
using System.Collections.Generic;

namespace OganiShop.Entities;

public partial class ContactMessage
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public string? Email { get; set; }

    public string? Message { get; set; }

    public DateTime? Time { get; set; }
}
