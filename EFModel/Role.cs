using System;
using System.Collections.Generic;

namespace dem04.EFModel;

public partial class Role
{
    public int Id { get; set; }

    public string Rolename { get; set; } = null!;

    public virtual ICollection<Worker> Workers { get; set; } = new List<Worker>();
}
