using System;
using System.Collections.Generic;

namespace dem04.EFModel;

public partial class Worker
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string Surname { get; set; } = null!;

    public string Number { get; set; } = null!;

    public int? Role { get; set; }

    public string? Login { get; set; }

    public virtual Login? LoginNavigation { get; set; }

    public virtual ICollection<Request> Requests { get; set; } = new List<Request>();

    public virtual Role? RoleNavigation { get; set; }
}
