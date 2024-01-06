using System;
using System.Collections.Generic;

namespace dem04.EFModel;

public partial class Equipment
{
    public int Id { get; set; }

    public string? Type { get; set; }

    public string? SerialNumber { get; set; }

    public virtual ICollection<Request> Requests { get; set; } = new List<Request>();
}
